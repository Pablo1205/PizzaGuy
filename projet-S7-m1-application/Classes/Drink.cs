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
        private int drinkID;
        private string name;    
        private string price;

        public Drink(int drinkID, string name, string price)
        {
            this.drinkID = drinkID;
            this.name = name;
            this.price = price;
        }

        public int GetDrinkID()
        {
            return this.drinkID;
        }

        public string GetName()
        {
            return this.name;
        }
        public string GetPrice()
        {
            return this.price;
        }
    }
}
