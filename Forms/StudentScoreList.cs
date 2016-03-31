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
using MySql.Data;
namespace StudentManagementSystem
{
    public partial class StudentScore : Form
    {
        public StudentScore()
        {
            InitializeComponent();

        }
        public static int ID;
        public int GetID
        {
            get { return ID; }
            set { ID = value; }
        }
        private void StudentScore_Load(object sender, EventArgs e)
        {
    
            SC_List List = new SC_List();
            List.TopLevel = false;
            List.AutoScroll = true;
            pnSScore.Controls.Add(List);
            List.Show();
            ChangeLanguage();
        }
        public void btnList_Click(object sender, EventArgs e)
        {
            ID = 0;
            btnDelete.Enabled = true;
            btnEdit.Enabled = true;

            DetectColorButton(1);
            pnSScore.Controls.Clear();
            SC_List List = new SC_List();
            List.TopLevel = false;
            List.AutoScroll = true;
            pnSScore.Controls.Add(List);
            List.Show();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (ID > 0)
            {
                DetectColorButton(3);
                pnSScore.Controls.Clear();
                SC_Edit Edit = new SC_Edit();
                Edit.Owner = this;
                Edit.TopLevel = false;
                Edit.AutoScroll = true;
                pnSScore.Controls.Add(Edit);
                Edit.Show();
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
           }
            else MessageBox.Show("Please Select Student First!!!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;
            pnSScore.Controls.Clear();
            DetectColorButton(4);
            SC_Settings Option = new SC_Settings();
            Option.TopLevel = false;
            Option.AutoScroll = true;
            pnSScore.Controls.Add(Option);
            Option.Show();
        }
        void DetectColorButton(int button)
        {
            if (button == 1)
            {
                btnList.BackColor = Color.WhiteSmoke;
                //
                btnEdit.BackColor = Color.Gray;
                btnSettings.BackColor = Color.Gray;
            }
            else if (button == 3)
            {
                btnEdit.BackColor = Color.WhiteSmoke;
                //
                btnList.BackColor = Color.Gray;
                btnSettings.BackColor = Color.Gray;
            }
            else if(button == 4)
            {
                btnSettings.BackColor = Color.WhiteSmoke;
                //
                btnEdit.BackColor = Color.Gray;
                btnList.BackColor = Color.Gray;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ID > 0)
            {
                string mySql = "Server=localhost; Port=3306; User=root; Password=root ; Database=studentmanagmentsystem";
                MySqlConnection con = new MySqlConnection(mySql);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT FullName FROM studentscore Where ID = @ID";
                cmd.Parameters.AddWithValue("ID", ID);
                String FullName = "";
                try
                {
                    con.Open();
                    MySqlDataReader read = cmd.ExecuteReader();
                    while (read.Read() == true)
                    {
                        FullName = read.GetString(read.GetOrdinal("FullName"));
                    }
                }
                catch (Exception) { }
                con.Close();
                //////////////////////Delete
                var result = MessageBox.Show("Are you sure want to delete student name : " + FullName + " From this Class ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    con.Open();
                    MySqlCommand cmd1 = new MySqlCommand();
                    cmd1.Connection = con;
                    cmd1.CommandText = "Delete FROM studentscore Where ID = @ID";
                    cmd1.Parameters.AddWithValue("ID", ID);
                    cmd1.ExecuteNonQuery();
                    var result1 =  MessageBox.Show("Data Deleted", "Delect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if(result1 == DialogResult.OK)
                    {
                        ///
                        pnSScore.Controls.Clear();
                        SC_List List = new SC_List();
                        List.TopLevel = false;
                        List.AutoScroll = true;
                        pnSScore.Controls.Add(List);
                        List.Show();
                    }
                }
                con.Close();
            }
            else MessageBox.Show("Please Select Student First!!!", "Delete Error!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        public void ChangeLanguage()
        {
            if ((this.Owner as ToolMenu).btnOption.Text == "   ជម្រើស")
            {
                btnList.Font = new Font("Khmer OS Bokor", 13, FontStyle.Regular);
                btnList.Text = "បញ្ជីឈ្មោះសិស្ស";
                btnEdit.Font = new Font("Khmer OS Bokor", 13, FontStyle.Regular);
                btnEdit.Text = "បញ្ចូល/កែពិន្ទុ";
                btnDelete.Font = new Font("Khmer OS Bokor", 13, FontStyle.Regular);
                btnDelete.Text = "លុបសិស្ស";
                btnSettings.Font = new Font("Khmer OS Bokor", 13, FontStyle.Regular);
                btnSettings.Text = "កំណត់";
            }
            else if ((this.Owner as ToolMenu).btnOption.Text == "     Option")
            {
                btnList.Font = new Font("Chaparral Pro", 15, FontStyle.Bold);
                btnList.Text = "List";
                btnEdit.Font = new Font("Chaparral Pro", 15, FontStyle.Bold);
                btnEdit.Text = "Add/Edit";
                btnDelete.Font = new Font("Chaparral Pro", 15, FontStyle.Bold);
                btnDelete.Text = "Delete";
                btnSettings.Font = new Font("Chaparral Pro", 15, FontStyle.Bold);
                btnSettings.Text = "Setting";
            }
        }

    }
}
