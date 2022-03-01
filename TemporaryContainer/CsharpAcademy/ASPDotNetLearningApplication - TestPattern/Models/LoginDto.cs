using System;
using System.Collections.Generic;

namespace ASPDotNetLearningApplication
{
    public class LoginDto
    {
        //mo¿naby tu dodaæ atrybuty walidacji, albo wykorzystaæ osobny validator (ale dla uproszczenia zostawiamy tak jak jest)
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
