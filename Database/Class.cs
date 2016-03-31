using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
namespace StudentManagementSystem
{
    public class Class
    {
        public int ClassID;
        public int UserID;
        public string ClassName;
        public DateTime StartDate;
        public DateTime EndDate;
        public int StudentNumber;


        public static List<Class> GetAllClasses()
        {
            List<Class> c = new List<Class>();
            try
            {
                string query = "SELECT * FROM class";
                MySqlCommand cmd = new MySqlCommand(query, Database1.connection);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Class p = new Class();
                    p.ClassID = Int16.Parse(reader["classID"].ToString());
                    p.UserID = Int16.Parse(reader["userID"].ToString());
                    p.ClassName = reader["className"].ToString();
                    p.StartDate = Convert.ToDateTime(reader["startDate"].ToString());
                    p.EndDate = Convert.ToDateTime(reader["endDate"].ToString());
                    p.StudentNumber = Int16.Parse(reader["studentNumber"].ToString());

                    c.Add(p);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
            return c;
        }

        public static void Insert(Class c){
            // insert into database
            string query = "INSERT INTO class(userID, className, startDate, endDate, studentNumber) VALUES(@userID,@className, @startDate, @endDate, @studentNumber)";
            MySqlCommand cmd = new MySqlCommand(query, Database1.connection);
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@userID", c.UserID);
            cmd.Parameters.AddWithValue("@className", c.ClassName);
            cmd.Parameters.AddWithValue("@startDate", c.StartDate);
            cmd.Parameters.AddWithValue("@endDate", c.EndDate);
            cmd.Parameters.AddWithValue("@studentNumber", c.StudentNumber);
            cmd.ExecuteNonQuery();
        }

        public static void Update(Class c)
        {
            string query = "UPDATE classSET userID=@userID, className= @className, startDate=@startDate, endDate=@endDate, studentNumber=@studentNumber WHERE classID=@classID";
            MySqlCommand cmd = new MySqlCommand(query, Database1.connection);
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@classID", c.ClassID);
            cmd.Parameters.AddWithValue("@userID", c.UserID);
            cmd.Parameters.AddWithValue("@className", c.ClassName);
            cmd.Parameters.AddWithValue("@startDate", c.StartDate);
            cmd.Parameters.AddWithValue("@endDate", c.EndDate);
            cmd.Parameters.AddWithValue("@studentNumber", c.StudentNumber);

            cmd.ExecuteNonQuery();
        }

        public static void Delete(int ID)
        {
            // delete from database
            string query = "DELETE FROM class WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, Database1.connection);
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@id", ID);

            cmd.ExecuteNonQuery();
        }
    }
}
