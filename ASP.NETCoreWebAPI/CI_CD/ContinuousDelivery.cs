namespace ASP.NETCoreWebAPI.CI_CD;

public class ContinuousDelivery
{
    //Continuous Delivery - set the code to the production state with manual acceptation
    //This will be also named pipieline

    //GitHub repository is being triggered (for example merge to the master branch)
    //-> publish application
    //-> deploy developer environment (can be also test environment and other)
    //-> deploy production environment (with manual approval)
}