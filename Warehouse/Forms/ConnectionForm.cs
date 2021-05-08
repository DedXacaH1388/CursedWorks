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
using Npgsql;
using Newtonsoft.Json;
using Journal.Classes;
using System.Runtime.InteropServices;


namespace Journal.Forms
{
     
    public partial class ConnectionForm : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        public ConnectionForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            //get connection strings
            Info.connStr.Host = hostTextBox.Text;
            Info.connStr.Port = Convert.ToInt32(portTextBox.Text);
            Info.connStr.Username = userTextBox.Text;
            Info.connStr.Password = passwordTextBox.Text;
            Info.connStr.Database = databaseComboBox.Text;

            bool flag = true;

            ConnectString db = new ConnectString(
                Info.connStr.Host,
                Info.connStr.Port,
                Info.connStr.Username,
                Cryptography.Encrypt(Info.connStr.Password, "CSharp is not the best language"),
                Info.connStr.Database
                );

            flag = db.checkConnection();

            //if flag is set then save connection string to connection.json
            if (flag)
            {
                db.save();
                Info Launch = new Info();
                Launch.LoadDatabase();
                Launch.LoadUser();
                this.Hide();
            }
            else
                MessageBox.Show("Unexpected error occured", "Connection error", MessageBoxButtons.OK);
        }

        // get database list
        IEnumerable<string> getDatabaseNames()
        {
            var connStrBuilder = new NpgsqlConnectionStringBuilder();
            connStrBuilder.Host = hostTextBox.Text;
            connStrBuilder.Port = Convert.ToInt32(portTextBox.Text);
            connStrBuilder.Username = userTextBox.Text;
            connStrBuilder.Password = passwordTextBox.Text;
            connStrBuilder.Database = "postgres";

            bool flag = false;

            try
            {
                var chkConn = new NpgsqlConnection(connStrBuilder.ConnectionString);
                chkConn.Open();
                chkConn.Close();
            }
            catch (Exception e)
            {
                flag = true;
                MessageBox.Show(e.ToString(), "Ошибка подключения", MessageBoxButtons.OK);
            }
            if (!flag)
            {
                using (var conn = new NpgsqlConnection(connStrBuilder.ConnectionString))
                {
                    conn.Open();
                    using (var command = new NpgsqlCommand("SELECT datname FROM pg_database WHERE datistemplate = false ORDER BY datname;", conn))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return reader.GetString(0);
                        }
                    }
                }
            }
        }

        private void databaseComboBox_DropDown_1(object sender, EventArgs e)
        {
            databaseComboBox.Items.Clear();
            foreach (var x in getDatabaseNames())
                databaseComboBox.Items.Add(x);
        }

        private void ConnectionForm_Load(object sender, EventArgs e)
        {

            this.BackColor = Info.pal.background;
            Color tmp = Info.pal.text;
            this.ForeColor = tmp;
            hostTextBox.ForeColor = tmp;
            portTextBox.ForeColor = tmp;
            userTextBox.ForeColor = tmp;
            passwordTextBox.ForeColor = tmp;
            databaseComboBox.ForeColor = tmp;
            topLabel.ForeColor = tmp;
            closeButton.IconColor = tmp;

            tmp = Info.pal.backgroundAccent;
            hostTextBox.BackColor = tmp;
            portTextBox.BackColor = tmp;
            userTextBox.BackColor = tmp;
            passwordTextBox.BackColor = tmp;
            databaseComboBox.BackColor = tmp;
            databaseComboBox.BorderColor = tmp;
            databaseComboBox.ButtonColor = Info.pal.background;
            connectButton.BackColor = tmp;
            cancelButton.BackColor = tmp;

            tmp = Info.pal.themeColor;
            topPanel.BackColor = tmp;
            closeButton.BackColor = tmp;

            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void ConnectionForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.BackColor = Info.pal.negativeValue;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.BackColor = Info.pal.themeColor;
        }
    }
}
