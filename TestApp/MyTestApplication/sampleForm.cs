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
    public partial class sampleForm : Form
    {
        private sampleForm() { InitializeComponent(); }

        public sampleForm(int index):this()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "TestWorkersDB"
            };

            SqlConnection con = new SqlConnection(connectionStringBuilder.ConnectionString);
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter();

            var sql = $@"SELECT 
                    Workers.id as 'ID',
                    Workers.Firstname as 'Имя',
                    Workers.Lastname  as 'Фамилия',
                    Workers.Birthday  as 'Дата рождения',
                    Workers.Address as 'Адрес',
                    Departments.Name as 'Название отдела'
                    FROM  Workers, Departments
                    WHERE Workers.Department = {index} and Departments.Id = {index}
                    Order By Workers.Id";
            da.SelectCommand = new SqlCommand(sql, con);
            dt.Clear();
            da.Fill(dt);

            sampleGridView.DataSource = dt.DefaultView;
        }
    }
}
