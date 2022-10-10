using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    public abstract class Order
    {
        public int id;
        public int customerOrderID;
        public int quantity;

        public int GetID()
        {   
            return this.id;
        }

        public int GetCustomerOrderID()
        {
            return this.customerOrderID;
        }

        public int GetQuantity()
        {
            return this.quantity;
        }
    }
}
