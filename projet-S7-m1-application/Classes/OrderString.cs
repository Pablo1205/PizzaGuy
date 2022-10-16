using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    internal class OrderString
    {
        public string name { get; set; }
        public int qty { get; set; }


        public OrderString(string name, int qty)
        {
            this.name = name;
            this.qty = qty;
        }
    }
}
