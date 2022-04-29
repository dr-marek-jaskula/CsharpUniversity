namespace ASP.NETCoreWebAPI.Dockerize.DockerCommands;

public class WorkingWithImagesContainersCommands
{
    //Executes the command "cat" (which is read) with parameter "skni.txt" (some text file) in active container named "elastic_branhmagupta"
    //docker exec elastic_brahmagupta cat skni.txt

    //Get inside the container ContA
    //docker attach ContA

    //Creates the image on container f82cc3 with name new_image_layer. That make a new layer, to the previous image (for example after some changes)
    //docker commit f82cc3 new_image_layer

    //Important command, that allows us to copy file from container to host, and from host to container, without entering the container
    //docker cp

    //Copy file skni.txt from container named "containername" to current folder. We can use container id of course
    //docker cp containername:/skni.txt

    //Revers copying
    //docker cp skni.txt containername:/

    //Copying can be automatically, using the docker file, but sometimes manual investigation is important
}