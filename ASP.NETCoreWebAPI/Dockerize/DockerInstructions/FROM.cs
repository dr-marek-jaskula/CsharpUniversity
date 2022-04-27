using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace ASP.NETCoreWebAPI.Dockerize;

public class FROM
{
    //FROM instruction initializes a new build stage and sets the Base Image for subsequent instructions. Dockerfile must start with FROM instruction.

    //Good practice is to give a a build state a name (alias) by "AS" operator
    //Example:
    //FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

    //The image can be any valid image. In many cases the image will be pulled from Public Repository like above.

    //Images in Public Repositories that are important:

    //dotnet sdk (for dotnet)
    //"mcr.microsoft.com/dotnet/sdk:6.0"
    //This image contains the .NET SDK which is comprised of three parts:
    //1) .NET CLI
    //2) .NET runtime
    //3) ASP.NET Core
    //Use this image for your development process (developing, building and testing applications).
    //https://hub.docker.com/_/microsoft-dotnet-sdk/

    //dotnet aspnet (for web api)
    //"mcr.microsoft.com/dotnet/aspnet:6.0"
    //This image contains the ASP.NET Core and .NET runtimes and libraries and is optimized for running ASP.NET Core apps in production.
    //https://hub.docker.com/_/microsoft-dotnet-aspnet
}
