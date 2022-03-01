using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationComponent;

namespace LearningApplication3
{
    //regular expressions wzięte ze strony:
    //https://regexlib.com/Search.aspx?k=UK+post+code&c=-1&m=-1&ps=20

    public class EmployeeCustom
    {
        [RequiredCustom]
        public int Id { get; set; }
        
        [RequiredCustom]
        [StringLenghtCustom(15,"Field, {0}, cannot exceed {1} characer in lenght and cannot be less then {2} characters in lenght",2)]
        public string FirstName { get; set; }

        [RequiredCustom]
        [StringLenghtCustom(15, "Field, {0}, cannot exceed {1} characer in lenght and cannot be less then {2} characters in lenght", 2)]
        public string LastName { get; set; }

        [RequiredCustom]
        [StringLenghtCustom(15, "Field, {0}, cannot exceed {1} characer in lenght and cannot be less then {2} characters in lenght", 2)]
        [RegularExpressionCustom(@"(\s*\(?0\d{4}\)?\s*\d{6}\s*)|(\s*\(?0\d{3}\)?\s*\d{3}\s*\d{4}\s*)")] //z małpą zeby c# zle nie interpretował backslashy
        public string PhoneNumber { get; set; }

        [RequiredCustom]
        [StringLenghtCustom(15, "Field, {0}, cannot exceed {1} characer in lenght and cannot be less then {2} characters in lenght", 2)]
        [RegularExpressionCustom(@"^ *(([\.\-\+\w]{2,}[a-z0-9])@([\.\-\w]+[a-z0-9])\.([a-z]{2,3})) *(; *(([\.\-\+\w]{2,}[a-z0-9])@([\.\-\w]+[a-z0-9])\.([a-z]{2,3})) *)* *$")]
        public string EmailAdress { get; set; }

        [RequiredCustom]
        [StringLenghtCustom(15, "Field, {0}, cannot exceed {1} characer in lenght and cannot be less then {2} characters in lenght", 2)]
        [RegularExpressionCustom(@"^ ?(([BEGLMNSWbeglmnsw][0-9][0-9]?)|(([A-PR-UWYZa-pr-uwyz][A-HK-Ya-hk-y][0-9][0-9]?)|(([ENWenw][0-9][A-HJKSTUWa-hjkstuw])|([ENWenw][A-HK-Ya-hk-y][0-9][ABEHMNPRVWXYabehmnprvwxy])))) ?[0-9][ABD-HJLNP-UW-Zabd-hjlnp-uw-z]{2}$")]
        public string PostCode { get; set; }

    }

}
