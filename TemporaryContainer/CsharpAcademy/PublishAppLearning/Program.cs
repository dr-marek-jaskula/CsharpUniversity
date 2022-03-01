using System;

namespace PublishAppLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadKey();
            //dot net can run in the command line
            //TFM -> Target Framework Monikers
            //how to publish an application when we are done

            //1.
            //debug version
            //release version
            //2.
            //Publish our app!! right click on project, then select "publish"
            //Next select one of the options:
            //Cloud (Microsoft prefer way)
            //Docker (good way)
            //Folder (standard)

            //we select Folder -> and give publish directory
            //after the publish profile is done
            //we edit the Target Runtime !! its very important
            //IMPORTANT IS: deployment mode:
            //Framework-dependent : this gives like 168kb, and its like bin (web apps a bit different)
            //Self-contained : this will give all files to run this on the specified environment (its takes longer and make many files: 224 with 64MB) -> all files for .net to run the app (many to many) - but it does not need to install dot net of machine that runs it
            //WE CAN SPECIFY: File publish options ->

            //1. we apply -> produce single file -> this will give us just a few file with execute file (6 files with 58MB and still machine that runs done need to install .Net)
            //2. we can apply (still in preview but its interesting) -> Trim unused assemblies -> this will eliminate all redundant libraries of .Net and keep just important ones, so your app will be much more smaller (but its still in preview! so be careful)
            //3. we can apply -> ReadyToRun -> this will optimize the startup performance but for some cost. Read this:
            /*
             In general, the size of an assembly will grow to between two to three times larger. This increase in the physical size of the file may reduce the performance of loading the assembly from disk, and increase working set of the process. However, in return the number of methods compiled at runtime is typically reduced substantially. The result is that most applications that have large amounts of code receive large performance benefits from enabling ReadyToRun. Applications that have small amounts of code will likely not experience a significant improvement from enabling ReadyToRun, as the .NET runtime libraries have already been precompiled with ReadyToRun.

            The startup improvement discussed here applies not only to application startup, but also to the first use of any code in the application. For instance, ReadyToRun can be used to reduce the response latency of the first use of Web API in an ASP.NET application.
             */
        }
    }
}
