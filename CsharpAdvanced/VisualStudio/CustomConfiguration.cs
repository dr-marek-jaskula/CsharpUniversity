namespace CsharpAdvanced.VisualStudio;

public class CustomConfiguration
{
    //Change base convention for naming the private fields to use prefix underscore (_)
    //Tools -> Options -> Text Editor -> C# -> Code Style -> Naming -> Manage Naming Styles -> Add naming style for Private or Internal Field
    //Naming style: _fieldName
    //Required prefix: _
    //Capitalization: camel Case Name
    //https://ardalis.com/configure-visual-studio-to-name-private-fields-with-underscore/

    /*
    Get into the Visual Studio Installer (for example from Tool -> Get Tools and Features -> Singletons)
    Find "Class Designer"
    It will enable us to visually design the diagrams of DB model relations

    To get class diagram just -> Add New Item -> Class Diagram
    To make bound just right click on certain property of field and "show as association" (for singular) and collection for multiple
    to export diagrams as images just right click and export as image
    to get the class diagram view just type in search
     */

    //FILE_SCOPE NAMESPACES
    //In Visual Studio, for c# 10 apply "file-scoped namespaces"
    //To do this:
    //1. Right click on project
    //2. Add -> NewEditorConfig
    //3. Go to -> Code Styles
    //3. Change "Namespace declarations" to "File scoped"

    //Globally change:
    //Options -> Text Editor -> Basic -> Code Style -> General
    //find "Namespaces declarations" and set it to "refactor only"

    //PINNED TABS
    //Pinned tabs (in search) -> show pinned tabs in a separate row

    //Add folder, add class
    //Tools -> Options -> General -> Keyboard -> Project.AddClass -> Global -> Alt + n
    //Tools -> Options -> General -> Keyboard -> Project.NewFolder -> Global -> Alt + f

    //ToggleComment
    //Tools -> Options -> General -> Keyboard -> Edit.ToggleComment -> Global -> ctrl + /
}