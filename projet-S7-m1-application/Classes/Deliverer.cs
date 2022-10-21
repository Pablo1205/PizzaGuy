using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    internal class Deliverer
    {
        private int idDeliverer;
        public string fname { get; set; }
        public string lname { get; set; }

        private MySqlConnection conn = null;

        private MySqlConnection GetConnection()
        {
            if (this.conn != null) return this.conn;
            Database database = new Database();
            this.conn = database.conn;
            return this.conn;
        }
        private void CloseConnection()
        {
            this.conn.Close();
        }
        public Deliverer(int idDeliverer)
        {
            this.idDeliverer = idDeliverer;
            GetDelivererInfoFromID();
        }
        private void GetDelivererInfoFromID()
        {
            MySqlConnection conn = this.GetConnection();
            string sql = "SELECT * from Deliverer WHERE idDeliverer='" + this.idDeliverer + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                this.fname = rdr[1].ToString();
                this.lname = rdr[2].ToString();
            }
            rdr.Close();
            this.CloseConnection();
        }
        public void AddDelivererToDatabase(string fname, string lname)
        {
            this.fname = fname;
            this.lname = lname;
            string sql = "INSERT INTO Deliverer (fname, lname) VALUES ('" + fname + "','" + lname + "')";
            try
            {
                MySqlConnection conn = this.GetConnection();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                this.CloseConnection();


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
