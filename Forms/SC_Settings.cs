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
    public partial class SC_Settings : Form
    {
        public SC_Settings()
        {
            InitializeComponent();
        }
        //
        public static int HomeworkPct;
        public static int QuizPct;
        public static int AssignmentPct;
        public static int MidtermPct;
        public static int AttendentPct;
        public static int FinalPct;

        private void SC_Settings_Load(object sender, EventArgs e)
        {
            DataBase.DB();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DataBase.connection;
            cmd.CommandText = "Select * From percentage";
            try
            {
                DataBase.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read() == true)
                {
                    //
                    HomeworkPct = read.GetInt32(read.GetOrdinal("HomeworkPct"));
                    QuizPct = read.GetInt32(read.GetOrdinal("QuizPct"));
                    AssignmentPct = read.GetInt32(read.GetOrdinal("AssignmentPct"));
                    MidtermPct = read.GetInt32(read.GetOrdinal("MidtermPct"));
                    AttendentPct = read.GetInt32(read.GetOrdinal("AttendentPct"));
                    FinalPct = read.GetInt32(read.GetOrdinal("FinalPct"));
                    //
                    trbHW.Value = HomeworkPct;
                    trbQuiz.Value = QuizPct;
                    trbAss.Value = AssignmentPct;
                    trbMidterm.Value = MidtermPct;
                    trbAtt.Value = AttendentPct;
                    trbFinal.Value = FinalPct;

                    //
                    lbHomework.Text = trbHW.Value.ToString() + " %";
                    lbQuiz.Text = trbQuiz.Value.ToString() + " %";
                    lbAssignment.Text = trbAss.Value.ToString() + " %";
                    lbMidterm.Text = trbMidterm.Value.ToString() + " %";
                    lbAttendent.Text = trbAtt.Value.ToString() + " %";
                    lbFinal.Text = trbFinal.Value.ToString() + " %";
                }
                read.Close();
            }
            catch (Exception) { }
            DataBase.Close();
            trbHW.Enabled = false;
            trbQuiz.Enabled = false;
            trbAss.Enabled = false;
            trbMidterm.Enabled = false;
            trbAtt.Enabled = false;
            trbFinal.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            trbHW.Enabled = true;
            trbQuiz.Enabled = true;
            trbAss.Enabled = true;
            trbMidterm.Enabled = true;
            trbAtt.Enabled = true;
            trbFinal.Enabled = true;
            /////////////////////////////////
            trbHW.BackColor = Color.Peru;
            trbQuiz.BackColor = Color.Peru;
            trbAss.BackColor = Color.Peru;
            trbMidterm.BackColor = Color.Peru;
            trbAtt.BackColor = Color.Peru;
            trbFinal.BackColor = Color.Peru;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataBase.DB();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DataBase.connection;
            cmd.CommandText = "Update percentage Set HomeworkPct=@HomeworkPct,QuizPct=@QuizPct,AssignmentPct=@AssignmentPct,MidtermPct=@MidtermPct,AttendentPct=@AttendentPct,FinalPct=@FinalPct";
            //////////////////////////////////////
            HomeworkPct = trbHW.Value;
            QuizPct = trbQuiz.Value;
            AssignmentPct = trbAss.Value;
            MidtermPct = trbMidterm.Value;
            AttendentPct = trbAtt.Value;
            FinalPct = trbFinal.Value;
            ///////////////////////////////////////
            cmd.Parameters.AddWithValue("HomeworkPct", HomeworkPct);
            cmd.Parameters.AddWithValue("QuizPct", QuizPct);
            cmd.Parameters.AddWithValue("AssignmentPct",AssignmentPct);
            cmd.Parameters.AddWithValue("MidtermPct", MidtermPct);
            cmd.Parameters.AddWithValue("AttendentPct", AttendentPct);
            cmd.Parameters.AddWithValue("FinalPct", FinalPct);
            try
            {
                DataBase.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update Successful", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }  
            catch(Exception){}
            DataBase.Close();
            ///////////////////////////////////////
            trbHW.Enabled = false;
            trbQuiz.Enabled = false;
            trbAss.Enabled = false;
            trbMidterm.Enabled = false;
            trbAtt.Enabled = false;
            trbFinal.Enabled = false;
            ///////////////////////////////////////
            trbHW.BackColor = Color.Silver;
            trbQuiz.BackColor = Color.Silver;
            trbAss.BackColor = Color.Silver;
            trbMidterm.BackColor = Color.Silver;
            trbAtt.BackColor = Color.Silver;
            trbFinal.BackColor = Color.Silver;
        }
        private void trbHW_Scroll(object sender, EventArgs e)
        {
            lbHomework.Text = trbHW.Value.ToString() + " %";
        }
        private void trbQuiz_Scroll(object sender, EventArgs e)
        {
            lbQuiz.Text = trbQuiz.Value.ToString() + " %";
        }

        private void trbAss_Scroll(object sender, EventArgs e)
        {
            lbAssignment.Text = trbAss.Value.ToString() + " %";
        }

        private void trbMidterm_Scroll(object sender, EventArgs e)
        {
            lbMidterm.Text = trbMidterm.Value.ToString() + " %";
        }

        private void trbAtt_Scroll(object sender, EventArgs e)
        {
            lbAttendent.Text = trbAtt.Value.ToString() + " %";
        }

        private void trbFinal_Scroll(object sender, EventArgs e)
        {
            lbFinal.Text = trbFinal.Value.ToString() + " %";
        }
    }
}
