namespace EFCore.Data_models;

public class DataModelReadMe
{
	//1. Prefer to make nullable properties
	//2. Make nullable references and nullable id references (like ProductId)
	//3. Make all references virtual
	//4. Make all multiple references (lists) set to default value new()
	//5. Configure the classes

	//name table with PascalCase but for tables that are used to demonstrate the many-to-many relationship use underscored x-times pascal case: Salary_Transfer

	//Best example is a class "Employee"

	//To obtain many to many relationship, we should add class like Salary_Transfer. We can omit it but then the ef core will create an auto-generated table for that purpose.

	//In order to illustrate the relations, we can create a ModelDiagram. For that purpose at first we should install an extension by Visual Studio Installer (class diagram).
	//Next we add a ModelDiagram.cd element and fill it will our classes.

	//Performance:
	//Prefer to make SMALLINT or TINYINT if possible
	//Prefer to use VARCHAR if possible with minimal possible number of bytes
	//Try to build rows (column definition) that would use a divider or page size (8kb, 8096bytes, because rest is Page Header)
	//Have such amount of data that one extent (8 pages = 64kb) would be retrieved or its multiplication (so do not get data that is like 1/3 of extent if possible)
}
