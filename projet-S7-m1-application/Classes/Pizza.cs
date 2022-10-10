using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_S7_m1_application.Classes
{
    internal class Pizza
    {
        private int pizzaID;
        private string name;
        private string description;
        private string price;

        public Pizza(int pizzaID, string name, string description, string price)
        {
            this.pizzaID = pizzaID;
            this.name = name;
            this.description = description;
            this.price = price;
        }

        public int GetPizzaID()
        {
            return this.pizzaID;
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetDescription()
        {
            return this.description;
        }

        public string GetPrice()
        {
            return this.price;
        }
    }
}
