using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    public abstract class Order
    {
        public int Id;
        public int CustomerOrderID;
        public int Quantity;

        public int GetID()
        {   
            return this.Id;
        }

        public int GetCustomerOrderID()
        {
            return this.CustomerOrderID;
        }

        public int GetQuantity()
        {
            return this.Quantity;
        }
    }
}
