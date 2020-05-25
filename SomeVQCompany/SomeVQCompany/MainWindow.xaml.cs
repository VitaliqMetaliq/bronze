using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

#region ToDo
//Добавить статические члены
//Использовать индексаторы
//Использовать вложенные классы для сортировки
//Использовать некоторые стандартные интерфейсы
#endregion
namespace SomeVQCompany
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Worker> unallocatedWorkers = new ObservableCollection<Worker>();
        Department VQcompany = new Department("SomeVQCompany", new TeamLead(1, "Vitaliq", "Metaliq", 25));
        Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            VQcompany.Departments = VQcompany.DeserializeJson("SomeVQCompany.json");
            DepartmentsTreeView.ItemsSource = VQcompany.Departments;
            unallocatedWorkers = JsonConvert.DeserializeObject<ObservableCollection<Worker>>(File.ReadAllText("unallocatedWorkers.json"));
            //DepartmentsComboBox.ItemsSource = VQcompany.Departments;
            //allocationTreeView.ItemsSource = VQcompany.Departments;
            unallocatedWorkersListView.ItemsSource = unallocatedWorkers;
            this.Closed += MainWindow_Closed;
            
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Обработка события выбора сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Worker someWorker = WorkersListView.SelectedItem as Worker;
            if (someWorker != null)
            {
                nameBox.Text = someWorker.FirstName;
                surnameBox.Text = someWorker.LastName;
                ageBox.Text = Convert.ToString(someWorker.Age);
            }
        }

        /// <summary>
        /// Обработка события выбора департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentsTreeView_Selected(object sender, RoutedEventArgs e)
        {
            Department someDepartment = DepartmentsTreeView.SelectedItem as Department;
            WorkersListView.ItemsSource = someDepartment.chief.Team;
            Binding binding = new Binding { Source = someDepartment.chief, Path = new PropertyPath("Salary") };
            ChiefInfoBox.Text = $"{someDepartment.chief.FirstName} {someDepartment.chief.LastName}, {someDepartment.chief.Status}.  Зарплата";
            ChiefSalaryBox.SetBinding(TextBlock.TextProperty, binding);
            Dollar.Text = "$";
        }
        /// <summary>
        /// Обработка нажатия кнопки добавления сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Department someDepartment = DepartmentsTreeView.SelectedItem as Department;
            AddWindow addWindow = new AddWindow();
            addWindow.Owner = this;
            if(addWindow.ShowDialog()==true)
            {
                if(someDepartment==null || addWindow.assignCheckBox.IsChecked==true)
                {
                    if (addWindow.JuniorCheck == true) unallocatedWorkers.Add(new Junior(rnd.Next(100, 10000), addWindow.InputName, addWindow.InputSurname, addWindow.InputAge));
                    else if (addWindow.MiddleCheck == true) unallocatedWorkers.Add(new Middle(rnd.Next(100, 10000), addWindow.InputName, addWindow.InputSurname, addWindow.InputAge));
                    else unallocatedWorkers.Add(new TeamLead(rnd.Next(100, 10000), addWindow.InputName, addWindow.InputSurname, addWindow.InputAge));
                    MessageBox.Show("Добавлено в базу\nнераспределенных работников", "Успешно добавлено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (addWindow.JuniorCheck == true) someDepartment.AddWorker(new Junior(rnd.Next(100, 10000), addWindow.InputName, addWindow.InputSurname, addWindow.InputAge));
                    else if (addWindow.MiddleCheck == true) someDepartment.AddWorker(new Middle(rnd.Next(100, 10000), addWindow.InputName, addWindow.InputSurname, addWindow.InputAge));
                    else someDepartment.AddWorker(new TeamLead(rnd.Next(100, 10000), addWindow.InputName, addWindow.InputSurname, addWindow.InputAge));
                    MessageBox.Show($"Добавлено в {someDepartment.Name}", "Успешно добавлено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }

        /// <summary>
        /// Обработка нажатия кнопки созхранения информации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonConvert.SerializeObject(VQcompany.Departments);
            File.WriteAllText("SomeVQCompany.json", json);
            json = JsonConvert.SerializeObject(unallocatedWorkers);
            File.WriteAllText("unallocatedWorkers.json", json);
        }

        /// <summary>
        /// Обработка нажатия кнопки удаления сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Worker someWorker = WorkersListView.SelectedItem as Worker;
            Department someDepartment = DepartmentsTreeView.SelectedItem as Department;
            if (someWorker == null) MessageBox.Show("Выберите работника", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else someDepartment.DeleteWorker(someWorker);
        }

        /// <summary>
        /// Обработка нажатия кнопки изменения названия департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameDepButton_Click(object sender, RoutedEventArgs e)
        {
            Department department = DepartmentsTreeView.SelectedItem as Department;
            if (department != null && DepartmentNameTextBox.Text.Length!=0)
            {
                department.Name = DepartmentNameTextBox.Text;
            }
            else MessageBox.Show("Выберите департамент и введите\nкорректное имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            DepartmentsTreeView.Items.Refresh();
        }

        /// <summary>
        /// Обработка события нажатия кнопки добавления департамента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            Department department = DepartmentsTreeView.SelectedItem as Department;
            Worker worker = unallocatedWorkersListView.SelectedItem as Worker;
            if (worker == null || worker.GetType() != typeof(TeamLead)) MessageBox.Show("Выберите TeamLead'a из списка\n" +
                   "нераспределенных работников, чтобы назначить\n" +
                   "его на должность руководителя нового департамента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (department == null)
            {
                VQcompany.Departments.Add(new Department(DepartmentNameTextBox.Text, worker as TeamLead));
                unallocatedWorkers.Remove(worker);
            }
            else
            {
                department.AddDepartment(new Department(DepartmentNameTextBox.Text, worker as TeamLead));
                unallocatedWorkers.Remove(worker);
            }

        }

        private void DeleteDepartmentButton_Click(object sender, RoutedEventArgs e)
        {
            //var selected = DepartmentsTreeView.SelectedItem as TreeViewItem;
            //var department = selected.Parent as Department;
            
            try
            {
                string name = (DepartmentsTreeView.SelectedItem as Department).Name;
                VQcompany.FindAndDelete(name);
                DepartmentsTreeView.Items.Refresh();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //VQcompany.FindAndDelete(name);
            //DepartmentsTreeView.Items.Refresh();
        }

        //private void AddRootDepButton_Click(object sender, RoutedEventArgs e)
        //{
        //    VQcompany.Departments.Add(new Department(DepartmentNameTextBox.Text, TeamleadListView.SelectedItem as TeamLead));
        //}


        //private void RaiseButton_Click(object sender, RoutedEventArgs e)
        //{
        //    Department someDepartment = DepartmentsTreeView.SelectedItem as Department;
        //    someDepartment.RaiseToDirector();
        //}

        /// <summary>
        /// Обработка нажатия кнопки изменения информации о сотруднике
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renameWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            Worker someWorker = WorkersListView.SelectedItem as Worker;
            if(someWorker!=null)
            {
                bool check = Byte.TryParse(ageBox.Text, out byte result);
                if (!check) MessageBox.Show("Неверный формат поля Возраст", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    someWorker.FirstName = nameBox.Text;
                    someWorker.LastName = surnameBox.Text;
                    someWorker.Age = result;
                }
                //someWorker.Age = 
            }
            WorkersListView.Items.Refresh();
        }

        /// <summary>
        /// Обработка нажатия кнопки распределения сотрудника 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allocateButton_Click(object sender, RoutedEventArgs e)
        {
            Worker worker = unallocatedWorkersListView.SelectedItem as Worker;
            Department department = DepartmentsTreeView.SelectedItem as Department;
            if (worker == null || department == null) MessageBox.Show("Выберите работника из списка нераспределенных\n" +
                     "работников, а также департамент, в который его\n" +
                     "необходимо определить", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                department.AddWorker(worker);
                unallocatedWorkers.Remove(worker);
            }
        }

        private void DepartmentsTreeView_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(DepartmentsTreeView?.SelectedItem?.GetType().ToString());
        }
    }
}
