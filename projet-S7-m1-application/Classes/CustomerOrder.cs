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


