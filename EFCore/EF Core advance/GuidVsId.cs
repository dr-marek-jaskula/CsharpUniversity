namespace EFCore.EF_Core_advance;

public class GuidVsId
{
    //If we use a sequential "Id" for example for deliveries, then in endpoint like:
    //GET "/api/delivery/4" we get the forth delivery.
    //Therefore, the client can think "if I use "/api/delivery/5"" request, then I will get the others person delivery
    //This is a serious security leak.

    //In order to deal with this problem we can use guid's.
    //There is no chance that the client will come up with other guid.

    //We can also use "hashids" package:
    //It creates YouTube-like hashes (a bit shorter) based on a seed we plug in (they are deterministic to the seed)
}