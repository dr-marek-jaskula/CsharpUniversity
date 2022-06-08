namespace CsharpAdvanced.AsyncProgramming;

//ValueTask is a alternative to Task, and it can be awaited
//ValueTask is a structure (struct), while Task is a reference type (class)
//ValueTask is either value (a type like value type "int" or reference type "GitHubUser") or a Task.
//Can not be both.
//Therefore, in some rare cases it can be used to improve performance, but we need to be careful, because there are traps

//The best use case for ValueTask is when we get have caching:
//If the value is not retrieved from cache but from other server (request to different API), we need to return and await a Task
//However, when the value is retrieved for cache we do not need to return a Task but just a value in the cache.
//Therefore, we can use ValueTask in such case to improve performance
public class ValueTasks
{
    //our "cache"
    private string? cachedName;

    public async Task<string?> GetNameWithTaskReturnType()
    {
        //second time we just return a cached "Jackob". It is inefficient to return it as a Task, because we would allocate more memory
        if (cachedName is not null)
            return cachedName;

        var name = await Task.Run(() =>
        {
            Task.Delay(200);
            return "Jackob";
        });

        cachedName = name;

        return name;
    }

    public async ValueTask<string?> GetNameWithValueTaskReturnType()
    {
        //second time we just return a cached "Jackob". It is efficient to return it as a ValueTask, because we would not allocate more memory
        if (cachedName is not null)
            return cachedName;

        var name = await Task.Run(() =>
        {
            Task.Delay(200);
            return "Jackob";
        });

        cachedName = name;

        return name;
    }
}