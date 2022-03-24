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