namespace ASP.NETCoreWebAPI.Dockerize.Volumes;

public class VolumesBasics
{
    //Data will perish when the container closes
    //Volumes are virtual discs that will be connected to the container and store the data modification done in the container

    //Basic commands:

    //Creates a volume name "my-volume"
    //docker volume create my-volume

    //Returns the list of all volumes
    //docker volume ls

    //Deletes the volume named "my-volume"
    //docker volume rm my-volume

    //The automatized process of connecting to the volume is made by "docker-compose.yaml" file
}