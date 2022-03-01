namespace CsharpAdvanced.Memory;

public class MemoryAllocation
{

    #region Stack and Heap

    //Stack is the part of the RAM memory. Data stored in stack are linear in sense that it has layers. Therefore, it fast way
    //to store data, however it is limited (famous "stack overflow"). Memory at stack is static. The stack is always reseed in a
    //LIFO (last in first out); the most recently reserved block is always the next block to be freed.
    //This makes it really simple to keep track of the stack; freeing a block from the stack is nothing more than adjusting one pointer.

    //The heap is the part of RAM memory set aside for dynamic allocation.
    //Unlike the stack, there's no enforced pattern to the allocation and deallocation of blocks from the heap; you can allocate a
    //block at any time and free it at any time. This makes it much more complex to keep track of which parts of the heap are allocated or
    //free at any given time

    //All reference type are allocated on the heap (like object), however they pointers are stored in the stack.

    //Value type can be allocated on the stack or on the heap.
    //It depends:
    //If the variable is created as the top level class variable
    //So the value types can be allocated on the heap - it depends where they were created
    //If value type is created for example in a method it is allocated on the stack

    //If the instance of the "MemoryAllocation" class is created, this variable will be allocated with its parent, so on the heap
    public int valueAllocatedOnTheHeeap = 100;

    //If this method is called, the parameter "number" (that is value type) is allocated on the stack.
    //The garbage collector will release memory after the method executes
    public void SomeMethod(int number)
    {
    }

    //If this method is called, the variables defined in the methods scope (that are value type) will be allocated on the stack.
    //The garbage collector will release memory after the method executes
    public void SomeMethod2()
    {
        int luckyNumber = 12;
        int veryLuckyNumber = 13;
        int theMostLuckyNumber = 15;

        //if these variable get boxed, for example by some other method, they will go to the heap. For example by:
        string.Format("Our lucky numbers are {0}, {1}, {2}", luckyNumber, veryLuckyNumber, theMostLuckyNumber);
        //It is because the Format method box the value by considering them as 'object' (examine the type of Format parameters)
    }

    //The same goes with a "struct".

    #endregion Stack and Heap

    #region Garbage Collector and Generations 0,1,2
    
    //Garbage Collector (short. GC) is a mechanism that runs in a background in different thread 
    //The aim of the GC is to free the memory allocated in the heap if there are no pointers to it (that it has no references point to them)
    
    //Generation 0 )short. Gen 0) is the 'bucked' to which we put the freshly created objects. At first GC searches object in the Generation 0, then Generation 1 and then Generation 2
    //So Generation 1 (short. Gen 1) is the 'bucked' in which we 'store' object that were not removed by GC from Generation 0
    //Generation 2 (short. Gen 2) is the 'bucked' in which we 'store' object that were not removed by GV from Generation 1

    //The idea is to optimize the GC performance.
    //The object that were used for a long time should be examined by GC lastly
    //The fresh object are suspected to have a short life cycle

    #endregion Garbage Collector and Generations 0,1,2
}