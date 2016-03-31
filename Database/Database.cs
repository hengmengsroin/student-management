using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace StudentManagementSystem
{
    public class DataBase
    {
        public static MySqlConnection connection;
        public static void DB()
        {
            string connectionString = "Server=localhost; Port=3306; User=root; Password=root ; Database=studentmanagmentsystem; CharSet= utf08";
            connection = new MySqlConnection(connectionString);
        }
        public static bool Open()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool Close()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
