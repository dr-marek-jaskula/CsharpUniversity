using System.Security.Cryptography;

namespace CsharpBasics.Introduction;

internal static class RandomShuffle
{
    private static Random rng = new();

    //Basic Way to Randomly Shuffle the list : without much effort put on security
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    //Safer way to shuffle the list using cryptography
    public static void Shuffle2<T>(this IList<T> list)
    {   
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

//For thread safty
public static class ThreadSafeRandom
{
    //this attribute says that the static value is unique for each thread
    [ThreadStatic] private static Random _local = new();

    public static Random ThisThreadsRandom
    {
        get { return _local ?? (_local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
    }
}

static class MyExtensions
{
    //Threads safty method
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

