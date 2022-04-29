namespace ASP.NETCoreWebAPI.Dockerize.DockerCompose;

public class DockerCompseFiles
{
    //Docker compose file named "docker-compose.yaml" or "docker-compose.yml" are use to automatize the process presented in the "CreatingNetworks" file

    //The key command to use the docker-compose.yaml is:
    //docker-compose up

    //The base structure of this file is:

    #region Example docker-compole.yaml

    /*

 version: '3.1'

services:    
  api:                                          #determines the container for WebApi
    container_name: api-container               #write the container name
    build:
      context: .                                #set the context to the current folder
      dockerfile: Dockerfile                    #specify the name of the Dockerfile
    image: api                                  #determine the image from which the container will be created
    networks:
      - db-net                                  #connect the container to the network name "db-net"
    ports:
      - 8081:80                                 #specify the ports
    depends_on:
      - "db"                                    #explicitly tell that this container will rely on container "db"
  db:
    image: mysql:latest
    container_name: mysql-container
    volumes:
      - dbdata:/var/lib/mysql                   #determines the volume connected to this container
    environment:
     MYSQL_ROOT_PASSWORD: test
     MYSQL_DATABASE: TestDataBase
     MYSQL_USER: marek
     MYSQL_PASSWORD: hunter
    ports:
      - 5432:5432
    networks:
      - db-net

networks:
  db-net:                                       #define the network

volumes:
  dbdata:                                       #define the volume

    */

    #endregion Example docker-compole.yaml
}