using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms; 
using Npgsql;
using Newtonsoft.Json;
using System.IO;

namespace Journal.Classes
{
    class ConnectString
    {
        public string host;
        public int port;
        public string username;
        public string enc_password;
        public string database;

        public ConnectString(string host, int port, string username, string enc_password, string database)
        {
            this.host = host;
            this.port = port;
            this.username = username;
            this.enc_password = enc_password;
            this.database = database;
        }

        public bool checkConnection()
        {
            var connStrBuilder = new NpgsqlConnectionStringBuilder();
            connStrBuilder.Host = this.host;
            connStrBuilder.Port = this.port;
            connStrBuilder.Username = this.username;
            connStrBuilder.Password = Cryptography.Decrypt(this.enc_password, "CSharp is not the best language");
            connStrBuilder.Database = this.database;

            try
            {
                using (var chkConn = new NpgsqlConnection(connStrBuilder.ConnectionString))
                    chkConn.Open();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public void save()
        {
            var jsonToWrite = JsonConvert.SerializeObject(this, Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(Info.configFilePath))
                sw.Write(jsonToWrite);
        }
    }
}
