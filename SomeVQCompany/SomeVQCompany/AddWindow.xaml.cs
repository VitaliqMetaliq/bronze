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
using System.Windows.Shapes;

namespace SomeVQCompany
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public string InputName { get { return inputNameBox.Text; } }
        public string InputSurname { get { return inputSurnameBox.Text; } }
        public byte InputAge
        {
            get
            {
                bool check = Byte.TryParse(inputAgeBox.Text, out byte correctAge);
                if (check) return correctAge;
                else return 0;
            }
        }
        public bool JuniorCheck { get { return (bool)junCheck.IsChecked; } }
        public bool MiddleCheck { get { return (bool)midCheck.IsChecked; } }
        public bool TeamLeadCheck { get { return (bool)tleadCheck.IsChecked; } }
        //public string PositionState { get; set; }

        public AddWindow()
        {
            InitializeComponent();
            this.Closed += AddWindow_Closed;
        }

        private void AddWindow_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputAge == 0)
            {
                MessageBox.Show("Некорректный формат поля Возраст", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!JuniorCheck && !MiddleCheck && !TeamLeadCheck)
            { 
                MessageBox.Show("Выберите занимаемую должность", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else this.DialogResult = true;
        }

        #region GotFocus
        private void inputNameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            inputNameBox.Clear();
        }

        private void inputSurnameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            inputSurnameBox.Clear();
        }

        private void inputAgeBox_GotFocus(object sender, RoutedEventArgs e)
        {
            inputAgeBox.Clear();
        }
        #endregion
    }
}
