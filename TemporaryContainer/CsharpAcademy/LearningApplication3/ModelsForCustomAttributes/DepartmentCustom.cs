using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationComponent;

namespace LearningApplication3
{
    public class DepartmentCustom
    {
        [RequiredCustom]
        public int Id { get; set; }

        [RequiredCustom]
        [StringLenghtCustom(15, 2)]
        public string DeptShortName { get; set; }
    
        public string DeptLongName { get; set; }
    
    }
}
