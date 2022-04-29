namespace ASP.NETCoreWebAPI.Dockerize.DockerCommands;

public class EnvironmentalVariables
{
    //Environmental variables are useful

    //Prints all the environmental variables from the operating system
    //docker run ubuntu env

    //Adds a new environmental variable
    //docker run -e MY_NEW_VARIABLE = true ubuntu env

    //It is common to share settings between containers in the following manner
    //docker run -e DATABASE = my_database ubuntu env
}