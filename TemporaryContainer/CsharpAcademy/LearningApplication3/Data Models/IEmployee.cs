using System;
using System.Collections.Generic;
using System.Text;

namespace LearningApplication3
{
    public interface IEmployee
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        decimal Salary { get; set; }

    }
}
