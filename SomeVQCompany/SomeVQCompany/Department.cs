using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace SomeVQCompany
{
    class Department
    {
        public TeamLead chief;

        #region Конструкторы

        public Department()
        {

        }

        public Department(string name,TeamLead leader)
        {
            Name = name;
            this.chief = leader;
            Departments = new ObservableCollection<Department>();
            //chief.ToString();
        }

        #endregion

        #region Свойства

        public ObservableCollection<Department> Departments { get; set; }

        public string Name { get; set; }

        public TeamLead Chief { get; set; }

        #endregion

        #region Методы

        public void AddDepartment(Department InputDepartment)
        {
            Departments.Add(InputDepartment);
        }

        /// <summary>
        /// Метод поиска и удаления департамента по имени
        /// </summary>
        /// <param name="name"></param>
        public void FindAndDelete(string name)
        {
            //bool isFinded = false;
            if (Departments.Count != 0)
            {
                foreach (Department e in Departments)
                {
                    if (e.Name == name)
                    {
                        DelDepartment(this[name]);
                        break;
                    }
                    else e.FindAndDelete(name);
                }
            }
        }

        public void DelDepartment(Department InputDepartment)
        {
            Departments.Remove(InputDepartment);
        }

        //public string DepartmentInfo()
        //{
        //    return $"Количество сотрудников {chief.Team.Count}";
        //}

        public ObservableCollection<Department> DeserializeJson(string path)
        {
            ObservableCollection<Department> temp = new ObservableCollection<Department>();
            string json = File.ReadAllText(path);
            temp = JsonConvert.DeserializeObject<ObservableCollection<Department>>(json);
            return temp;
        }

        /// <summary>
        /// Метод повышения до директора, в котором идет пересчет З\П в соответствии с ТЗ
        /// </summary>
        public void RaiseToDirector()
        {
            chief.isDirector = true;
            chief.Salary = 0;
            chief.Salary = GetDirectorSalary();
        }

        /// <summary>
        /// Метод пересчета ЗП для директора
        /// </summary>
        /// <returns></returns>
        public int GetDirectorSalary()
        {
            int temp = 0;
            if (Departments.Count != 0)
            {
                foreach (var e in Departments)
                {
                    if (e.Departments.Count == 0)
                    {
                        temp += e.chief.Salary + e.chief.GetTeamSalary();
                    }
                    else temp += e.GetDirectorSalary();
                }
            }
            temp += chief.Salary + chief.GetTeamSalary();
            return temp;
        }

        public void AddWorker(Worker worker)
        {
            chief.Team.Add(worker);
            if (!chief.isDirector) chief.GetSalary(); 
            else
            {
                chief.Salary = 0;
                chief.Salary = GetDirectorSalary();
            }
        }

        public void DeleteWorker(Worker worker)
        {
            chief.Team.Remove(worker);
            if (!chief.isDirector) chief.GetSalary();
            else
            {
                chief.Salary = 0;
                chief.Salary = GetDirectorSalary();
            }
        }

        #endregion

        /// <summary>
        /// Индексатор возвращающий объект типа Department с указанным именем
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Department this[string name]
        {
            get
            {
                Department some = null;
                foreach(Department dep in this.Departments)
                {
                    if(dep.Name == name)
                    {
                        some = dep;
                        break;
                    }
                }
                return some;
            }
        }
    }
}
