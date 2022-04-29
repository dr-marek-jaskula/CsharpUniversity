namespace ASP.NETCoreWebAPI.Dockerize;

//In this file the general overview is presented

//Images are the readonly files that represents the instruction how to build a container (a bit like a class-instance relation)
//Images can have layers, one on another like pancakes

//Containers are like virtual machines created from the blueprint provided by the image
//Images can be downloaded from the DockerHub or other public repository

//Data will perish when the container closes. To deal with this problem go to: Volumes

//The automatized process of connecting to the volume to the container and creating a network between different container is made by "docker-compose.yaml" file approach

public class DockerReadMe
{
    //Choosing the Linux or Windows container can be specified in the Docker Desktop -> choose Linux containers

    //In order to containerize the WebApi in the preferred way we should create a file named "Dockerfile" (text file with no extension).
    //The file can have other name but the good practice is to name it "Dockerfile" and store it in the root folder of our application (we can keep in elsewhere if necessary).

    //The Dockerfile is the "blueprint" to containerize the application.
    //The result is the readonly image in the container (image can multilayer image).
    //The ENTRYPOINT or CMD keywords in the Dockerfile determines the commands that will be executed when we run the container.
    //For details examine other files.

    //Comments in Dockerfile can be created by '#'

    #region Example Dockerfile:

    /*
    #See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
    FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
    WORKDIR /app
    EXPOSE 80
    EXPOSE 443

    FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
    WORKDIR /src
    COPY ["ASP.NETCoreWebAPI/ASP.NETCoreWebAPI.csproj", "ASP.NETCoreWebAPI/"]
    RUN dotnet restore "ASP.NETCoreWebAPI/ASP.NETCoreWebAPI.csproj"
    COPY . .
    WORKDIR "/src/ASP.NETCoreWebAPI"
    RUN dotnet build "ASP.NETCoreWebAPI.csproj" -c Release -o /app/build

    FROM build AS publish
    RUN dotnet publish "ASP.NETCoreWebAPI.csproj" -c Release -o /app/publish

    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .
    ENTRYPOINT ["dotnet", "ASP.NETCoreWebAPI.dll"]
     */

    #endregion Example Dockerfile:

    //Next, if we need to have multiple container to with together, we create a new file with a name:
    //"docker-compose.yaml" - popular way (USE THIS because for example auto generated .dockerignore has "docker-compose*"
    //"compose.yaml" - most preferred way
    //"compose.yml" - second preferred way
    //"docker-compose.yml" - least preferred way

    //File is YAML file where we define:
    //1. services (webs -> networks, needed to make container with together)
    //2. volumes (write-read objects, connected to containers, important for databases, because images are just readonly)
    //3. configuration (for additional configurations)
    //4. secrets (for keeping the sensitive data hidden)

    #region Example compose file:

    /*
    services:
      frontend:
        image: awesome/webapp
        ports:
          - "443:8043"
        networks:
          - front-tier
          - back-tier
        configs:
          - httpd-config
        secrets:
          - server-certificate

      backend:
        image: awesome/database
        volumes:
          - db-data:/etc/data
        networks:
          - back-tier

    volumes:
      db-data:
        driver: flocker
        driver_opts:
          size: "10GiB"

    configs:
      httpd-config:
        external: true

    secrets:
      server-certificate:
        external: true

    networks:
      # The presence of these objects is sufficient to define them
      front-tier: {}
      back-tier: {}
     */

    #endregion Example compose file:
}