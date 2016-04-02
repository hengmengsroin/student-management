using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StudentManagementSystem
{
    public partial class SignUp1 : Form
    {
        public SignUp1()
        {
            InitializeComponent();
        }
        public static Image img;
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

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtPhone.MaxLength = 15;
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
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            //1.verify all input (textbox, combobox, and datetimepicker)
            if (String.IsNullOrEmpty(txtFullName.Text) || (string.IsNullOrEmpty(txtUsername.Text)) || (string.IsNullOrEmpty(txtPassword.Text)) || (string.IsNullOrEmpty(txtPhone.Text)))
            {
                MessageBox.Show("Please input student info", "Erorr Adding", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFullName.Focus();
                return;
            }

            //2.insert to table student in the database
            User s = new User();
            s.Name = txtFullName.Text.Trim();
            if (rdbFemale.Checked == true)
            {
                s.Gender = true;
            }
            else
            {
                s.Gender = false;
            }
            s.Username = txtUsername.Text.Trim();
            s.Password = txtPassword.Text.Trim();
            s.Phone = (txtPhone.Text.Trim());
            s.Path = Picture.ImageLocation;
            s.Photo = img;
            User.SignUp(s);
            this.Close();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            Database1.Open();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddPhoto_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                string fileName = openFileDialog1.SafeFileName;
                string extension = System.IO.Path.GetExtension(fileName);
                string newFileName = AppDomain.CurrentDomain.BaseDirectory + " \\newFile." + extension;
                //MessageBox.Show(filePath);
                Picture.ImageLocation = filePath;
                img = Image.FromFile(filePath);

            }
        }
    }
}
