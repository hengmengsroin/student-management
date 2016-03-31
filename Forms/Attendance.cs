using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace StudentManagementSystem
{
    public class Attendance
    {
        public static List<StudentListDB> GetStudent()
        {
            List<StudentListDB> students = new List<StudentListDB>();

            try
            {
                string query = "SELECT * FROM studentlist";
                MySqlCommand cmd = new MySqlCommand(query, Database1.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    StudentListDB s = new StudentListDB();
                    s.Id = Int16.Parse(reader["studentID"].ToString());
                    s.Name = reader["name"].ToString();
                    s.Gender = Convert.ToBoolean(reader["gender"].ToString());
                    students.Add(s);
                }
                reader.Close();
            }
            catch(Exception ex){ MessageBox.Show(ex.ToString()); }
            return students;

        }        
        //----------------------------------------------------------------------------------------------------------------

        public static void Add (string dateTime, string name)
        {
            try
            {
                string sql = "insert into attendancedb(Date, StudentID) values(@Date, @StudentID)";
                MySqlCommand cmd = new MySqlCommand(sql, Database1.connection);

                cmd.Parameters.AddWithValue("@Date", dateTime);
                cmd.Parameters.AddWithValue("@StudentID", name);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        //-------------------------------------------------------------------------------------------------------------

        public static List<string> GetAttendance()
        {
            List<string> atds = new List<string>();
            
            try
            {
                string query = "SELECT * FROM attendancedb";
                MySqlCommand cmd = new MySqlCommand(query, Database1.connection);

                //compare database with datagridview
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader.GetString(2);
                    atds.Add(name);
                }
                reader.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            return atds;
        }//end GetAttendance
    }//end class Attendance
}//end StudentMangementSystem
