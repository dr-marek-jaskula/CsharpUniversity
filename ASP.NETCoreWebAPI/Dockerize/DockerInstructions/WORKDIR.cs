namespace ASP.NETCoreWebAPI.Dockerize.DockerInstructions;

public class WORKDIR
{
    //WORKDIR instruction makes that RUN, CMD, ENTRYPOINT, COPY and ADD instructions in our Dockerfile will be executed in given directory
    //If it doesn’t exist it will be created (even if it wouldn’t be used).

    //Example: WORKDIR /app
}
