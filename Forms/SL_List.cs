using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using StudentManagementSystem.Forms;

namespace StudentManagementSystem
{
    public partial class SL_List : Form
    {
        static List<StudentListDB> Mystudent;
        public static int rowIndex=-1;

        
        public SL_List()
        {
            InitializeComponent();
        }
        public static int ID;

        public void LoadStudent()
        {
            Mystudent = StudentListDB.GetAllStudnts();
            string gender;
            dataGridViewScore.Rows.Clear();
            foreach (StudentListDB s in Mystudent)
	        {
                if (s.Gender==true)
                {
                    gender = "M";
                }
                else
                {
                    gender = "F";
                }
                dataGridViewScore.Rows.Add(s.Id, s.Name, gender, s.DOB.ToShortDateString(), s.POB, s.FatherName, s.MotherName, s.Address, s.Phone);
	        }
           
        }

        private void SL_List_Load(object sender, EventArgs e)
        {
            Database1.Open();
            LoadStudent();
        }

        private void SL_List_Leave(object sender, EventArgs e)
        {
            Database1.Close();
        }
        
        private void dataGridViewScore_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewScore.RowsDefaultCellStyle.SelectionBackColor = Color.Red;
            dataGridViewScore.RowsDefaultCellStyle.SelectionForeColor = Color.Blue;
            rowIndex = Int16.Parse(dataGridViewScore.SelectedCells[0].Value.ToString());
        }
        //private StudentListDB student = new StudentListDB();
        public StudentListDB GetSelected()
        {

            if (rowIndex<0)
            {
                return null;
               // (this.Owner as StudentList)
            }
            else
            {
                 int id=-1;
                foreach (StudentListDB s in Mystudent)
                {
                    if (s.Id==rowIndex)
                    {
                        id= Int16.Parse (Mystudent.IndexOf(s).ToString());
                    }
                }
                StudentListDB ss = new StudentListDB();
                try
                {
                ss= Mystudent.ElementAt(id);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                   
                }
                return ss;
            }
        }

        private void SL_List_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
