using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace projet_S7_m1_application.Classes
{
    public class Order
    {
        public int Id;
        public int CustomerOrderID;

        public int Price { get; set; }
        public string Name { get; set; }

        public int Quantity { get; set; }


        public int GetID()
        {   
            return this.Id;
        }

        public int GetCustomerOrderID()
        {
            return this.CustomerOrderID;
        }

    }
}
