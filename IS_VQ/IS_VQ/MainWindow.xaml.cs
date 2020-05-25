using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;


namespace IS_VQ
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Коллекция департаментов
        /// </summary>
        ObservableCollection<Department> db = new ObservableCollection<Department>();

        public MainWindow()
        {
            InitializeComponent();
            BoxOfDeps.ItemsSource = db;

            #region Загрузка из БД

            db.Add(new Department("Отдел кадров", "Workers"));
            db.Add(new Department("Работа с клиентами", "Customers"));
            db.Add(new Department("Бухгалтерия", "Bookkeeping"));
            db.Add(new Department("Логистика", "Logistics"));

            #endregion

            #region Загрузка из CSV
            /*db.Add(new Department("Отдел кадров", "HumanResourse.csv"));
            db.Add(new Department("Работа с клиентами", "CustomerService.csv"));
            db.Add(new Department("Бухгалтерия", "Bookkeeping.csv"));
            db.Add(new Department("Логистика", "Logistics.csv"));*/
            #endregion

        }

        #region Обработка событий

        /// <summary>
        /// Обработка вкладок ComboBox'а
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChosenDep(object sender, SelectionChangedEventArgs e)
        {
            Department department = BoxOfDeps.SelectedValue as Department;
            ListView.ItemsSource = department.Workers;
            //InfoListBox.ItemsSource = (BoxOfDeps.SelectedValue as Department).Workers;
            if(department.Workers.Count != 0)
            {
                DirectorBlock.Text = department.Director.ToString();
                //LastWriteBlock.Text = department.LastWriteDate.ToShortDateString();
                CountWorkersBlock.Text = department.CountWorkers.ToString();
            }
            
        }

        /// <summary>
        /// Обработчик события нажатия кнопки добавления нового работника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            Department chosenDepartment = BoxOfDeps.SelectedValue as Department;
            if (BoxOfDeps.SelectedValue == null)
            {
                MessageBox.Show("Сначала выберите отдел","Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                int CurrentCount = chosenDepartment.CountWorkers;//(BoxOfDeps.SelectedValue as Department).CountWorkers;
                //chosenDepartment.Add(new Worker(CurrentCount + 1, "", nameBox.Text, surnameBox.Text, Convert.ToByte(ageBox.Text)));
                bool check = Byte.TryParse(ageBox.Text, out byte result);
                if (!check) MessageBox.Show("Wrong format data", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                else chosenDepartment.Add(new Worker(CurrentCount + 1, nameBox.Text, surnameBox.Text, Convert.ToByte(ageBox.Text)));
            }
        }

        /// <summary>
        /// Обработчик события когда TextBox поля Name выделен
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            nameBox.Clear();
        }

        /// <summary>
        /// Обработчик события когда TextBox поля surname выделен
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void surnameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            surnameBox.Clear();
        }

        /// <summary>
        /// Обработчик события когда TextBox поля Age выделен
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ageBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ageBox.Clear();
        }

        /// <summary>
        /// Обработчик события кнопки сохранения данных в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Department chosenDepartment = BoxOfDeps.SelectedValue as Department;
            if (BoxOfDeps.SelectedValue == null)
            {
                MessageBox.Show("Сначала выберите отдел", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                chosenDepartment.SaveIntoDb();
                MessageBox.Show("Сохранено в БД", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            #region Сохранение в форматы XML, json, csv
            /*Department chosenDepartment = BoxOfDeps.SelectedValue as Department;
            if (BoxOfDeps.SelectedValue == null)
            {
                MessageBox.Show("Сначала выберите отдел", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                chosenDepartment.Save();
                chosenDepartment.SerializeDepartment(chosenDepartment.Workers, $"_{chosenDepartment.Title}.xml");
                chosenDepartment.SerializeJSON(chosenDepartment.Workers, $"__{chosenDepartment.Title}.json");
                MessageBox.Show($"Сохранно в файлы: \n{chosenDepartment.Path} \n_{chosenDepartment.Title}.xml \n__{chosenDepartment.Title}.json",
                    "Сохранено",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }*/
            #endregion
        }

        /// <summary>
        /// Обработчик события кнопки сортировки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            Department department = BoxOfDeps.SelectedValue as Department;
            if (BoxOfDeps.SelectedValue != null)
            {
                if ((bool)salary_check.IsChecked && (bool)age_check.IsChecked)
                {
                    department.SortByBoth();
                }
                else if ((bool)salary_check.IsChecked && (bool)!age_check.IsChecked)
                {
                    department.SortBySalary();
                    
                }
                else if ((bool)!salary_check.IsChecked && (bool)age_check.IsChecked) 
                {
                    department.SortByAge();
                    
                }
                else
                {
                    //MessageBox.Show("Выберите хотябы 1 критерий сортировки", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    department.SortByID();
                    
                }
                ListView.ItemsSource = department.Workers;
            } 
            else MessageBox.Show("Сначала выберите отдел", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }


        /// <summary>
        /// Обработчик события кнопки сортировки по ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void Sort_ID_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (BoxOfDeps.SelectedValue == null)
        //    {
        //        MessageBox.Show("Сначала выберите отдел", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //    else
        //    {
        //        (BoxOfDeps.SelectedValue as Department).SortByID();
        //    }
        //}

        #endregion

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Worker someWorker = ListView.SelectedItem as Worker;
            if (someWorker != null)
            //{
            //    MottoBlock.Text = "";
            //    AgeTextBlock.Text = "";
            //    PersonalIdTextBlock.Text = "";
            //}
            //else
            {
                MottoBlock.Text = $"{someWorker.Name} {someWorker.Surname}";
                AgeTextBlock.Text = $"Возраст: {someWorker.Age}";
                PersonalIdTextBlock.Text = $"Персональный ID: {someWorker.Id}";
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Worker someWorker = ListView.SelectedItem as Worker;
            if(someWorker!=null)
            {
                (BoxOfDeps.SelectedValue as Department).DeleteWorker(someWorker);
                ListView.ItemsSource = (BoxOfDeps.SelectedValue as Department).Workers;
            }
            else
            {
                MessageBox.Show("Работник не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
