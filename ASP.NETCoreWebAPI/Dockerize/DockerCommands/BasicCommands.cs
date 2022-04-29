namespace ASP.NETCoreWebAPI.Dockerize.DockerCommands;

public class BasicCommands
{
    //Show all docker options
    //docker

    //Show all docker images on the machine
    //docker images

    //Show history of image given by image id starting from b9da while this starting string is unique within the image ids
    //docker image history b9da
    //the full image id can be like "f1a8244ea0d9" or much longer, but to ref to it we can do shortcut like "l1a82" if its unique

    //These two commands give the same: the containers list. Here "-a" states for "all". Moreover ps is shortcut from "proceses"
    //docker container ls -a
    //docker ps -a
    //Without writing "-a" we would get only the active containers.

    //This gives massive information about container b5d, in json format
    //docker inspect b5d

    //Examines the ip addresses in the container (of running one when we are inside the container)
    //ip addr

    //Specify custom dockerfile in the current directory
    //docker build -f hehe.Dockerfile .
}