using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    internal class Drink
    {

        public Drink(int DrinkID, string Name, string NameDescription, int Price, int Number)
        {
            this.DrinkID = DrinkID;
            this.Name = Name;
            this.Description = Description;
            this.Price = Price;
            this.Number = Number;

        }
        public int DrinkID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Number { get; set; }

    }
}
