using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace IS_VQ
{
    public class Department //: INotifyCollectionChanged
    {
        #region Поля
        /// <summary>
        /// Название отдела
        /// </summary>
        private string title;

        /// <summary>
        /// Коллекция работников
        /// </summary>
        private ObservableCollection<Worker> workers = new ObservableCollection<Worker>();

        /// <summary>
        /// Путь к файлу, куда созраняются данные работников
        /// </summary>
        private string path;

        #region For my own

        /// <summary>
        /// Дата последних изменений
        /// </summary>
        //public DateTime LastWriteDate { get; }
        //FileInfo info = new FileInfo(this.path);
        //LastWriteDate = info.LastWriteTime;

        #endregion

        public Worker Director { get; }

        //public event PropertyChangedEventHandler PropertyChanged;

        //public event CollectionChangeEventHandler CollectionChanged;
        #endregion

        #region Конструкторы

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Title">Название отдела</param>
        /// <param name="Path">Путь к файлу</param>
        public Department(string Title, string Path)
        {
            this.path = Path;
            this.title = Title;
            //workers = DeserializeDepartment($"_{this.title}.xml"); //Для чтения из XML

            //Load(); //Для чтения данных из CSV файлов

            LoadFromDb(); //Чтение данных из БД

            //Назначаем директором сотрудника с ИД 1
            foreach (Worker e in workers)
            {
                if (e.Id == 1) this.Director = e;
                break;
            }
            
        }

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Department(): this("", "")
        {
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Свойство, возвращающее количество работников в данном отделе
        /// </summary>
        public int CountWorkers { get { return workers.Count; } }

        /// <summary>
        /// Свойство, возвращающее текущую коллекцию работников
        /// </summary>
        public ObservableCollection<Worker> Workers 
        { 
            get { return this.workers; } 
            set 
            { 
                this.workers = value; 
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Workers))); 
                //CollectionChanged?.BeginInvoke(this, 
            } 
        }

        /// <summary>
        /// Свойство, возвращающее название отдела
        /// </summary>
        public string Title { get { return this.title; } }

        /// <summary>
        /// Свойство, возвращающее путь к файлу
        /// </summary>
        public string Path { get { return this.path; } }

        #endregion

        #region Методы

        /// <summary>
        /// Загрузить информацию из БД
        /// </summary>
        public void LoadFromDb()
        {
            this.Workers = DbUsage.getData(path);
        }

        /// <summary>
        /// Сохранить информацию в БД
        /// </summary>
        public void SaveIntoDb()
        {
            DbUsage.saveData(path, workers);
        }

        /// <summary>
        /// Метод загрузки данных из файла
        /// </summary>
        public void Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('\t');
                    workers.Add(new Worker(Convert.ToInt32(args[0]), args[1], args[2], Convert.ToByte(args[3]), Convert.ToInt32(args[4]), Convert.ToInt32(args[5])));
                }
            }
        }

        /// <summary>
        /// Метод сохранения данных в файл
        /// </summary>
        public void Save()
        {
            File.Delete(this.path);
            using (StreamWriter sw = new StreamWriter(this.path, true, Encoding.Unicode))
            {
                foreach (Worker e in workers)
                {
                    string[] args = { Convert.ToString(e.Id), e.Name, e.Surname, Convert.ToString(e.Age), Convert.ToString(e.Salary), Convert.ToString(e.CurrentProjects) };
                    sw.WriteLine($"{args[0]}\t{args[1]}\t{args[2]}\t{args[3]}\t{args[4]}\t{args[5]}");
                }
            }
        }

        /// <summary>
        /// Метод добавления нового работника
        /// </summary>
        /// <param name="worker">Экземпляр класса Worker</param>
        public void Add(Worker worker)
        {
            workers.Add(worker);
        }

        /// <summary>
        /// Перегрузка строкового представления экземпляра класса Department
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Title;
        }

        /// <summary>
        /// Метод сортировки по оплате труда
        /// </summary>
        public void SortBySalary()
        {
            workers = new ObservableCollection<Worker>(workers.OrderBy(i=>i.Salary));
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Workers)));
        }

        /// <summary>
        /// Метод сортировки по возрасту
        /// </summary>
        public void SortByAge()
        {
            workers = new ObservableCollection<Worker>(workers.OrderBy(i => i.Age));
        }

        /// <summary>
        /// Метод сортировки сначала по зарплате, потом по возрасту
        /// </summary>
        public void SortByBoth()
        {
            workers = new ObservableCollection<Worker>(workers.OrderBy(i => i.Salary).ThenBy(i => i.Age));
        }

        /// <summary>
        /// Метод сортировки по ID
        /// </summary>
        public void SortByID()
        {
            workers = new ObservableCollection<Worker>(workers.OrderBy(i => i.Id));
        }

        public void DeleteWorker(Worker someWorker)
        {
            workers.Remove(someWorker);
        }
        #endregion

        #region Методы сериализации и десериализации

        /// <summary>
        /// Сериализация данных в xml файл
        /// </summary>
        /// <param name="ConcreteWorkersArray"></param>
        /// <param name="path"></param>
        public void SerializeDepartment(ObservableCollection<Worker> ConcreteWorkersArray, string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Worker>));
            Stream fstream = new FileStream(path, FileMode.Create, FileAccess.Write);
            xmlSerializer.Serialize(fstream, ConcreteWorkersArray);
            fstream.Close();
        }

        /// <summary>
        /// Десериализация данных из xml файла, используется в конструкторе
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ObservableCollection<Worker> DeserializeDepartment(string path)
        {
            ObservableCollection<Worker> tempCollection = new ObservableCollection<Worker>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Worker>));
            Stream fstream = new FileStream(path, FileMode.Open, FileAccess.Read);
            tempCollection = xmlSerializer.Deserialize(fstream) as ObservableCollection<Worker>;
            return tempCollection;
        }

        /// <summary>
        /// Сериализация в JSON
        /// </summary>
        /// <param name="ConcreteWorkersArray"></param>
        /// <param name="path"></param>
        public void SerializeJSON(ObservableCollection<Worker> ConcreteWorkersArray, string path)
        {
            string json = JsonConvert.SerializeObject(ConcreteWorkersArray);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Десериализация данных из JSON файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ObservableCollection<Worker> DeserializeJSON(string path)
        {
            ObservableCollection<Worker> tempCollection = new ObservableCollection<Worker>();
            string json = File.ReadAllText(path);
            tempCollection = JsonConvert.DeserializeObject<ObservableCollection<Worker>>(json);
            return tempCollection;
        }

        #endregion
    }
}
