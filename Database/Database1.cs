using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace StudentManagementSystem
{
    class Database1
    {
        public static MySqlConnection connection;
        static Database1()
        {
            string connectionString = "server=127.0.0.1; port= 3306; Database= studentmanagementdb; user= root;password= root; CharSet= utf08";
            connection = new MySqlConnection(connectionString);
        }
        public static bool Open()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
