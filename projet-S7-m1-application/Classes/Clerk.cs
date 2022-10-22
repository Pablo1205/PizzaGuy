using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    internal class Clerk
    {
        public int idClerk { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }

        private MySqlConnection conn = null;

        public Clerk(int idClerk, string fname, string lname) 
        {
            this.idClerk = idClerk;
            this.fname = fname;
            this.lname = lname;
        }

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
        public Clerk(int idClerk)
        {
            this.idClerk = idClerk;
            GetClerkInfoFromID();
        }
        private void GetClerkInfoFromID()
        {
            MySqlConnection conn = this.GetConnection();
            string sql = "SELECT * from Clerk WHERE idClerk='" + this.idClerk + "'";
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
        public void AddClerkToDatabase(string fname, string lname)
        {
            this.fname = fname;
            this.lname = lname;
            string sql = "INSERT INTO Clerk (fname, lname) VALUES ('" + fname + "','" + lname +"')";
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
