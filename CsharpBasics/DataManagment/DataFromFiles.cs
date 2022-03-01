namespace CsharpBasics.DataManagement;

public class DataFromFiles
{
    public static void InvokeDataFromFilesExamples()
    {
        //We can define the absolute path to the file. '@' can be use to avoid double slashes in the string
        string absolutePath = @"D:\CsharpRepozitories\CsharpUniversity\CsharpBasics\DataManagment\Example.txt";

        //We can use the relative path, so path starting from the position of exec file
        //string relativePath = @"DataManagment\Example.txt";

        if (!File.Exists(absolutePath))
            File.Create(absolutePath).Close(); //create and close the file

        //Method that stores the write to file methodology.
        WriteToFile(absolutePath, "Some random text.\n Even more");

        string? textFromFile = ReadFromFile(absolutePath);
    }

    public static void WriteToFile(string path, string inputText)
    {
        //We define a StreamWriter object, that will be used to write a text to a file (using is to call display when code goes out of the scope
        using StreamWriter sw = new(path, true);

        //writes a line into a file by a stream writer
        sw.WriteLine(inputText);
    }

    public static string? ReadFromFile(string path)
    {
        using StreamReader sr = File.OpenText(path);
        string riddenText = string.Empty;
        return sr.ReadToEnd();
    }
}
