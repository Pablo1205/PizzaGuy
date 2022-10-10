using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    internal class PizzaOrder
    {
        private int pizzaID;
        private int customerOrderID;
        private int quantity;

        public int GetPizzaID()
        {
            return this.pizzaID;
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
