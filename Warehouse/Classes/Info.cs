using Newtonsoft.Json;
using Npgsql;
using System;
using System.IO;
using System.Windows.Forms;
using Warehouse.Classes;
using Warehouse.Forms;
using System.Collections.Generic;

namespace Warehouse.Classes
{
    public class Info
    {

        //forms instances
        ConnectionForm conForm = new ConnectionForm();
        LoginForm logForm = new LoginForm();

        // pathways to settings
        private static string configPath = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "Journal/Config");
        private static string palettePath = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "Journal/Themes");

        // pathways to setting files
        public static string configFilePath = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "Journal/Config/connection.json");
        public static string paletteFilePath = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "Journal/Themes/DefaultTheme.json");
        public static string userFilePath = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "Journal/profile.prof");

        //encryption password
        public static string password = "CSharp is not the best language";

        //PostgreSQL connection string
        public static NpgsqlConnectionStringBuilder connStr = new NpgsqlConnectionStringBuilder();

        //active palette
        public static Palette pal = new Palette();

        //current user
        public static User _user;
        public static Records _class;
        public static Records _subject;

        public void LoadPalette()
        {
            string json;

            //check if palette path exists
            if (!Directory.Exists(palettePath))
            {
                //if not then create path
                Directory.CreateDirectory(palettePath);
            }

            //check if palette.json exists
            if (!File.Exists(paletteFilePath))
            {
                //if not then load deafult values
                pal = new Palette();
                var myFile = File.Create(paletteFilePath);
                myFile.Close();
                pal.save();
            }

            //try deserializing json to object
            try
            {
                using (var reader = new StreamReader(paletteFilePath))
                json = reader.ReadToEnd();
                pal = JsonConvert.DeserializeObject<Palette>(json);
                if (pal == null)
                {
                    pal = new Palette();
                    var jsonToWrite = JsonConvert.SerializeObject(pal, Formatting.Indented);
                    using (StreamWriter sw = new StreamWriter(paletteFilePath))
                        sw.Write(jsonToWrite);
                }
                json = null;
            }
            catch (Exception)
            {

            }
                
        }

        public void LoadDatabase()
        {
            string json;
            
            //check if config path exists
            if (!Directory.Exists(configPath))
            {
                //if not then create path
                Directory.CreateDirectory(configPath);
            }

            //check if connection.json exists
            if (!File.Exists(configFilePath))
            {
                //if not then create file and open connection form
                var myFile = File.Create(configFilePath);
                myFile.Close();
                conForm.Show();
                conForm.Activate();
            }
            //try deserializing json to object
            try
            {
                using (var reader = new StreamReader(configFilePath))
                    json = reader.ReadToEnd();
                ConnectString db = JsonConvert.DeserializeObject<ConnectString>(json);
                json = null;

                if (db == null || !db.checkConnection())
                {
                    conForm.Show();
                    conForm.Activate();
                }
                else
                {
                    //convert db to connStr
                    connStr.Host = db.host;
                    connStr.Port = db.port;
                    connStr.Username = db.username;
                    connStr.Password = Cryptography.Decrypt(db.enc_password, password);
                    connStr.Database = db.database;
                    this.LoadUser();
                }
            }
            catch (Exception)
            {
                conForm.Show();
                conForm.Activate();
            }
        }

        public void LoadUser()
        {
            string json;

            //check if user path exists
            if (!Directory.Exists(configPath))
            {
                //if not then create path
                Directory.CreateDirectory(configPath);
            }

            //check if user.json exists
            if (!File.Exists(userFilePath))
            {
                //if not then create file and open connection form
                var myFile = File.Create(userFilePath);
                myFile.Close();
                logForm.Show();
                logForm.Activate();
            }

            //try deserializing json to object
            try
            {
                using (var reader = new StreamReader(userFilePath))
                    json = reader.ReadToEnd();
                json = Cryptography.Decrypt(json, password);
                _user = JsonConvert.DeserializeObject<User>(json);
                json = null;

                if (_user == null || !_user.check())
                {
                    logForm.Show();
                    logForm.Activate();
                }
            }
            catch (Exception)
            {
                logForm.Show();
                logForm.Activate();
            }
        }
    }
    
    public class Records
    {
        public int id;
        public string value;

        public Records(int id, string value)
        {
            this.id = id;
            this.value = value;
        }
    }
}
