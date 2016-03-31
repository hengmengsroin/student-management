using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
namespace StudentManagementSystem
{
            
    public class StudentListDB
    {

        public int Id;
        public int ClassID;
        public string Name;
        public bool Gender;
        public DateTime DOB;
        public string POB;
        public string MotherName;
        public string FatherName;
        public string Address;
        public int Phone;
        public string PhotoPath;
        public Image Photo;
        public static int ID = 0;
        public static int GetID
        {
            get { return ID; }
            set { ID = value; }
        }
        public static List<StudentListDB> GetAllStudnts()
        {
            List<StudentListDB> student = new List<StudentListDB>();
            try
            {
                string query = "SELECT * FROM studentlist";
                MySqlCommand cmd = new MySqlCommand(query, Database1.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    StudentListDB p = new StudentListDB();
                    p.Id = Int16.Parse(reader["studentID"].ToString());
                    p.Name = reader["name"].ToString();
                    p.Gender = Convert.ToBoolean(reader["gender"].ToString());
                    p.DOB = DateTime.Parse(reader["DOB"].ToString());
                    p.POB = reader["POB"].ToString();
                    p.MotherName = reader["motherName"].ToString();
                    p.FatherName = reader["fatherName"].ToString();
                    p.Address = reader["address"].ToString();
                    p.Phone = Int32.Parse(reader["phone"].ToString());
                    p.PhotoPath = reader["photoPath"].ToString();
                    student.Add(p);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
            
            return student;
        }

   
        
        public static void Insert(StudentListDB student)
        {
            int studentID=0 ;
            
            // insert into database
            string query1 = "INSERT INTO student(name, classID) VALUES(@name,@classID)";
            MySqlCommand cmd1 = new MySqlCommand(query1, Database1.connection);
            cmd1.Prepare();
            cmd1.Parameters.AddWithValue("@name", student.Name);
            cmd1.Parameters.AddWithValue("@classID", student.ClassID);
            cmd1.ExecuteNonQuery();

            //...................
            string query2 = "SELECT * FROM student where name= @name";
            MySqlCommand cmd2 = new MySqlCommand(query2, Database1.connection);
            cmd2.Prepare();
            cmd2.Parameters.AddWithValue("@name", student.Name);
            MySqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                studentID = Int16.Parse(reader["studentID"].ToString());
            }
            reader.Close();
            //.....................

            string query = "INSERT INTO studentlist(studentID, name, gender, DOB, POB, motherName, fatherName, address, phone, photo, photoPath ) VALUES(@studentID,@name,@gender,@DOB, @POB, @motherName, @fatherName, @address, @phone, @photo, @photoPath)";
            MySqlCommand cmd = new MySqlCommand(query, Database1.connection);
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@studentID", studentID);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@gender", student.Gender);
            cmd.Parameters.AddWithValue("@DOB", student.DOB);
            cmd.Parameters.AddWithValue("@POB", student.POB);
            cmd.Parameters.AddWithValue("@motherName", student.MotherName);
            cmd.Parameters.AddWithValue("@fatherName", student.FatherName);
            cmd.Parameters.AddWithValue("@address", student.Address);
            cmd.Parameters.AddWithValue("@phone", student.Phone);
            cmd.Parameters.AddWithValue("photoPath", student.PhotoPath);
            cmd.Parameters.AddWithValue("photo", student.Photo);
            cmd.ExecuteNonQuery();

        }
        public static void Update(StudentListDB student)
        {
            string query = "UPDATE studentlist SET name=@name, gender= @gender, DOB= @DOB, POB =@POB, motherName= @motherName, fatherName= @fatherName, address= @address, phone=@phone, photo=@photo, photoPath=@photoPath WHERE studentID=@id";
            MySqlCommand cmd = new MySqlCommand(query, Database1.connection);
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@id", student.Id);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@gender", student.Gender);
            cmd.Parameters.AddWithValue("@DOB", student.DOB);
            cmd.Parameters.AddWithValue("@POB", student.POB);
            cmd.Parameters.AddWithValue("@motherName", student.MotherName);
            cmd.Parameters.AddWithValue("@fatherName", student.FatherName);
            cmd.Parameters.AddWithValue("@address", student.Address);
            cmd.Parameters.AddWithValue("@phone", student.Phone);
            cmd.Parameters.AddWithValue("@photo", student.Photo);
            cmd.Parameters.AddWithValue("@photoPath", student.PhotoPath);
            try
            {
            cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static void Delete(int id)
        {
            // delete from database
            string query = "DELETE FROM studentlist WHERE studentID = @id";
            MySqlCommand cmd = new MySqlCommand(query, Database1.connection);
            cmd.Prepare();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

        }
    }
}
