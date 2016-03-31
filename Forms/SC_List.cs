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
    public partial class SC_List : Form
    {
        public static int ID = 0;
        public SC_List()
        {
            InitializeComponent();
            ttSearch.SetToolTip(btnSearch, "Search");
        }
        
        MySqlDataAdapter adapter;
        DataTable tb = new DataTable();
       
        private void SC_List_Load(object sender, EventArgs e)
        {
            
           //Database
            DataBase.DB();
            //Show Student Score in DataGridView
            string query = "SELECT * FROM studentscore";
            MySqlCommand cmd = new MySqlCommand(query, DataBase.connection);
            ///
            ///
            try
            {
                DataBase.Open();
                adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(tb);
                StudentScoreList.DataSource = tb;
                ColumnDesign();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DataBase.Close();
            ////////////////////////////////////Update Total Score By Percentage That User Limit/////////////////////////////////////////////////
             MySqlCommand cmd1 = new MySqlCommand();
             cmd1.Connection = DataBase.connection;
             cmd1.CommandText = "SELECT Homework,Quiz,Assignment,Midterm,Attendent,Final FROM studentscore Where Status =1";
            /////////////////////////////////////////////////////////////////////////////////////
            MySqlCommand cmdPct = new MySqlCommand();
            cmdPct.Connection = DataBase.connection;
            cmdPct.CommandText = "Select * From percentage";
            ////////////////////////////////////////////////////////////////////////////////////
            MySqlCommand cmdRow = new MySqlCommand();
            cmdRow.Connection = DataBase.connection;
            cmdRow.CommandText = "Select count(*) From studentscore";
            ////////////////////////////////////////////////////////////////////////////////////
            MySqlCommand cmdTotal = new MySqlCommand();
            cmdTotal.Connection = DataBase.connection;
            cmdTotal.CommandText = "Update studentscore set Total=@Total Where ID = @id";
            try
            {
                /////////////////////////////////Start Get Percentage Of Score//////////////////////////////////////
                float HwPct = 0;
                float QuizPct = 0;
                float AssPct = 0;
                float MidtermPct = 0;
                float AttPct = 0;
                float FinalPct = 0;
                DataBase.Open();
                MySqlDataReader readPct = cmdPct.ExecuteReader();
                while (readPct.Read() == true)
                {
                    HwPct = readPct.GetFloat(readPct.GetOrdinal("HomeworkPct"));
                    QuizPct = readPct.GetFloat(readPct.GetOrdinal("QuizPct"));
                    AssPct = readPct.GetFloat(readPct.GetOrdinal("AssignmentPct"));
                    MidtermPct = readPct.GetFloat(readPct.GetOrdinal("MidtermPct"));
                    AttPct = readPct.GetFloat(readPct.GetOrdinal("AttendentPct"));
                    FinalPct = readPct.GetFloat(readPct.GetOrdinal("FinalPct"));
                    //////////////////////////////////////////////////////////////                   
                }
                readPct.Close();
                /////////////////////////////////End of Get Percentage Of Score//////////////////////////////////////
                ////////////////////////////////Count Number of Row//////////////////////////////////////////////////
                int Index = Convert.ToInt32(cmdRow.ExecuteScalar());
                /////////////////////////////finish count////////////////////////////////////////////////////////////
                /////////////////////////////////Start of Get Value Of Score//////////////////////////////////////////
                float Hw = 0;
                float Quiz =0;
                float Ass = 0;
                float Midterm = 0;
                float Att = 0;
                float Final = 0;
                float Total = 0;
                float[] ArrayTotal = new float[Index];
                int i = 0;
                MySqlDataReader read = cmd1.ExecuteReader();
                while (read.Read() == true)
                {
                     Hw = read.GetFloat(read.GetOrdinal("Homework"));
                     Quiz = read.GetFloat(read.GetOrdinal("Quiz"));
                     Ass = read.GetFloat(read.GetOrdinal("Assignment"));
                     Midterm = read.GetFloat(read.GetOrdinal("Midterm"));
                     Att = read.GetFloat(read.GetOrdinal("Attendent"));
                     Final = read.GetFloat(read.GetOrdinal("Final"));
                     Total = (Hw * HwPct) / 100 + (Quiz * QuizPct) / 100 + (Ass * AssPct) / 100 + (Midterm * MidtermPct) / 100 + (Att * AttPct) / 100 + (Final * FinalPct) / 100;
                     ArrayTotal[i] = Total;
                     i++;
                }
                read.Close();
                ///////////////////////////////////End of Get Value Of Score//////////////////////////////////////
                ///////////////////////////////////Start Update All total of Score////////////////////////////////
                for (i=0;i<Index;i++)
                {
                    StudentScoreList.Rows[i].Cells[10].Value = ArrayTotal[i]; 
                }
                //Runing Select in DGV////////////////////////////////////////////////////////////////////////////
                int id = 0;
                for (i = 0; i < Index; i++)
                {
                    id = Convert.ToInt32(StudentScoreList.Rows[i].Cells[0].Value.ToString());
                    cmdTotal.Parameters.AddWithValue("@ID",id);
                    Total = ArrayTotal[i];
                    cmdTotal.Parameters.AddWithValue("Total", Total);
                    cmdTotal.ExecuteNonQuery();
                    cmdTotal.Parameters.Clear();
                }
                ////////////////////////////End of Update Total
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString());}
            DataBase.Close();
        }
        private void StudentScoreList_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            StudentScoreList.RowsDefaultCellStyle.SelectionBackColor = Color.DarkGray;
            StudentScoreList.RowsDefaultCellStyle.SelectionForeColor = Color.Blue;
            try
            {
                ID = Convert.ToInt32(StudentScoreList.Rows[e.RowIndex].Cells[0].Value.ToString());
                ///Send Value of ID to Form SC_Edit to Edit Score////////////////////////////////
                SC_Edit Edit = new SC_Edit();
                Edit.GetID = ID;
                MyStudentScore.GetID = ID;
                
                ///Send Value of ID to Form StudentScore /////////////////////////////////////////
                StudentScore Score = new StudentScore();
                Score.GetID = ID;
                
            }
            catch (Exception) { }
        }
        public void ColumnDesign()
        {
            StudentScoreList.Columns["ID"].Visible = false;
            StudentScoreList.Columns["StudentID"].Width = 170;
            StudentScoreList.Columns["FullName"].Width = 170;
            StudentScoreList.Columns["Gender"].Width = 80;
            StudentScoreList.Columns["Homework"].Width = 110;
            StudentScoreList.Columns["Quiz"].Width = 80;
            StudentScoreList.Columns["Status"].Visible = false;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            StudentScoreList.RowsDefaultCellStyle.SelectionBackColor = Color.DarkGray;
            StudentScoreList.RowsDefaultCellStyle.SelectionForeColor = Color.Blue;
            DataBase.DB();
            string query = "SELECT * FROM studentscore Where FullName like '" + txtSearch.Text + "%' or StudentID like '" + txtSearch.Text + "%' or Total like '" + txtSearch.Text + "%'";
            MySqlCommand cmd = new MySqlCommand(query, DataBase.connection);
            tb.Clear();
            adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(tb);
            StudentScoreList.DataSource = tb;
            ColumnDesign();
        }
        private void btnSearch_MouseHover(object sender, EventArgs e)
        {
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
        }

        private void txtSearch_MouseLeave(object sender, EventArgs e)
        {
            txtSearch.BorderStyle = BorderStyle.Fixed3D;
        
        }
    }
}
