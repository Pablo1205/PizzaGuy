﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    public class DrinkOrder : Order
    {
        public DrinkOrder(int id, int customerOrderID, int quantity)
        {
            this.id = id;
            this.customerOrderID = customerOrderID; 
            this.quantity = quantity;
        }
    }
}
