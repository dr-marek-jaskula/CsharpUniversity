namespace ASP.NETCoreWebAPI.Dockerize.DockerCompose;

public class ComposeBasics
{
    //List of docker network commands:
    //1) connect
    //2) create
    //3) disconnect
    //4) inspect
    //5) ls
    //6) prune(remove all unused networks)
    //7) rm(remove one or more networks)

    //Types of network driver:
    //bridge
    //host
    //none

    //Bridge allows to connect two containers and to host and to Internet.
    //Docker automatically makes one such network that is connected to every containers (unless we specify we done want to).
    //We can make more such networks.

    //To list the existing networks we use:
    //docker network ls

    //Get into the container "ContA" (can write commands in the container (like command line there))
    //docker attach ContA

    //Examines addresses in this container
    //ip addr

    //Examines that we can use Internet in the container (ping Google)
    //ping google.com

    //Private ping the private IPv4Address of private ContB (get from docker network inspect bridge)
    //ping 172.17.0.3

    //To inspect the working network
    //docker network inspect my-network
}