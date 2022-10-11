using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    public class DrinkOrder : Order
    {

        public int DrinkID { get; set; }

        public int CustomerOrderID { get; set; }

        public int Quantity { get; set; }
    }
}
