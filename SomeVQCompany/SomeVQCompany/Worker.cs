using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeVQCompany
{
    class Worker : INotifyPropertyChanged
    {
        #region Поля

        private string firstName;
        private string lastName;
        private byte age;

        public event PropertyChangedEventHandler PropertyChanged;

        protected int salary;
        protected int id;
        protected string status;

        #endregion

        #region Конструкторы

        public Worker(int Id, string FirstName, string LastName, byte Age)
        {
            this.id = Id;
            this.firstName = FirstName;
            this.lastName = LastName;
            this.age = Age;
        }

        #endregion

        #region Свойства

        public int Salary 
        {
            get { return this.salary; } 
            set 
            { 
                this.salary = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Salary)));
            } 
        }
        public int Id { get { return this.id; } set { this.id = value; } }
        public string Status { get { return this.status; } set { this.status = value; } }
        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }
        public string LastName { get { return this.lastName; } set { this.lastName = value; } }
        public byte Age { get { return this.age; } set { this.age = value; } }

        #endregion

        #region Методы

        public override string ToString()
        {
            return $"{firstName} {lastName} {salary}";
        }

        //public virtual int GetSalary()
        //{
        //    return 0;
        //}
        #endregion
    }
}
