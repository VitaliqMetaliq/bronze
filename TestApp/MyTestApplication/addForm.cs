using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTestApplication
{
    public partial class addForm : Form
    {
        private addForm() { InitializeComponent(); }

        public bool check()
        {
            if (nameTextBox.Text.Trim(' ').Length == 0) return false;
            if (surnameTextBox.Text.Trim(' ').Length == 0) return false;
            if (addressTextBox.Text.Trim(' ').Length == 0) return false;
            if (!int.TryParse(depTextBox.Text, out int n)) return false;
            return true;
        }

        public addForm(DataRow row):this()
        {
            cancelBtn.Click += delegate { this.DialogResult = DialogResult.Cancel; };
            addBtn.Click += delegate
            {
                if(!check())
                {
                    MessageBox.Show("Одно или несколько полей заполнены неверно.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    row["firstname"] = nameTextBox.Text.Trim(' ');
                    row["lastname"] = surnameTextBox.Text.Trim(' ');
                    row["birthday"] = birthDatePicker.Value.ToShortDateString();
                    row["address"] = addressTextBox.Text;
                    row["department"] = depTextBox.Text;
                    row["description"] = descTextBox.Text;
                    this.DialogResult = DialogResult.OK;
                }
            };
        }
    }
}
