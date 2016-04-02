using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
namespace StudentManagementSystem
{
    public class User
    {
        public int Id;
        public string Name;
        public bool Gender;
        public string Phone;
        public string Username;
        public string Password;
        public string Role;
        public string Path;
        public Image Photo;

        public static User LoginWithUsername (string username)
        {
            User user = null;
            try
            {
                string query = "SELECT * FROM user where username = @username";
                MySqlCommand cmd = new MySqlCommand(query, Database1.connection);

                cmd.Prepare(); // prepare the cmd
                cmd.Parameters.AddWithValue("@username", username);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user = new User();
                    user.Id = Int16.Parse(reader["id"].ToString());
                    user.Username = reader["username"].ToString();
                    user.Password = reader["password"].ToString();
                    user.Path = reader["path"].ToString();
                    user.Name = reader["name"].ToString();
                    //byte[] photoByte = (byte[])reader["photo"];
                    //MemoryStream ms = new MemoryStream(photoByte);
                    //user.Photo = Image.FromStream(ms);
                    //user.PhotoText = reader["photo"].ToString();
                    //user.Role = reader["role"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
            
            return user;
        }

        public static User LoginWithPassword(string password)
        {
            User user = null;
            string query = "SELECT * FROM user where  password= @password";
            MySqlCommand cmd = new MySqlCommand(query, Database1.connection);

            cmd.Prepare(); // prepare the cmd
            cmd.Parameters.AddWithValue("@password", password);

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user = new User();
                user.Id = Int16.Parse(reader["id"].ToString());
                user.Password = reader["password"].ToString();
                user.Username = reader["username"].ToString();
                //user.Role = reader["role"].ToString();
            }
            reader.Close();

            return user;
        }
        public static void SignUp(User u)
        {
            try
            {
                string query = "INSERT INTO user(username, password, name, gender, phone,photo, path) VALUES(@username,@password,@name, @gender,@phone,@photo,@path)";
                MySqlCommand cmd = new MySqlCommand(query, Database1.connection);
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@username", u.Username);
                cmd.Parameters.AddWithValue("@password", u.Password);
                cmd.Parameters.AddWithValue("@name", u.Name);
                cmd.Parameters.AddWithValue("@gender", u.Gender);
                cmd.Parameters.AddWithValue("@phone", u.Phone);
                cmd.Parameters.AddWithValue("@photo", u.Photo);
                cmd.Parameters.AddWithValue("path", u.Path);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            // sign up success
            
        }
    }
}
