using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTestApplication
{
    /// <summary>
    /// Класс для взаимодействия с БД
    /// </summary>
    public class DbUsage
    {
        public SqlConnection con;
        public SqlDataAdapter da;
        public DataTable dt;
        public DataRowView row;
        public DataTable depDt;

        public DbUsage()
        {
            //Формируем строку подключения
            var connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "TestWorkersDB",
                IntegratedSecurity = true,
                Pooling = true
            };
            con = new SqlConnection(connectionStringBuilder.ConnectionString);
            dt = new DataTable();
            da = new SqlDataAdapter();
            depDt = new DataTable();
            Preparing();
        }

        private void Preparing()
        {
            #region Update 

            var sql = @"UPDATE Workers SET 
                        Firstname = @name, Lastname = @surname, 
                        Birthday = @birth, Address = @address, 
                        Department = @dep, Description = @desc
                      WHERE id = @id";
            da.UpdateCommand = new SqlCommand(sql, con);

            da.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id").SourceVersion = DataRowVersion.Original;
            da.UpdateCommand.Parameters.Add("@name", SqlDbType.NVarChar, 20, "Firstname");
            da.UpdateCommand.Parameters.Add("@surname", SqlDbType.NVarChar, 20, "Lastname");
            da.UpdateCommand.Parameters.Add("@birth", SqlDbType.Date, 20, "Birthday");
            da.UpdateCommand.Parameters.Add("@address", SqlDbType.NVarChar, 150, "Address");
            da.UpdateCommand.Parameters.Add("@dep", SqlDbType.Int, 4, "Department");
            da.UpdateCommand.Parameters.Add("@desc", SqlDbType.NVarChar, 250, "Description");
            #endregion

            #region Select

            sql = @"SELECT * FROM Workers Order By Workers.Id";
            da.SelectCommand = new SqlCommand(sql, con);

            #endregion

            #region Insert

            sql = @"INSERT INTO Workers (Firstname,  Lastname,  Birthday, Address, Department, Description) 
                                 VALUES (@name, @surname, @birth, @address, @dep, @desc); 
                     SET @id = @@IDENTITY;";
            da.InsertCommand = new SqlCommand(sql, con);

            da.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id").Direction = ParameterDirection.Output;
            da.InsertCommand.Parameters.Add("@name", SqlDbType.NVarChar, 20, "Firstname");
            da.InsertCommand.Parameters.Add("@surname", SqlDbType.NVarChar, 20, "Lastname");
            da.InsertCommand.Parameters.Add("@birth", SqlDbType.Date, 20, "Birthday");
            da.InsertCommand.Parameters.Add("@address", SqlDbType.NVarChar, 150, "Address");
            da.InsertCommand.Parameters.Add("@dep", SqlDbType.Int, 4, "Department");
            da.InsertCommand.Parameters.Add("@desc", SqlDbType.NVarChar, 250, "Description");

            #endregion

            #region Delete

            sql = "DELETE FROM Workers WHERE id = @id";

            da.DeleteCommand = new SqlCommand(sql, con);
            da.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id");

            #endregion

            da.Fill(dt);

            //Заполняем DataTable для ComboBox
            da.SelectCommand = new SqlCommand("Select * from Departments", con);
            da.Fill(depDt);
        }
    }
}

