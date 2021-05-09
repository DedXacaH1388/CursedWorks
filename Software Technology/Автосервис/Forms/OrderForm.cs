using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carService
{
    public partial class orderForm : Form
    {
        orderCreateForm f3 = new orderCreateForm();
        loginForm f1 = new loginForm();

        public orderForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            f1.Hide();
        }
    }
}
