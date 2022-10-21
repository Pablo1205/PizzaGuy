using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    public class NumberOfOrdersByWorker
    {
        public string fname { get; set; }
        public string lname { get; set; }
        public int customerOrderID;
        public int NOfOrders { get; set; }

        public NumberOfOrdersByWorker(string fname, string lname, int customerOrderID, int NOfOrders)
        {
            this.fname = fname;
            this.lname = lname;
            this.customerOrderID = customerOrderID;
            this.NOfOrders = NOfOrders;
        }
    }
}
