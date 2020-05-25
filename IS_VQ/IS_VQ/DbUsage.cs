using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace IS_VQ
{
    static class DbUsage
    {
        private static SqlConnectionStringBuilder str;
        public static SqlDataAdapter da;

        static DbUsage()
        {
            //Формируем строку подключения
            str = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "TestDb",
                IntegratedSecurity = true,
                Pooling = true
            };
            da = new SqlDataAdapter();
        }

        /// <summary>
        /// Метод получения данных из Таблицы БД
        /// </summary>
        /// <param name="dbName">Название таблицы</param>
        /// <returns></returns>
        public static ObservableCollection<Worker> getData(string dbName)
        {
            string sql = $"SELECT * FROM {dbName}";
            ObservableCollection<Worker> temp = new ObservableCollection<Worker>();

            using(SqlConnection con = new SqlConnection(str.ConnectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sql, con);
                SqlDataReader reader = command.ExecuteReader(); 

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        temp.Add(new Worker(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            (byte)(reader.GetInt32(3)), reader.GetInt32(4), reader.GetInt32(5)));
                    }
                }
                reader.Close();
            }
            return temp;
        }

        /// <summary>
        /// Метод сохранения данных в Таблицу
        /// </summary>
        /// <param name="dbName">Имя таблицы</param>
        /// <param name="workers">Коллекция объектов для записи в таблицу</param>
        public static void saveData(string dbName, ObservableCollection<Worker> workers)
        {
            string sql = $@"SET IDENTITY_INSERT {dbName} ON
                            INSERT INTO {dbName} (id, Name, Surname, Age, Salary, Projects)
                            VALUES (@id, @name, @surname, @age, @salary, @projects);
                            SET IDENTITY_INSERT {dbName} OFF";
           
            using (SqlConnection con = new SqlConnection(str.ConnectionString))
            {
                con.Open();
                da.DeleteCommand = new SqlCommand($"TRUNCATE TABLE {dbName}", con);
                da.DeleteCommand.ExecuteNonQuery();
                da.InsertCommand = new SqlCommand(sql, con);
                foreach(Worker w in workers)
                {
                    da.InsertCommand.Parameters.Add(new SqlParameter("@id", w.Id));
                    da.InsertCommand.Parameters.Add(new SqlParameter("@name", w.Name));
                    da.InsertCommand.Parameters.Add(new SqlParameter("@surname", w.Surname));
                    da.InsertCommand.Parameters.Add(new SqlParameter("@age", (int)w.Age));
                    da.InsertCommand.Parameters.Add(new SqlParameter("@salary", w.Salary));
                    da.InsertCommand.Parameters.Add(new SqlParameter("@projects", w.CurrentProjects));
                    da.InsertCommand.ExecuteNonQuery();
                    da.InsertCommand.Parameters.Clear();
                }
                
            }
        }
    }
}
