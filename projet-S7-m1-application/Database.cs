using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace projet_S7_m1_application
{
    internal class Database
    {
        // MYSQL PASSWORD : kuYLhcayA8h373r
        // MYSQL USERNAME :     
        // Database : robebou_m1_application_project 
        // Host : mysql-robebou.alwaysdata.net
        public MySql.Data.MySqlClient.MySqlConnection conn;
        public Database()
        {
            this.InitConnection();
        }
        private void InitConnection()
        {
            try
            {
                string myConnectionString = "SERVER=mysql-robebou.alwaysdata.net; DATABASE=robebou_m1_application_project; UID=robebou_m1; PASSWORD=kuYLhcayA8h373r";
                this.conn = new MySql.Data.MySqlClient.MySqlConnection();
                this.conn.ConnectionString = myConnectionString;
                this.conn.Open();
                Console.Write("Connection suceeded");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void CloseConnection()
        {
            this.conn.Close();
        }
    }
}
