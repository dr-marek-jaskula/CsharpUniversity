namespace EFCore.Data_models;

public class DataModelReadMe
{
	//1. Prefer to make nullable properties
	//2. Make nullable references and nullable id references (like ProductId)
	//3. Make all references virtual
	//4. Make all multiple references (lists) set to default value new()
	//5. Configure the classes

	//Best example is a class "Employee"

	//To obtain many to many relationship, we should add class like Salary_Transfer. We can omit it but then the ef core will create an auto-generated table for that purpose.

	//In order to illustrate the relations, we can create a ModelDiagram. For that purpose at first we should install an extension by Visual Studio Installer (class diagram).
	//Next we add a ModelDiagram.cd element and fill it will our classes.
}
