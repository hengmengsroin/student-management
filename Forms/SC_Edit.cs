using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace StudentManagementSystem
{
    public partial class SC_Edit : Form
    {
        public SC_Edit()
        {
            InitializeComponent();
        }

        //Get ID from DataGridView Student Score(SC)
        public static int ID;
       // public static int Back = 0;
        public int GetID
        {
            get { return ID; }
            set { ID = value;}
        }
        private void SC_List_Load(object sender, EventArgs e)
        {
            DataBase.DB();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DataBase.connection;
            cmd.CommandText = "SELECT FullName,Homework,Quiz,Assignment,Midterm,Attendent,Final,Total FROM studentscore Where ID = @ID";
            cmd.Parameters.AddWithValue("ID", ID);
            try
            {
                DataBase.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read() == true)
                {
                    lbName.Text = read.GetString(read.GetOrdinal("FullName"));
                    txtHomework.Text = read.GetFloat(read.GetOrdinal("Homework")).ToString();
                    txtQuiz.Text = read.GetFloat(read.GetOrdinal("Quiz")).ToString();
                    txtAssignment.Text = read.GetFloat(read.GetOrdinal("Assignment")).ToString();
                    txtMidterm.Text = read.GetFloat(read.GetOrdinal("Midterm")).ToString();
                    txtAttendent.Text = read.GetFloat(read.GetOrdinal("Attendent")).ToString();
                    txtFinal.Text = read.GetFloat(read.GetOrdinal("Final")).ToString();
                }
                read.Close();
            }
            catch (Exception) { }
            DataBase.Close();
        }
        public void btnCencel_Click(object sender, EventArgs e)
        {
            (this.Owner as StudentScore).btnEdit.BackColor = Color.Gray;
            (this.Owner as StudentScore).btnList.BackColor = Color.WhiteSmoke;
            (this.Owner as StudentScore).btnEdit.Enabled = true;
            (this.Owner as StudentScore).btnDelete.Enabled = true;
            this.Controls.Clear();
            SC_List List = new SC_List();
            List.TopLevel = false;
            List.AutoScroll = true;
            this.Controls.Add(List);
            List.Show();
            ID = 0;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataBase.DB();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DataBase.connection;
            string MySpl = "UPDATE studentscore SET Homework=@Homework,Quiz=@Quiz,Assignment=@Assignment,Midterm=@Midterm,Attendent=@Attendent,Final=@Final WHERE ID = @ID";
            cmd.CommandText = MySpl;
            cmd.Parameters.AddWithValue("ID", ID);
            try
            {
                float Homework = float.Parse(txtHomework.Text);
                float Quiz = float.Parse(txtQuiz.Text);
                float Assignment = float.Parse(txtAssignment.Text);
                float Midterm = float.Parse(txtMidterm.Text);
                float Attendent = float.Parse(txtAttendent.Text);
                float Final = float.Parse(txtFinal.Text);


                //Update start
                cmd.Parameters.AddWithValue("Homework", Homework);
                cmd.Parameters.AddWithValue("Quiz", Quiz);
                cmd.Parameters.AddWithValue("Assignment", Assignment);
                cmd.Parameters.AddWithValue("Midterm", Midterm);
                cmd.Parameters.AddWithValue("Attendent", Attendent);
                cmd.Parameters.AddWithValue("Final", Final);
            }
            catch (Exception){ }
            try
            {
                DataBase.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Successful", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //////////////
                (this.Owner as StudentScore).btnEdit.BackColor = Color.Gray;
                (this.Owner as StudentScore).btnList.BackColor = Color.WhiteSmoke;
                (this.Owner as StudentScore).btnEdit.Enabled = true;
                (this.Owner as StudentScore).btnDelete.Enabled = true;
                ID = 0;
                this.Controls.Clear();
                SC_List List = new SC_List();
                List.TopLevel = false;
                List.AutoScroll = true;
                this.Controls.Add(List);
                List.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Fill all Information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataBase.Close();
        }

        private void SC_Edit_KeyPress(object sender, KeyPressEventArgs e)
        {

            ///
            txtHomework.MaxLength = 3;
            txtQuiz.MaxLength = 3;
            txtAssignment.MaxLength = 3;
            txtMidterm.MaxLength = 3;
            txtAttendent.MaxLength = 3;
            txtFinal.MaxLength = 3;
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
