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

        public CustomerOrder(int CustomerOrderID, int idClerk, int idDeliverer)
        {
            this.CustomerOrderID = CustomerOrderID;
            this.idClerk = idClerk;
            this.idDeliverer = idDeliverer;
        }

        public CustomerOrder(string orderDate)
        {
            this.orderDate = orderDate;
        }

        public int CustomerID { get; }
        public string status { get; }
        public int CustomerOrderID { get; }

        public string orderDate { get; set; }
        public int idClerk { get; set; }
        public int idDeliverer { get; set; }
        public Customer Customer { get; }

    }
}


