using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTestApplication
{
    public partial class Form1 : Form
    {
        DbUsage db = new DbUsage();
        public Form1()
        {
            InitializeComponent();
            //Установка источника данных для GridView
            WorkersGridView.DataSource = db.dt;
            db.dt.Columns[0].ReadOnly = true;
            //Источник данных для комбобокс
            depComboBox.DataSource = db.depDt;
            depComboBox.DisplayMember = "Name";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void setInfo(DataRowView row)
        {
            if(row != null)
            {
                nameTextBox.Text = row["Firstname"].ToString();
                surnameTextBox.Text = row["Lastname"].ToString();
                birthDatePicker.Value = (DateTime)row["Birthday"];
                addressTextBox.Text = row["Address"].ToString();
                descTextBox.Text = row["Description"].ToString();
            }
        }

        private bool checkRow(DataRowView row)
        {
            if (row["Firstname"].ToString().Trim(' ') == "") return false;
            if (row["Lastname"].ToString().Trim(' ') == "") return false;
            if (row["Birthday"] == null) return false;
            if (row["Address"].ToString().Trim(' ') == "") return false;
            if (row["Department"].ToString() == "") return false;

            return true;
        }

        private bool checkFields()
        {
            if (nameTextBox.Text.Trim(' ').Length == 0) return false;
            if (surnameTextBox.Text.Trim(' ').Length == 0) return false;
            if (addressTextBox.Text.Trim(' ').Length == 0) return false;
            return true;
        }

        private void WorkersGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            db.row = (DataRowView)WorkersGridView.CurrentRow.DataBoundItem;
            db.row.BeginEdit();
        }

        private void WorkersGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (db.row == null) return;
            if (!checkRow(db.row))
            {
                MessageBox.Show("Одно или несколько полей заполнены неверно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                db.row.CancelEdit();
            }
            else
            {
                db.row.EndEdit();
                db.da.Update(db.dt);
            }
        }

        private void WorkersGridView_SelectionChanged(object sender, EventArgs e)
        {
            db.row = (DataRowView)WorkersGridView.CurrentRow.DataBoundItem;
            setInfo(db.row);
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if(!checkFields())
            {
                MessageBox.Show("Одно или несколько полей заполнены неверно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                db.row["Firstname"] = nameTextBox.Text;
                db.row["Lastname"] = surnameTextBox.Text;
                db.row["Birthday"] = birthDatePicker.Value;
                db.row["Address"] = addressTextBox.Text;
                db.row["Description"] = descTextBox.Text;
                db.row.EndEdit();
                db.da.Update(db.dt);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            DataRow r = db.dt.NewRow();
            addForm add = new addForm(r);
            add.ShowDialog();

            if(add.DialogResult == DialogResult.OK)
            {
                db.dt.Rows.Add(r);
                db.da.Update(db.dt);
            }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            db.row = (DataRowView)WorkersGridView.CurrentRow.DataBoundItem;
            db.row.Row.Delete();
            db.da.Update(db.dt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = depComboBox.SelectedIndex + 1;
            //MessageBox.Show($"{index}", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            sampleForm sample = new sampleForm(index);
            sample.ShowDialog();
        }
    }
}
