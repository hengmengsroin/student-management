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
using StudentManagementSystem;

namespace StudentManagementSystem
{
    public partial class AttendanceForm : Form
    {
        List<StudentListDB> students;
        List<string> attendances;

        public AttendanceForm()
        {
            InitializeComponent();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private void Attendent_Load_1(object sender, EventArgs e)
        {
            Database1.Open();
            DisplayMonth();
            LoadStudent();    
            LoadAttendance();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public void LoadStudent()
        {
            students = Attendance.GetStudent();
            dataGridViewAtd.Rows.Clear();
            string gender;
            foreach (StudentListDB s in students)
            {
                if (s.Gender==true)
                {
                    gender = "M";
                }
                else
                {
                    gender = "F";
                }
                dataGridViewAtd.Rows.Add(s.Id, s.Name, gender);
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public void LoadAttendance() 
        {
            attendances = Attendance.GetAttendance();

            int i = 3;
            foreach (string atd in attendances)
            {
                string[] id = atd.Split(',');
                for (int j = 0; j < id.Count(); j++)
                {
                    for (int k = 0; k < dataGridViewAtd.Rows.Count; k++)
                    {
                        if (id[j] == dataGridViewAtd.Rows[k].Cells[1].Value.ToString())
                        {
                            dataGridViewAtd.Rows[k].Cells[i].Value = true;
                            break;
                        }//end if 
                    }//end for k
                }//end for j
                i = i + 1;      //row day increase
            }//end foreach
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnSave_Click(object sender, EventArgs e)
        {
            string student = "";
            string checkedStd = dataGridViewAtd.SelectedCells[1].Value.ToString();

            string dateTime = DateTime.Now.ToShortDateString();
            string[] Date = dateTime.Split('/');
            
            foreach (DataGridViewRow row in dataGridViewAtd.Rows)
            {
                if ((bool)row.Cells[int.Parse(Date[1])+2].FormattedValue == true)
                {
                    checkedStd = row.Cells[1].Value.ToString();
                    student = student + checkedStd + ",";
                }//end if
            }//end foreach
              
            if (student != "")
            {
                student = student.Remove(student.Length - 1, 1);
                Attendance.Add(dateTime,student);
                MessageBox.Show("Done");
            }//end if 
            else   { MessageBox.Show("Done !!!  There aren't student checked today"); }

            dataGridViewAtd.Rows.Clear();
            LoadStudent();
            LoadAttendance();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public void Month31() { dgvColumn(31); }
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private void Month30() { dgvColumn(30); }
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private void Month29() { dgvColumn(29); }
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private void Month28() { dgvColumn(28); }
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private void DisplayMonth() 
        {
            string dateTime = DateTime.Now.ToShortDateString();

            //split date into part of string by found '/'
            string[] Date = dateTime.Split('/');

            //date0 = month, date1 = day, date2 = year
            this.labelDate.Text = Date[1] + "/" + Date[0] + "/" + Date[2];

            switch (Date[0])
            {
                case "1":
                    labelMonth.Text = "January  " + Date[2];
                    break;
                case "2":
                    labelMonth.Text = "February  " + Date[2];
                    break;
                case "3":
                    labelMonth.Text = "March  " + Date[2];
                    break;
                case "4":
                    labelMonth.Text = "April  " + Date[2];
                    break;
                case "5":
                    labelMonth.Text = "May  " + Date[2];
                    break;
                case "6":
                    labelMonth.Text = "June  " + Date[2];
                    break;
                case "7":
                    labelMonth.Text = "July  " + Date[2];
                    break;
                case "8":
                    labelMonth.Text = "August  " + Date[2];
                    break;
                case "9":
                    labelMonth.Text = "September  " + Date[2];
                    break;
                case "10":
                    labelMonth.Text = "Octorber  " + Date[2];
                    break;
                case "11":
                    labelMonth.Text = "November  " + Date[2];
                    break;
                case "12":
                    labelMonth.Text = "December  " + Date[2];
                    break;
            }//end switch

            if ((int.Parse(Date[2]) - 2000) / 4 == 0 && Date[0] == "2")  { Month29(); }
            else
            {
                if (Date[0] == "4" || Date[0] == "6" || Date[0] == "9" || Date[0] == "11") { Month30(); }
                else if (Date[0] == "2") { Month28(); }
                else { Month31(); }
            }   //end else
        }   // end DisplayMonth
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        
        public void dgvColumn(int day)
        {
            dataGridViewAtd.ColumnCount = 3;
            dataGridViewAtd.Columns[0].Name = "ID";
            dataGridViewAtd.Columns[0].Width = 30;
            dataGridViewAtd.Columns[0].ReadOnly = true;
            dataGridViewAtd.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewAtd.Columns[1].Name = "Student Name";
            dataGridViewAtd.Columns[1].Width = 150;
            dataGridViewAtd.Columns[1].ReadOnly = true;
            dataGridViewAtd.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewAtd.Columns[2].Name = "Gender";
            dataGridViewAtd.Columns[2].Width = 70;
            dataGridViewAtd.Columns[2].ReadOnly = true;
            dataGridViewAtd.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            for (int i = 0; i < day; i++)
            {
                DataGridViewCheckBoxColumn cb1 = new DataGridViewCheckBoxColumn();
                cb1.HeaderText = (i+1).ToString();
                cb1.Width = 30;
                dataGridViewAtd.Columns.Add(cb1);
            }   //end for loop i
        }   //end dgvColumn   
    }   //end class AttedanceForm
}   //end StudentMangementSystem