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
        private string description; 
        private int price;

        public Drink(int drinkID, string name, string description, int price)
        {
            this.drinkID = drinkID;
            this.name = name;
            this.description = description;
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

        public string GetDescription()
        {
            return this.description;
        }

        public int GetPrice()
        {
            return this.price;
        }
    }
}
