using rainflow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace record_of_clicks_on_files
{
    public partial class Login : Form
    {
        public Login()
        {
            
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            label3.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "ssaletti") && (textBox2.Text == "@weg123"))
            {

                this.Hide();
                label3.Hide();
                Form1 form1 = new Form1();
                form1.FormClosed += (s, args) => this.Close();
                form1.Show();
            }
            else
            {
                label3.Show();
            }
        }
    }
}
