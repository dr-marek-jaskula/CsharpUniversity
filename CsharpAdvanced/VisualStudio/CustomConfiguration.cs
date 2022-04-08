using static System.Net.Mime.MediaTypeNames;

namespace CsharpAdvanced.VisualStudio;

public class CustomConfiguration
{
    //Change base convention for naming the private fields to use prefix underscore (_)
    //https://ardalis.com/configure-visual-studio-to-name-private-fields-with-underscore/

    /*
    Get into the Visual Studio Installer (for example from Tool -> Get Tools and Features -> Singeltons (Pojedyncze))
    Find "Class Designer" (Projektant Klas)
    It will anable us to visually design the diagarms of DB model relations

    To get class diagram just -> Add New Item -> Class Diagram
    To make bound just rigth click on certain property of field and "show as association" (for sigular) and collection for multiple
    to export diagrams as images just rigth lick and export as image
    to get the class diagram view just type in in search
     */

    //In Visual Studio, for c# 10 apply "file-scoped namespaces"
    //To do this:
    //1. Right click on project
    //2. Add -> NewEditorConfig
    //3. Go to -> Code Styles
    //3. Change "Namespace declarations" to "File scoped"

    //Globally change:
    //Options -> Text Editor -> Basic -> Code Style -> General
    //find "Namespaces declarations" and set it to "refactor only"


    //Pinned tabs ->	show pinned tabs in a separate row
}

