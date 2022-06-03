namespace CsharpBasics.Introduction;

public class Comments
{
    //In C# there are two basic types of comments
    //The first one is by using "//"

    /*
     This is a second one, for multiline comments
    */

    //We can also comment-like method descriptions (to use it type "///" above the method):

    /// <summary>
    /// This is my described method
    /// </summary>
    /// <param name="stringInput">This is a first parameter</param>
    /// <param name="intInput">This is a second parameter</param>
    /// <returns>This method return only 4</returns>
    public int MyDescribedMethod(string stringInput, int intInput)
    {
        return 4;
    }

    //There is also a tracking mechanism for comments with "TODO", "UNDONE", "HACK" and "UnresolvedMergeConflict" tokens
    //Mostly use "TODO" to mark parts of code that requires correction, refactor or other. For instance:
    //TODO: create a new method to calculate employee bonus
    //In order to search to such "TODO's" we need to open the "Task List"
}