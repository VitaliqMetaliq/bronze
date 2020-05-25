using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeVQCompany
{
    class Middle : Worker
    {
        private Random rnd;
        public Middle(int id, string firstname, string lastname, byte age)
           : base(Id: id, FirstName: firstname, LastName: lastname, Age: age)
        {
            rnd = new Random();
            Salary = 12*(rnd.Next(80,120));
            Status = "Middle";
        }
        //public override int GetSalary()
        //{
        //    return base.GetSalary();
        //}
    }
}
