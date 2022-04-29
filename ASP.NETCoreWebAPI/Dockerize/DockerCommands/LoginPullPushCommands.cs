namespace ASP.NETCoreWebAPI.Dockerize.DockerCommands;

public class LoginPullPushCommands
{
    //Try to log u on your hub.docker account, for example to push to DockerHub repository
    //docker login

    //Downloads the latest version of the postgres image from hub.docker.com as a image named "postgres"
    //docker pull postgres

    //Push image to your public repository on docker.hub.
    //You need to be logged if u want to push something on docker.hub.
    //IMPORTANT: the name of the image must be like: username/something

    //Push the image "hello-docker:1.0.0" named "eltin/my_image"
    //docker tag hello-docker:1.0.0 eltin/my_image
}