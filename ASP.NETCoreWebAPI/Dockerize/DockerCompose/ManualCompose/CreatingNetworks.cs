namespace ASP.NETCoreWebAPI.Dockerize.DockerCompose;

public class CreatingNetworks
{
    //Creates a network named "best_network"
    //docker network create best_network

    //Creates new network named "my-network" (--driver by default is bridge, but here we explicitly specify it)
    //docker network create --driver bridge my-network

    //Creates and runs the container named "my_container" created with network "my-network" and from image "my_image"
    //-dit ->
    //d stats for --detach (Run container in background and print container ID)
    //i states for --interactive (Keep STDIN open even if not attached)
    //t states for --tty (Allocate a pseudo-TTY)
    //docker run -dit --name my_container --network my-network my_image

    //To inspect the working network
    //docker network inspect my-network

    //Connect my-network to container my_second_container, my_second_container will have two networks and its ok
    //docker network connect my-network my_second_container

    //Connect to database postgres though networks:
    //docker run --name database_container --volume my_volume:/var/lib/postgresql/data -e POSTGRES_DB = myDatabase - e POSTGRES_USER=eltin -e POSTGRES_PASSWORD = myPassword --network best_network --detach postgres

    //To sum up the above line:
    //We create and run container name "database_container" with volume "my_volume" in folder "/var/lib/postgresql/data".
    //Next, we set the postgres environmental variables
    //Then, we connect the container to the network "best_network"
    //We container is created using the image postgres (--detach is use to run it in the background)

    //Now we use container base on the adminer image (from docker hub) on port 8080 and connected to the network "best_network"
    //docker run -p 8080:8080 --network best_network adminer
}