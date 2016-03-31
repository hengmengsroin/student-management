using StudentManagementSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementSystem.Forms
{
    public partial class NewClass : Form
    {
        public static bool state= false;
        public static string ClassName;
        public int PosX;
        public int PosY;
        public NewClass(int x, int y)
        {
            InitializeComponent();
            PosX = x;
            PosY = y;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1) m.Result = (IntPtr)0x2;
                    return;
            }
            base.WndProc(ref m);
        }

        private void txtStudentNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtStudentNumber.MaxLength = 15;
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Database1.Close();
            this.Close();
            state = false;
        }

        private void btnNewClass_Click(object sender, EventArgs e)
        {
            //1.verify all input (textbox, combobox, and datetimepicker)
            if (String.IsNullOrEmpty(txtClassName.Text) || (string.IsNullOrEmpty(txtUserID.Text)) || (string.IsNullOrEmpty(txtStudentNumber.Text)))
            {
                MessageBox.Show("Please input class info", "Erorr Adding", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClassName.Focus();
                return;
            }

            //2.insert to table student in the database
            Class s = new Class();
            s.ClassName = txtClassName.Text.Trim();
            s.UserID = Int16.Parse(txtUserID.Text.Trim());
            s.StartDate = dateTimePickerStartDate.Value;
            s.EndDate = dateTimePickerEndDate.Value;
            s.StudentNumber = Int16.Parse(txtStudentNumber.Text.Trim());

            Class.Insert(s);
            state = true;
            this.Close();
            ClassName = s.ClassName;
        }

        private void NewClass_Load(object sender, EventArgs e)
        {
            Database1.Open();
            txtClassName.Focus(); 
        }

    }
}
