	#Build stage

#1. Get SDK image from Public Repository as a "build"
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

#2. Set working directory to /app
WORKDIR /app

#3. Copy the .csproj file to the working directory
COPY *.csproj ./

#4. Restore dependencies present in the project file.
RUN dotnet restore

#5. Cope all project files to the working directory
COPY . ./

#6. Build ASP.NETCoreWebAPI project in the Release mode and set the build output to the /app/build directory
RUN dotnet build "ASP.NETCoreWebAPI.csproj" -c Release -o /app/build

	#Publish stage

#1. From build image crated in the previous set, create a new image named "publish"
FROM build AS publish

#2. Publish the application (with no restore, becasue restore was done previously) and set the output directory to /app/publish
RUN dotnet publish "ASP.NETCoreWebAPI.csproj" -c Release -o /app/publish --no-restore

	#Final stage

#1. Use aspnet image to create an final image (we would not use the sdk here, just what is enough to run the app)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

#2. Set the name of the image owner (metadata)
LABEL Name="Eltin"

#3. Set the working directory to /app
WORKDIR /app

#4. Expose the docker container to ports: 80, 443
EXPOSE 80
EXPOSE 443

#5. Copy from ""publish" image the whole directory "/app/publish" to the current directory (which is "/app")
COPY --from=publish /app/publish .

#6. Set the entrypoint of the container to the ASP.NETCoreWebAPI.dll
ENTRYPOINT ["dotnet", "ASP.NETCoreWebAPI.dll"]