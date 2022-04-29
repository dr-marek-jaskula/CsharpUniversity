namespace ASP.NETCoreWebAPI.Dockerize.DockerCommands;

public class BuildStartImageContainerCommands
{
    //Build docker image with name "hello-docker" and tag "1.0.0". Its possible to use tag "latest"
    //docker build -t hello-docker:1.0.0 .
    //Its important to note that at the end of the formula there has to be space and dot i.e. " .". Otherwise, we would get error. Its out telling compiler that build files in current folder

    //Create the container named "first-container" from image "hello-docker:1.0.0" and expose it to the port 8080:80
    //docker create --name first-container -p 8080:80 hello-docker:1.0.0

    //Create and starts the container. Multiple containers can run at the same time
    //docker run -d --name first-container -p 8080:80 hello-docker:1.0.0

    //Starts the container given by "f82cc3"
    //docker start f82cc3

    //Stops the container given by "f82cc3"
    //docker stop f82cc3

    //Removes the container given by "f82cc3"
    //docker rm f82cc3

    //Removes the image given by "f1a8"
    //docker rmi f1a8
}