using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SomeVQCompany
{
    class TeamLead : Worker
    {
        public bool isDirector;
        public ObservableCollection<Worker> Team { get; set; }

        public TeamLead(int id, string firstname, string lastname, byte age)
           : base (Id:id, FirstName:firstname, LastName:lastname, Age: age)
        {
            Team = new ObservableCollection<Worker>();
            isDirector = false;
            Status = "TeamLead";
            GetSalary();
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Status}";
        }

        /// <summary>
        /// Метод пересчета ЗП у лидера команды
        /// </summary>
        /// <returns></returns>
        public void GetSalary()
        {
            int temp = 0;
            foreach (var e in Team)
            {
                    temp += e.Salary;
            }
            if (temp * 0.15 < 1300) this.Salary = 1300;
            else this.Salary = (int)(temp * 0.15);
        }

        /// <summary>
        /// Метод получения суммы всех зарплат в данной команде
        /// </summary>
        /// <returns></returns>
        public int GetTeamSalary()
        {
            int temp = 0; 
            foreach(var e in Team)
            {
                temp+= e.Salary;
            }
            return temp;
        }
    }
}
