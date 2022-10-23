﻿using MySql.Data.MySqlClient;
using projet_S7_m1_application.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projet_S7_m1_application.Pages
{
    /// <summary>
    /// Logique d'interaction pour OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Page
    {
        CustomerOrder customerOrder;

        List<CustomerOrder> ThisOrder = new List<CustomerOrder>();
        List<OrderString> OrderSD = new List<OrderString>();
        List<OrderString> OrderSP = new List<OrderString>();
        private string test = "string";
        public string curOrderStat
        {
            get { return test; }
            set { test = value; }
        }

        public OrderDetails()
        {
            InitializeComponent();
            this.customerOrder = Application.Current.Properties["CurrentOrder"] as CustomerOrder;
            this.DataContext = this;

            Database database = new Database();
            MySqlConnection conn = database.conn;

            //get customer information
            string sql = "SELECT * FROM CustomerOrder WHERE customerOrderID=" + this.customerOrder.CustomerOrderID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();



            while (rdr.Read())
            {

                this.ThisOrder.Add(new CustomerOrder((int)rdr[0], rdr[2].ToString(), (int)rdr[1]) { price = (int)rdr[6] });

            }
            
            clientcontact.ItemsSource = this.ThisOrder;
            clientaddress.ItemsSource = this.ThisOrder;
            rdr.Close();

            //get drink order 
            string sqldrink = "SELECT Drink.name, DrinkOrder.quantity FROM Drink JOIN DrinkOrder ON DrinkOrder.drinkID=Drink.drinkID JOIN CustomerOrder ON CustomerOrder.customerOrderID=DrinkOrder.customerOrderID WHERE CustomerOrder.customerOrderID=" + this.customerOrder.CustomerOrderID;
            MySqlCommand cmddrink = new MySqlCommand(sqldrink, conn);
            MySqlDataReader rdrdrink = cmddrink.ExecuteReader();


            while (rdrdrink.Read())
            {
                this.OrderSD.Add(new OrderString(rdrdrink[0].ToString(), (int)rdrdrink[1]));
            }
            drinks.ItemsSource = this.OrderSD;
            rdrdrink.Close();

            //get pizza order 
            string sqlpizza = "SELECT Pizza.name, PizzaOrder.quantity FROM Pizza JOIN PizzaOrder ON PizzaOrder.pizzaID=Pizza.pizzaID JOIN CustomerOrder ON CustomerOrder.customerOrderID=PizzaOrder.customerOrderID WHERE CustomerOrder.customerOrderID=" + this.customerOrder.CustomerOrderID;
            MySqlCommand cmdpizza = new MySqlCommand(sqlpizza, conn);
            MySqlDataReader rdrpizza = cmdpizza.ExecuteReader();


            while (rdrpizza.Read())
            {
                this.OrderSP.Add(new OrderString(rdrpizza[0].ToString(), (int)rdrpizza[1]));
            }
            pizzas.ItemsSource = this.OrderSP;
            rdrpizza.Close();
            database.CloseConnection();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["CustomerOrder"] = null;
            NavigationService.Navigate(new Order());
        }

        //show and update order status
        private void OnComboBoxChanged(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            this.customerOrder = Application.Current.Properties["CurrentOrder"] as CustomerOrder;
            string selected = (string)comboBox.SelectedValue;

            //this.customerOrder.SetAllToFalse();
            if (selected == "In delivery") {
                MessageBroker messageBroker = Application.Current.Properties["messageBroker"] as MessageBroker;
                messageBroker.SendMessageToLivreur(this.customerOrder);
            }
            Database database = new Database();
            MySqlConnection conn = database.conn;

            // update order status
            if (selected != null)
            {               
                string sql = "UPDATE CustomerOrder SET status = '" + selected + "' WHERE CustomerOrder.customerOrderID=" + this.customerOrder.CustomerOrderID;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Close();   
            }
            database.CloseConnection();
        }
    }
}
