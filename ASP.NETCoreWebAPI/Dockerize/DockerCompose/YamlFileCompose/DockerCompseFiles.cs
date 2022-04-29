namespace ASP.NETCoreWebAPI.Dockerize.DockerCompose;

public class DockerCompseFiles
{
    //Docker compose file named "docker-compose.yaml" or "docker-compose.yml" are use to automatize the process presented in the "ManualCompose" folder and also automatize the process of connecting to the volume
    //Keep the docker-compose.yaml file in the same directory that Dockerfile

    //The key command to use the docker-compose.yaml is:
    //docker-compose up

    //The base structure of this file requires tabulator (spacer) in the proper manner (like in python):

    //IMPORTANT: yaml files are easy to make an error due to the invalid formatt
    //yaml validator: https://codebeautify.org/yaml-validator

    #region Example docker-compole.yaml

    /*

version: '3'

services:
  api:
    container_name: api-container
    build:
      context: .
      dockerfile: Dockerfile
    image: api
    networks:
      - db-net
    ports:
      - 8081:80
    depends_on:
      - "db"
  db:
    image: mcr.microsoft.com/mssql/server:2019-latest
    container_name: sql-container
    volumes:
      - dbdata:/var/lib/mysql
    environment:
      SA_PASSWORD: My_password123
      ACCEPT_EULA: Y
    ports:
      - 5434:1433
    networks:
      - db-net

networks:
  db-net:

volumes:
  dbdata:

    */

    #endregion Example docker-compole.yaml
}