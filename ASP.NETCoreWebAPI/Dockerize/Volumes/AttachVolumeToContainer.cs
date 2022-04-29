namespace ASP.NETCoreWebAPI.Dockerize.Volumes;

public class AttachVolumeToContainer
{
    //Creates and starts the container with auto-generated name based on a image "my_image". Additional variable is --volume my_volume (pointing at a volume named my_volume)
    //Each time we save data in directory "my_folder" they will be saved in volume "my_volume"
    //docker run --volume my_volume:/my_folder my_image

    //We can create an anonymous volume instead
    //docker run --volume /my_folder my_image

    //We can also attach the files from the host to the container directly. However, the path needs to be absolute one
    //docker run --volume D:\Docker\volumesLearning/my_folder:/my_folder my_image
}