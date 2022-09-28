# NPlusOneProblem

N+1 problem with bad loops:

```csharp
app.MapGet("NPlusOneProblemWithLooping", async (MyBoardsContext db) =>
{
    var users = await db.Users
        .Include(u => u.Address)
        //The solution is the same as in Lazy Loading
        .Include(u => u.Comments)
        //
        .Where(u => u.Address.Country == "Albania")
        .ToListAsync();

    foreach (var user in users)
    {
        //This line will cause the n+1 problem ('n' time SELECT)
        //var userComments = db.Comments.Where(c => c.AuthorId == user.Id).ToList();

        //Solution
        var userComments = user.Comments;

        foreach (var comments in userComments)
        {
            //Process(comment)
        }
    }
});
```
N+1 problem with Lazy Loading:

```csharp
app.MapGet("NPlusOneProblemWithLazyLoading", async (MyBoardsContext db) =>
{
    //Turn on Lazy Loading for this endpoint
    var users = await db.Users
        .Include(u => u.Address)
        .Where(u => u.Address.Country == "Albania")
        //To solve this problem we add this line to get all the record, even if we use the Lazy Loading
        .Include(u => u.Comments)
        //The above line will deal with the problem described in this endpoint
        .ToListAsync();
    //First SELECT will be performed

    foreach (var user in users)
    {
        foreach (var comments in user.Comments)
        {
            //Process(comment)
            //due to the Lazy Loading, for each iteration, one SELECT will be executed. To sum up there will be 'n' SELECT's
        }
    }
    //Therefore 1+n SELECT's will be executed
});
```