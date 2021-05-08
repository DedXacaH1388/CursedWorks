using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Journal.Classes;
using Newtonsoft.Json;
using System.IO;

namespace Journal.Classes
{
    public class User
    {
        public int id;
        public string enc_hash_password;

        public User(int id, string enc_hash_password)
        {
            this.id = id;
            this.enc_hash_password = enc_hash_password;
        }

        public bool check()
        {
            using (var conn = new NpgsqlConnection(Info.connStr.ConnectionString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand(@"SELECT pass_valid(:_id, :_hash);", conn))
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("_id", id);
                    command.Parameters.AddWithValue("_hash", Cryptography.Decrypt(enc_hash_password, "CSharp is not the best language"));
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.GetInt32(0) == 1)
                            return true;
                    }

                }
                conn.Close();
            }
            return false;
        }
        
        public void save()
        {
            var jsonToWrite = JsonConvert.SerializeObject(this, Formatting.Indented);
            jsonToWrite = Cryptography.Encrypt(jsonToWrite, Info.password);
            using (StreamWriter sw = new StreamWriter(Info.userFilePath))
                sw.Write(jsonToWrite);
        }
    }
}
