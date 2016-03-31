using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace StudentManagementSystem
{
    public partial class Login : Form
    {
        string password;
        public Login()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Database1.Open();
            hidest(2);
            ttBack.SetToolTip(btnBack, "Back");
            ttShow.SetToolTip(btnShowPassword, "Show Password");
            ttShow.SetToolTip(btnExit, "Exit");

        }

        private void lbUsernameInTextbox_Click(object sender, EventArgs e)
        {
            lbUsernameInTextbox.Visible = false;
            this.ActiveControl = txtUsername;
        }

        private void txtUsername_Click(object sender, EventArgs e)
        {
            lbUsernameInTextbox.Visible = false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            
            txtPassword.Clear();
            //1. verify the username text and password
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                // show message
                lbUsername.Text = "Please enter username";
                //MessageBox.Show("Please enter username", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //2. comfirm to database
            User user = new User();
            user = User.LoginWithUsername(txtUsername.Text);
            if (user == null) // user = null mean the username or password is invalid
            {
                // show message
                lbUsername.Text = "Username is invalid";
                //MessageBox.Show("Username is invalid", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
            }
            else
            {
                // 3. load form student list
                lbUsername.Text = txtUsername.Text;
                password = user.Password;
                hidest(1);
                pictureCircle1.ImageLocation = user.Path;
            }

        }
        public void hidest(int user)
        {
            
            if (user == 1)
            {
                txtPassword.Focus();
                ////Hide User//////////
                txtUsername.Visible = false;
                btnNext.Visible = false;
                lbUsernameInTextbox.Visible = false;
                ///Show Password/////////
                txtPassword.Visible = true;
                lbPassword.Visible = true;
                btnLogin.Visible = true;
                btnBack.Visible = true;
                btnShowPassword.Visible = true;
            }
            else if (user == 2)
            {
                /////hide Password////////
                txtPassword.Visible = false;
                lbPassword.Visible = false;
                btnLogin.Visible = false;
                btnBack.Visible = false;
                btnShowPassword.Visible = false;
                //Show User/////
                txtUsername.Visible = true;
                btnNext.Visible = true;
                lbUsernameInTextbox.Visible = true;
                txtUsername.Clear();
            }
        }

        private void lbPassword_Click(object sender, EventArgs e)
        {
            lbPassword.Visible = false;
            this.ActiveControl = txtPassword;
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            lbPassword.Visible = false;
        }

        private void lbUsername_SizeChanged(object sender, EventArgs e)
        {
            lbUsername.Left = (this.ClientSize.Width - lbUsername.Size.Width) / 2;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            lbUsername.Text = "";
            hidest(2);
        }
        private void btnShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                // show message
                MessageBox.Show("Password is invalid", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //2. comfirm to database
            User user = new User();
            //user = User.LoginWithPassword(txtPassword.Text);
            if (password!= txtPassword.Text) // user = null mean the username or password is invalid
            {
                // show message
                lbUsername.Text = "Username is invalid";
                lbUsername.Text = txtUsername.Text;
                MessageBox.Show("Password is invalid", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
            }
            else
            {
                // 3. load form student list
                ToolMenu Menu = new ToolMenu();
                Menu.Show();
                this.Hide();
                Database1.Close();
            }
            
            
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignUp1 SU = new SignUp1();
            SU.ShowDialog();
        }

        private void Login_Leave(object sender, EventArgs e)
        {
            Database1.Close();
        }
    }
}
