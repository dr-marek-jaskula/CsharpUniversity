namespace ASP.NETCoreWebAPI.Dockerize.DockerizeDatabase;

public class DockerPostgres
{
    //Creates and runs a container named "postgresDatabase" and let is work in the background (by "--detach"). Then we set the environmental variable POSTGRES_PASSWORD, because it is required when created a postgres container
    //docker run --name postgresDatabase --detach -e POSTGRES_PASSWORD = my_password postgres

    //We execute the command "psql" in the running container "postgresDatabase"
    //docker exec -it postgresDatabase psql --username postgres
    //Then some SQL code
    //CREATE TABLE my_table();
    //SELECT * FROM my_table;

    //This command is do exit the postgres console
    //\q

    //jest to specyficzne dla postgresa(trzeba na docker hub na stronce postgresa naleźć w jakim folderze zapisują się dane (tutaj w /var/lib/posgresql/data)

    //In order to attach the volume to the container we can do:
    //docker run --name postgresDatabase --detach -e POSTGRES_PASSWORD = my_password --volume data_postgres:/var/lib/postgresql/data postgres

    //We can set other environmental variable POSTGRES_USER. The connection will be by localhost. The password is required
    //docker run --name postgresDatabase --detach -e POSTGRES_PASSWORD = my_password -e POSTGRES_USER = eltin --volume data_postgres:/var/lib/postgresql/data -p 5432:5432 postgres
}