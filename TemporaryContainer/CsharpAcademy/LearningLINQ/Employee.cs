using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningLINQ
{
    public class Employee
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public List<Note> Notes { get; set; }

        public List<Job> Jobs { get; set; }

        public string FullName { get { return String.Format("{0} {1}", FirstName, LastName); } }
    }
}
