using StudentManagementSystem.Forms;
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
    public partial class Home : Form
    {
        public int NameClass=0;
        public Button MyButton= new Button();
        public Home()
        {
            InitializeComponent();
            LoadButton();
        }

        public List<Class> classes = new List<Class>();

        public void LoadButton()
        {
            classes = Class.GetAllClasses();
            int X=70;
            int Y=215;
            foreach (Class c in classes)
            {
                if (X>900)
                {
                    X = 70;
                    Y = Y + 90;
                }
                CreateDynamicButton(c.ClassName, X,Y);
                X += 165;
            }
            if (X > 900)
            {
                X = 70;
                Y = Y + 90;
            }
            btnNewClass.Location = new Point(X, Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = btnNewClass.Location.X;
            int y = btnNewClass.Location.Y;

            NewClass c = new NewClass(x,y);
            c.ShowDialog();
            if (NewClass.state==true)
            {
                CreateDynamicButton(NewClass.ClassName, x, y);
                if (x>=895)
                {
                    x = -165+70-100+3;
                    MessageBox.Show(x.ToString());
                    y = y + 90;
                }
                btnNewClass.Location = new Point(x+165, y);
            }
        }

        public void CreateDynamicButton(string n, int x, int y)
        {
            // Create a Button object
            Button dynamicButton = new Button();
            NameClass++;
            // Set Button properties
            dynamicButton.Height = 73;
            dynamicButton.Width = 146;
            dynamicButton.BackColor = Color.Pink;
            dynamicButton.ForeColor = Color.White;
            dynamicButton.Location = new Point(x, y);
            dynamicButton.Text = n;
            dynamicButton.Name = NameClass.ToString();
            dynamicButton.Font = new Font("Georgia", 16);
            MyButton = dynamicButton;
            // Add a Button Click Event handler
            dynamicButton.Click += new EventHandler(DynamicButton_Click);

            // Add Button to the Form. Placement of the Button
            // will be based on the Location and Size of button
            Controls.Add(dynamicButton);
            btnNewClass.Location = new Point(x + 156, y);
        }

        private void DynamicButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Show Student List");
        }

        private void Home_Load(object sender, EventArgs e)
        {
            Database1.Open();
            pictureBox1.ImageLocation = Login.PhotoPath;
            lbID.Text = Login.ID.ToString() ;
            lbName.Text = Login.UserName.ToString() ;
        }

        private void Home_Leave(object sender, EventArgs e)
        {
            Database1.Close();
        }

    }
}
