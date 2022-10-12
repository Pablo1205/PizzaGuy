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
        public CustomerOrder(int CustomerOrderID, string status, int CustomerID)
        {
            this.Customer = new Customer(CustomerID);
            this.CustomerOrderID = CustomerOrderID;
            this.CustomerID = CustomerID;
            this.status = status;
        }

        public int CustomerID { get; }
        public string status { get; }
        public int CustomerOrderID { get; }
        public Customer Customer { get; }

    }
}


