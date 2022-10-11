using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    public class PizzaOrder : Order
    {
        public PizzaOrder(int id, int customerOrderID, int quantity)
        {
            this.Id = id;
            this.CustomerOrderID = customerOrderID;
            this.Quantity = quantity;
        }
    }
}
