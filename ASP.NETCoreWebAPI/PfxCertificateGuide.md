## Pfx Certificate Guide

Certificate is important for docker containers 

1) In a project folder write:
> dotnet dev-certs https -ep cert.pfx -p Test1234!

Where:

This is from docker-compose:
```yaml
  api:
    build: .
    ports:
      - "5001:443"
      - "5000:80"
    environment:
      - ASPNETCORE_URLS=https://+:443;http://+:80
      - ASPNETCORE_Kestrel__Certificates__Default__Password=Test1234!
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/cert.pfx
      - ASPNETCORE_Environment=Production
      - MyApi_Database__ConnectionString=Server=db;Port=5432;Database=mydb;User ID=course;Password=changeme;
    depends_on:
      db:
        condition: service_started
```

2) In Dockerfile, before ENTRYPOINT copy the certificate so write:

```
COPY --from=build-env /app/out .
# Run this to generate it: dotnet dev-certs https -ep cert.pfx -p Test1234!
COPY ["cert.pfx", "/https/cert.pfx"]
ENTRYPOINT ["dotnet", "Customers.Api.dll"]
```
