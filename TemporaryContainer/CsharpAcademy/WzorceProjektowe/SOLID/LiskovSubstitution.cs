using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WzorceProjektowe
{
    class LiskovSubstitution
    {
        // you should be able to use any derived class instead of a parent class and have it behave in the same manner without modification"
    }


    //we will make app that gets the SQL file and read it and store the text in variable. So we need to Load the text, and then save the text into SQL file (for example after modification that we need to do)

    #region Bad Example 

    class SqlFile
    {
        public string FilePath { get; set; }
        public string FileText { get; set; }

        public string LoadText()
        {
            Console.WriteLine("read text from sql file");
            return "LOAD TEXT";
        }

        public virtual void SaveText()
        {
            Console.WriteLine("Save text into sql file");
        }
    }

    class ReadOnlySqlFile : SqlFile
    {
        public override void SaveText()
        {
            //u cant save read only file
            throw new InvalidOperationException("Cant Save");
        }
    }

    public class SqlFileManager
    {
        List<SqlFile> listOfSqlFiles { get; set; }

        public string GetTextFromFiles()
        {
            StringBuilder stringBuilder = new();
            foreach (var file in listOfSqlFiles)
                stringBuilder.Append(file.LoadText());
            return stringBuilder.ToString();
        }

        public void SaveTextIntoFiles()
        {
            foreach (var file in listOfSqlFiles)
                if (!(file is ReadOnlySqlFile))
                    file.SaveText();
        }
    }

    #endregion

    #region Good Example

    public interface IReadableSqlFile
    {
        string LoadText();
    }

    public interface IWriteableSqlFile
    {
        void SaveText();
    }

    class SqlFile1 : IWriteableSqlFile, IReadableSqlFile
    {
        public string FilePath { get; set; }
        public string FileText { get; set; }

        public string LoadText()
        {
            Console.WriteLine("read text from sql file");
            return "LOAD TEXT";
        }

        public virtual void SaveText()
        {
            Console.WriteLine("Save text into sql file");
        }
    }

    class ReadOnlySqlFile1 : IReadableSqlFile
    {
        public string FilePath { get; set; }
        public string FileText { get; set; }
        public void SaveText()
        {
            //u cant save read only file
            throw new InvalidOperationException("Cant Save");
        }
        public string LoadText()
        {
            Console.WriteLine("read text from sql file");
            return "LOAD TEXT";
        }
    }

    public class SqlFileManager1
    {
        List<SqlFile> listOfSqlFiles { get; set; }

        public string GetTextFromFiles(List<IReadableSqlFile> readableSqlFiles)
        {
            StringBuilder stringBuilder = new();
            foreach (var file in readableSqlFiles)
                stringBuilder.Append(file.LoadText());
            return stringBuilder.ToString();
        }

        public void SaveTextIntoFiles(List<IWriteableSqlFile> writeableSqlFiles)
        {
            foreach (var file in writeableSqlFiles)
                 file.SaveText();
        }
    }

    #endregion






}
