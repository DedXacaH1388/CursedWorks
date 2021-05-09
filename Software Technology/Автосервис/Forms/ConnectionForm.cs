using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using carService.Classes;

namespace carService
{
    public partial class connectionForm : Form 
    {
        loginForm logForm = new loginForm();
        public connectionForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            Connection.getConnString(hostBox.Text, portBox.Text, dbComboBox.Text, logForm.loginBox.Text, logForm.passBox.Text);
        }

        private void Form4_Load(object sender, EventArgs e) 
        {
            logForm.Hide();
        }
    }
}
