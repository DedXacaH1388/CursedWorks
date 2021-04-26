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

namespace Автосервис
{
    public partial class Form1 : Form
    {
        Form2 f2 = new Form2();
        Form4 f4 = new Form4();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string l, p;
            l = "000";
            p = "111";
            string ll, pp;
            ll = textBox1.Text;
            pp = textBox2.Text;
            if ((ll == l) && (pp == p))
            {
                f2.Show();
            }
            else label1.Text = "Login or Password incorrect";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f4.Show();
        }
    }
}
