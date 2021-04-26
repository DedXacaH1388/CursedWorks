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

namespace Автосервис {
    public partial class Form4 : Form {
        Form1 f1 = new Form1();
        public Form4() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            #region Vars
            string server_name = textBox1.Text;
            string database_name = textBox2.Text;
            string user = f1.textBox1.Text;
            string password = f1.textBox2.Text;
            #endregion

            if (database_name == null) database_name = "Autoservice";

            string MyConStr = "Server=" + server_name + ";Database=" + database_name + ";uid=" + user + ";pwd=" + password;
            MySqlConnection conn = new MySqlConnection(MyConStr);
            conn.Open();

            if (conn.State == ConnectionState.Open) { 
                MessageBox.Show("Connection opened succesfully!"); 
                conn.Close();
                f1.Show();
            }
        }

        private void Form4_Load(object sender, EventArgs e) {
            f1.Hide();
        }
    }
}
