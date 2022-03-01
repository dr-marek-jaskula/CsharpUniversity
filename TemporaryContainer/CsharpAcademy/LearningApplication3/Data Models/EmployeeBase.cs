using System;
using System.Collections.Generic;
using System.Text;

namespace LearningApplication3
{
    public abstract class EmployeeBase : IEmployee
    {
        public int ID { get; set ; }
        public string FirstName { get; set ; }
        public string LastName { get; set; }
        public virtual decimal Salary { get; set; }
    }
}
