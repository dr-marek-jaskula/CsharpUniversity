using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningLINQ
{
    public class Data
    {
        public List<Employee> Employees { get; set; }


        public Data()
        {
            Employees = new List<Employee>();
            // Init sample data
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            string[] possibleNames = new string[] { "Dawid", "Mariola", "Maciej", "Zbigniew", "Katarzyna" };
            string[] possibleLastNames = new string[] { "Kowalski", "Nowak", "Pszczyński", "Wolski", "Murek" };

            string[] noteTitles = new string[] { "Meeting", "Cleaning", "Phone call" };
            string[] noteContents = new string[] { "Lorem ipsum dolor sit amet, consectuar..." };

            string[] jobTitles = new string[] { "Create project", "Create database", "Design database" };
            string[] jobDescriptions = new string[] { "Lorem ipsum dolor sit amet, consectuar..." };

            Employees = possibleNames.SelectMany(lastName => possibleLastNames, (x, y) => new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = x,
                LastName = y,

                Notes = noteTitles.SelectMany(content => noteContents, (zx, zy) => new Note
                {
                    Id = Guid.NewGuid(),
                    Title = zx,
                    Content = zy
                }).ToList(),

                Jobs = noteTitles.SelectMany(desc => jobDescriptions, (zx, zy) => new Job
                {
                    Id = Guid.NewGuid(),
                    Title = zx,
                    Description = zy
                }).ToList()

            }).ToList();
        }

        //internal IEnumerable<IGrouping<string, Employee>> GroupEmployees()
        //{
        //    throw new NotImplementedException();
        //}

        //internal IEnumerable<Employee> GetEmployeesByName(string name)
        //{
        //    throw new NotImplementedException();
        //}

        //internal IEnumerable<Job> GetEmployeeJobs(string name)
        //{
        //    throw new NotImplementedException();
        //}

        //internal IEnumerable<Employee> SortDescEmplyees()
        //{
        //    throw new NotImplementedException(); 
        //}

        //internal IEnumerable<Employee> SortAscEmplyees()
        //{
        //    Employees.OrderBy(s => s.FirstName).
        //}
    }
}
