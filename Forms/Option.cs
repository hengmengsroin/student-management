using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class Option : Form
    {
        public Option()
        {
            InitializeComponent();
        }
        private void Option_Load(object sender, EventArgs e)
        {
        }
        private void pbKhmerLanguage_Click(object sender, EventArgs e)
        {

            //////////////////////////////////////////Form Menu//////////////////////////////////////////////////
            (this.Owner as ToolMenu).btnHome.Font = new Font("Khmer OS Content", 13, FontStyle.Regular);
            (this.Owner as ToolMenu).btnHome.Text = "   ទំព័រដើម";
            (this.Owner as ToolMenu).btnStudentList.Font = new Font("Khmer OS Content", 13, FontStyle.Regular);
            (this.Owner as ToolMenu).btnStudentList.Text = "   បញ្ជីឈ្មោះសិស្ស";
            (this.Owner as ToolMenu).btnStudentScore.Font = new Font("Khmer OS Content", 13, FontStyle.Regular);
            (this.Owner as ToolMenu).btnStudentScore.Text = "   បញ្ជីពិន្ទុសិស្ស";
            (this.Owner as ToolMenu).btnStudentAttendent.Font = new Font("Khmer OS Content", 13, FontStyle.Regular);
            (this.Owner as ToolMenu).btnStudentAttendent.Text = "   បញ្ជីអវត្តមានសិស្ស";
            (this.Owner as ToolMenu).btnStudentPayment.Font = new Font("Khmer OS Content", 13, FontStyle.Regular);
            (this.Owner as ToolMenu).btnStudentPayment.Text = "   បញ្ជីបង់ប្រាក់";
            (this.Owner as ToolMenu).btnReport.Font = new Font("Khmer OS Content", 13, FontStyle.Regular);
            (this.Owner as ToolMenu).btnReport.Text = "   របាយការណ៍";
            (this.Owner as ToolMenu).btnOption.Font = new Font("Khmer OS Content", 13, FontStyle.Regular);
            (this.Owner as ToolMenu).btnOption.Text = "   ជម្រើស";
            (this.Owner as ToolMenu).btnClose.Font = new Font("Khmer OS Content", 13, FontStyle.Regular);
            (this.Owner as ToolMenu).btnClose.Text = "   ចាក់ចេញ";

            (this.Owner as ToolMenu).lbMainName.Font = new Font("Khmer OS Bokor", 21, FontStyle.Regular);
            (this.Owner as ToolMenu).lbMainName.Text = "   ជម្រើស";
            (this.Owner as ToolMenu).Body = "តើអ្នកចង់ចាក់ចេញពីកម្មវិធីនេះឫ ? ";
            (this.Owner as ToolMenu).Title = "ចាក់ចេញ​ ?";
           
            ////////////////////////////////////////////////Form Student Score/////////////////////////////////////////////
            //(this.Owner as StudentScore).btnList.Font =  new Font("Khmer OS Content",16, FontStyle.Regular);
            //(this.Owner as StudentScore).btnList.Text = "បញ្ជីឈ្មោះ";
        }

        private void pbEnglishLanguage_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////Form Menu//////////////////////////////////////////////////
            (this.Owner as ToolMenu).btnHome.Font = new Font("Chaparral Pro", 15, FontStyle.Regular);
            (this.Owner as ToolMenu).btnHome.Text = "     Home";
            (this.Owner as ToolMenu).btnStudentList.Font = new Font("Chaparral Pro", 15, FontStyle.Regular);
            (this.Owner as ToolMenu).btnStudentList.Text = "     Students List";
            (this.Owner as ToolMenu).btnStudentScore.Font = new Font("Chaparral Pro", 15, FontStyle.Regular);
            (this.Owner as ToolMenu).btnStudentScore.Text = "     Students Score";
            (this.Owner as ToolMenu).btnStudentAttendent.Font = new Font("Chaparral Pro", 15, FontStyle.Regular);
            (this.Owner as ToolMenu).btnStudentAttendent.Text = "     Students Attendent";
            (this.Owner as ToolMenu).btnStudentPayment.Font = new Font("Chaparral Pro", 15, FontStyle.Regular);
            (this.Owner as ToolMenu).btnStudentPayment.Text = "     Students Payment";
            (this.Owner as ToolMenu).btnReport.Font = new Font("Chaparral Pro", 15, FontStyle.Regular);
            (this.Owner as ToolMenu).btnReport.Text = "     Students Report";
            (this.Owner as ToolMenu).btnOption.Font = new Font("Chaparral Pro", 15, FontStyle.Regular);
            (this.Owner as ToolMenu).btnOption.Text = "     Option";
            (this.Owner as ToolMenu).btnClose.Font = new Font("Chaparral Pro", 15, FontStyle.Regular);
            (this.Owner as ToolMenu).btnClose.Text = "     Exit";

            (this.Owner as ToolMenu).lbMainName.Font = new Font("Chaparral Pro", 28, FontStyle.Regular);
            (this.Owner as ToolMenu).lbMainName.Text = "     Option";
            (this.Owner as ToolMenu).Body = "Are you sure want to exit ?";
            (this.Owner as ToolMenu).Title = "Exit";
        }
    }
}
