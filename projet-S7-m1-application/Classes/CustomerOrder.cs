using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    internal class CustomerOrder
    {
        private int CustomerOrderID;
        private int CustomerID;
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

        public int GetCustomerOrderID()
        {
            return this.CustomerOrderID;
        }

        public int GetCustomerID()
        {
            return this.CustomerID;
        }
    }
}


