using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeVQCompany
{
    class Junior : Worker
    {
        public Junior(int id, string firstname, string lastname, byte age)
           : base(Id: id, FirstName: firstname, LastName: lastname, Age: age)
        {
            Salary = 500;
            Status = "Junior";
        }
    }
}
