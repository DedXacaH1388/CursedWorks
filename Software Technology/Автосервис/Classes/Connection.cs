using System;
using System.Windows.Forms;
using System.Data;
using Npgsql;
using carService;

namespace carService.Classes
{
    class Connection
    {
        static loginForm logForm = new loginForm();
        static connectionForm conForm = new connectionForm();

        public static string host = conForm.hostBox.Text;
        public static string port = conForm.portBox.Text;
        public static string db   = conForm.dbComboBox.Text;
        public static string user = logForm.loginBox.Text;
        public static string pass = logForm.passBox.Text;

        public static string getConnString(string host, string port, string db, string user, string pass)
        {
            string myConStr = string.Format("Host={0};Port={1};Database={2};Username={3};Password={4};", 
                                            host, port, db, user, pass);
            return myConStr;
        }

        public static NpgsqlConnection con = new NpgsqlConnection(getConnString(host, port, db, user, pass));
        public static NpgsqlCommand cmd = default(NpgsqlCommand);
        public static string sql = string.Empty;

        public static DataTable niceCon(NpgsqlCommand com)
        {
            NpgsqlDataAdapter da = default(NpgsqlDataAdapter);
            DataTable dt = new DataTable();

            try
            {
                da = new NpgsqlDataAdapter();
                da.SelectCommand = com;
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yes;\n" + ex.Message, "No", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }

            return dt;
        }
    }
}
