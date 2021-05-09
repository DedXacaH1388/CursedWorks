using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Warehouse.Classes;
using Npgsql;

namespace Warehouse.Forms
{
    public partial class LoginForm : Form
    {
        private List<Records> teachers = new List<Records>();

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
        public LoginForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

            this.BackColor = Info.pal.background;
            Color tmp = Info.pal.text;
            this.ForeColor = tmp;
            usernameLabel.ForeColor = tmp;
            passwordLabel.ForeColor = tmp;
            usernameComboBox.ForeColor = tmp;
            passwordTextBox.ForeColor = tmp;
            loginButton.ForeColor = tmp;
            topLabel.ForeColor = tmp;
            closeButton.IconColor = tmp;
            iconPicture.IconColor = tmp;
            

            tmp = Info.pal.backgroundAccent;
            usernameComboBox.BackColor = tmp;
            passwordTextBox.BackColor = tmp;
            loginButton.BackColor = tmp;
            

            tmp = Info.pal.themeColor;
            topPanel.BackColor = tmp;
            closeButton.BackColor = tmp;
            using (var conn = new NpgsqlConnection(Info.connStr.ConnectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM get_teachers();", conn))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        teachers.Add(new Records(reader.GetInt32(0), reader.GetString(1)));
                        usernameComboBox.Items.Add(reader.GetString(1));
                    }
                }
                conn.Close();
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            var teacher = teachers[usernameComboBox.SelectedIndex];
            int id = teacher.id;
            string hsh = Cryptography.GetHashString(passwordTextBox.Text);
            hsh = Cryptography.Encrypt(hsh, Info.password);
            User user = new User(id, hsh);
            if (user.check())
            {
                user.save();
                Info Launch = new Info();
                Launch.LoadUser();
                this.Hide();
            }
            else
                MessageBox.Show("Login or password is incorrect", "Connection error", MessageBoxButtons.OK);
        }

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Info.pal.backgroundAccent);
            Graphics g = e.Graphics;
            int variance = 1;
            g.DrawRectangle(p, new Rectangle(
                passwordTextBox.Location.X - variance,
                passwordTextBox.Location.Y - variance,
                passwordTextBox.Width + variance,
                passwordTextBox.Height + variance
                ));
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
