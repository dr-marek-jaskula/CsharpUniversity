namespace ASP.NETCoreWebAPI.Dockerize.DockerizeDatabase;

public class DockerMySql
{
    //To get into the container:
    //1. Get the container id
    //docker ps -a
    //Open bash inside the running container
    //docker exec -it mysql-container bash
    //Log in
    //mysql -u root -ptestPaswword

    //In order to import database to the container with the volume and MySql we need to:

    //1. Export database in form of sql file(dummy file)
    //2. Use command cp to copy this file to container without even running it: (first get to current folder)
    //docker cp TestDataBase.sql mysql-container:/

    //3. Import database inside the container (so firstly get inside the container)
    //mysql -u root -ptest TestDataBase < TestDataBase.sql

    #region MySql docker-compose.yaml

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

    #endregion MySql docker-compose.yaml
}