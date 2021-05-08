using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace Warehouse.Classes
{
    class Database
    {

        private static string getConnectionString()
        {

            string host = "Host=localhost;";
            string port = "Port=5432;";
            string db = "Database=car_service;";
            string user = "Username=postgres;";
            string pass = "Password=1;";

            return string.Format("{0}{1}{2}{3}{4}", host, port, db, user, pass);

        }

        public static NpgsqlConnection con = new NpgsqlConnection(getConnectionString());
        public static NpgsqlCommand cmd = default(NpgsqlCommand);
        public static string sql = string.Empty;

        public static DataTable PerformDatabase(NpgsqlCommand com)
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
                MessageBox.Show("Ошибка: " + ex.Message, "Выполнение операции прервано",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }

            return dt;

        }

    }
}