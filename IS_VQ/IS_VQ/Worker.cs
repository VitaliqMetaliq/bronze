using System;

namespace IS_VQ
{
    public class Worker //, INotifyPropertyChanged
    {
        #region Поля

        /// <summary>
        /// Порядковый номер работника
        /// </summary>
        private int id;
        /// <summary>
        /// Имя работника
        /// </summary>
        private string name;
        /// <summary>
        /// Фамилия работника
        /// </summary>
        private string surname;
        /// <summary>
        /// Возраст работника
        /// </summary>
        private byte age;

        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Id">Уникальный номер</param>
        /// <param name="Name">Имя</param>
        /// <param name="Surname">Фамилия</param>
        /// <param name="Age">Возраст</param>
        public Worker(int Id, string Name, string Surname, byte Age)
        {
            this.id = Id;  
            this.name = Name;
            this.surname = Surname;
            this.age = Age;
            this.Salary = GetSalary();
            this.CurrentProjects = GetProjects();
        }

        /// <summary>
        /// Конструктор со всеми параметрами для загрузки из файла
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Surname"></param>
        /// <param name="Age"></param>
        /// <param name="Salary"></param>
        /// <param name="CurrentProjects"></param>
        public Worker(int Id, string Name, string Surname, byte Age, int salary, int currentProjects)
        {
            this.id = Id;
            this.name = Name;
            this.surname = Surname;
            this.age = Age;
            this.Salary = salary;
            this.CurrentProjects = currentProjects;
        }

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Worker() : this(1 , "", "", 1)
        {
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Текущее количество проектов работника
        /// </summary>
        public int CurrentProjects { get; set; }

        /// <summary>
        /// Оплата труда
        /// </summary>
        public int Salary { get; set; }

        /// <summary>
        /// Свойство, возвращающее уникальный номер работника
        /// </summary>
        public int Id { get { return this.id; } set { this.id = value; } }

        /// <summary>
        /// Свойство, возвращающее имя работника
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }

        /// <summary>
        /// Свойство, возвращающее фамилию работника
        /// </summary>
        public string Surname { get { return this.surname; } set { this.surname = value; } }

        /// <summary>
        /// Свойство, возвращающее возраст работника
        /// </summary>
        public byte Age { get { return this.age; } set { this.age = value; } }

        //public string Motto() { return $"{name} {surname}"; }

        #endregion

        #region Методы

        //public static int CompareBySalary(Worker worker1, Worker worker2)
        //{
        //    return worker1.Salary.CompareTo(worker2.Salary);
        //}

        //public static int CompareByAge(Worker worker1, Worker worker2)
        //{
        //    return worker1.Age.CompareTo(worker2.Age);
        //}

        /// <summary>
        /// Метод получения случайной зарплаты
        /// </summary>
        /// <returns></returns>
        public int GetSalary()
        {
            Random rnd = RandomProvider.GetThreadRandom();
            int[] sal = { 10000, 20000, 30000, 40000, 50000 };
            return sal[rnd.Next(sal.Length)];
        }

        /// <summary>
        /// Метод получения случайного количества текущих проектов
        /// </summary>
        /// <returns></returns>
        public int GetProjects()
        {
            Random rnd = RandomProvider.GetThreadRandom();
            return rnd.Next(1, 5);
        }

        public override string ToString()
        {
            return $"{name} {surname}";
        }
        #endregion
    }
}
