using System;
using System.Windows.Forms;
using System.Data;
using Npgsql;

namespace Автосервис.Classes
{
    class Connection
    {
        public static string getConnString()
        {
            string host = "localhost";
            string port = "5432";
            string db   = "postgres";
            string user = "postgres";
            string pass = "1";

            if (db == null || db == "") db = "car_service";

            string myConStr = string.Format("Host={0};Port={1};Database={2};Username={3};Password={4};", host, port, db, user, pass);
            return myConStr;
        }

        public static NpgsqlConnection con = new NpgsqlConnection(getConnString());
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
                MessageBox.Show("An erroe occured" + ex.Message, "Пошёл нахуй отсюда",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }

            return dt;
        }
    }
}
