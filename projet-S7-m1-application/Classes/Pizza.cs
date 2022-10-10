using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    internal class Pizza
    {
        private int pizzaID;
        private string name;
        private string description;
        private int price;
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
            this.conn = null;
        }

        public int GetPizzaID()
        {
            return this.pizzaID;
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetDescription()
        {
            return this.description;
        }

        public int GetPrice()
        {
            return this.price;
        }
    }
}
