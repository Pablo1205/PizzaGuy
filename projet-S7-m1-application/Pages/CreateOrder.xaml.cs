using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using projet_S7_m1_application.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    /// Logique d'interaction pour CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Page
    {
        private Customer customer;
        private List<Pizza> pizzaList = new List<Pizza>();
        private List<Drink> drinkList = new List<Drink>();
        private List<DrinkOrder> drinkOrderList = new List<DrinkOrder>();
        private List<PizzaOrder> pizzaOrderList = new List<PizzaOrder>();
        private int totalPrice = 0;

        public CreateOrder()
        {
            InitializeComponent();
            getCustomerInfo();
            GetPizzaList();
            GetDrinkList();
            RecapOrder.ItemsSource = this.pizzaOrderList;
            RecapOrderDrink.ItemsSource = this.drinkOrderList;
        }
        private void GetDrinkList()
        {
            Database database = new Database();
            MySqlConnection conn = database.conn;

            string sql = "SELECT * FROM Drink";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();


            while (rdr.Read())
            {
                this.drinkList.Add(new Drink((int)rdr[0], rdr[1].ToString(), rdr[2].ToString(), (int)rdr[3], 0));
            }
            DrinkList.ItemsSource = this.drinkList;
            database.CloseConnection();
            rdr.Close();
        }
        private void GetPizzaList()
        {
            Database database = new Database();
            MySqlConnection conn = database.conn;

            string sql = "SELECT * FROM Pizza";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();


            while (rdr.Read())
            {
                //(int)rdr[0], rdr[1].ToString(), rdr[2].ToString(),(int)rdr[3], 0)
                this.pizzaList.Add(new Pizza() { PizzaID = (int)rdr[0], Name= rdr[1].ToString(), Description= rdr[2].ToString(), Price= (int)rdr[3], Number = 0, SmallSize = true  });

            }
            PizzaList.ItemsSource = this.pizzaList;
            database.CloseConnection();
            rdr.Close();
        }
        private void getCustomerInfo()
        {
            Customer customer = Application.Current.Properties["CurrentCustomer"] as Customer;
            this.customer = customer;
            LastNameInput.Text = this.customer.GetLastName();
            FirstNameInput.Text = this.customer.GetFirstName();
            Street.Text = this.customer.GetStreet();
            Town.Text = this.customer.GetCity();
            postalCode.Text = this.customer.GetPostalCode();
            PhoneNumber.Text = this.customer.GetPhoneNumber();
        }
        private void goBack(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["CurrentCustomer"] = null;
            NavigationService.Navigate(new NewOrder());
        }
        private void AddDrink(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var dataContext = button.DataContext as Drink;
            foreach (Drink drink in drinkList)
            {
                if (drink.Name == dataContext.Name)
                {
                    drink.Number++;
                }

            }
            this.totalPrice += dataContext.Price;
            DrinkList.Items.Refresh();
        }
        private void RemoveDrink(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var dataContext = button.DataContext as Drink;
            foreach (Drink drink in drinkList)
            {
                if (drink.Name == dataContext.Name)
                {
                    drink.Number--;
                    if (drink.Number <0)
                    {
                        drink.Number = 0;
                    }
                    
                }

            }
            this.totalPrice -= dataContext.Price;
            DrinkList.Items.Refresh();
        }
        private void AddPizza(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var dataContext = button.DataContext as Pizza;
            foreach (Pizza pizza in pizzaList)
            {
                if (pizza.Name == dataContext.Name)
                {
                    pizza.Number++;
                }

            }

            if (dataContext.Size == "Medium")
            {
                this.totalPrice += dataContext.Price * 2;
            }
            else if (dataContext.Size == "Large")
            {
                this.totalPrice += dataContext.Price * 3;
            } else
            {
                this.totalPrice += dataContext.Price;
            }
            PizzaList.Items.Refresh();
        }

        private void RemovePizza(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var dataContext = button.DataContext as Pizza;
            foreach (Pizza pizza in pizzaList)
            {
                if (pizza.Name == dataContext.Name)
                {
                    pizza.Number--;
                    if (pizza.Number < 0)
                    {
                        pizza.Number = 0;
                    }
                }

            }
            if(dataContext.Size == "Medium")
            {
                this.totalPrice -= dataContext.Price * 2;
            } else if (dataContext.Size == "Large")
            {
                this.totalPrice -= dataContext.Price * 3;
            }
            else
            {
                this.totalPrice -= dataContext.Price;
            }
       
            PizzaList.Items.Refresh();
        }

        private void AddToOrder(object sender, RoutedEventArgs e)
        {
            // add all pizzas to the list order
            foreach (Pizza pizza in pizzaList)
            {
                if (pizza.Number > 0)
                {
                    PizzaOrder order = this.pizzaOrderList.Find(i => (i.PizzaID == pizza.PizzaID && i.Size == pizza.Size));
                    if(order == null)
                    {
                        // small = *1 medium = *2 large = *3
                        int price = pizza.Price;  
                        if(pizza.Size == "Medium")
                        {
                            price *= 2;

                        } else if(pizza.Size == "Large")
                        {
                            price *= 3;
                        }
                        order = new PizzaOrder() { PizzaID = pizza.PizzaID, Quantity = pizza.Number, Size = pizza.Size, Price = price, Name = pizza.Name}; 
                        this.pizzaOrderList.Add(order);
                    } else
                    {
                        order.Quantity += pizza.Number;
                    }

                }
                // now reset value of pizza
                pizza.Number = 0;

            }
            // add all drinks to the list order
            foreach (Drink drink in drinkList)
            {
                if (drink.Number > 0)
                {
                    DrinkOrder order = this.drinkOrderList.Find(i => i.DrinkID == drink.DrinkID);
                    if (order == null)
                    {
                        // small = *1 medium = *2 large = *3
                        order = new DrinkOrder() { DrinkID = drink.DrinkID, Quantity = drink.Number, Price = drink.Price, Name = drink.Name };
                        this.drinkOrderList.Add(order);
                    }
                    else
                    {
                        order.Quantity += drink.Number;
                    }

                }
                // now reset value of pizza
                drink.Number = 0;

            }
            totalPriceText.Text = this.totalPrice.ToString();
            DrinkList.Items.Refresh();
            PizzaList.Items.Refresh();
            RecapOrder.Items.Refresh();
            RecapOrderDrink.Items.Refresh();
        }
        private void ConfirmOrder(object sender, RoutedEventArgs e)
        {
            if(this.pizzaOrderList.Count == 0)
            {
                MessageBox.Show("Customer need to order one pizza at least");
                return;
            }

            Database database = new Database();
            MySqlConnection conn = database.conn;
            DateTime date = DateTime.Now;
            
            try
            {
                // insert customerorder
                MySqlCommand cmd = new MySqlCommand("INSERT INTO CustomerOrder (customerID, status, orderDate, price) VALUES(@customerID, @status, @orderDate, @price)", conn);
                
                cmd.Parameters.AddWithValue("@customerID", this.customer.CustomerID);
                cmd.Parameters.AddWithValue("@status", "ordered");
                cmd.Parameters.AddWithValue("@orderDate", date);
                cmd.Parameters.AddWithValue("@price", this.totalPrice);


                cmd.ExecuteNonQuery();

                // get customer order ID
                MySqlCommand cmd2 = new MySqlCommand("SELECT customerOrderID FROM CustomerOrder WHERE customerID=@customerID ORDER BY customerOrderID DESC LIMIT 1;", conn);
                cmd2.Parameters.AddWithValue("@customerID", this.customer.CustomerID);
                MySqlDataReader rdr = cmd2.ExecuteReader();
                int customerOrderID = -1;
                while (rdr.Read())
                {
                    customerOrderID = (int)rdr[0];

                }
                rdr.Close();

                // insert PizzaOrder
                foreach (PizzaOrder order in pizzaOrderList)
                {
                    MySqlCommand cmd3 = new MySqlCommand("INSERT INTO PizzaOrder (pizzaID, customerOrderID, quantity, size) VALUES (@pizzaID, @customerOrderID, @quantity, @size)", conn);
                    cmd3.Parameters.AddWithValue("@customerOrderID", customerOrderID);
                    cmd3.Parameters.AddWithValue("@pizzaID", order.PizzaID);
                    cmd3.Parameters.AddWithValue("@quantity", order.Quantity);
                    cmd3.Parameters.AddWithValue("@size", order.Size);
                    cmd3.ExecuteNonQuery();
                }

                // isnert DrinkOrder
                foreach (DrinkOrder order in drinkOrderList)
                {
                    MySqlCommand cmd4 = new MySqlCommand("INSERT INTO PizzaOrder (pizzaID, customerOrderID, quantity) VALUES (@pizzaID, @customerOrderID, @quantity)", conn);
                    cmd4.Parameters.AddWithValue("@customerOrderID", customerOrderID);
                    cmd4.Parameters.AddWithValue("@pizzaID", order.DrinkID);
                    cmd4.Parameters.AddWithValue("@quantity", order.Quantity);
                    cmd4.ExecuteNonQuery();
                }

                database.CloseConnection();

                Application.Current.Properties["CurrentCustomer"] = null;
                MessageBroker messageBroker = Application.Current.Properties["messageBroker"] as MessageBroker;
                messageBroker.SendMessageToCuisine(new CustomerOrder(){ CustomerOrderID = customerOrderID, status= "ordered",price=this.totalPrice, Customer = this.customer, orderDate=date.ToString()});
                NavigationService.Navigate(new Order());

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }

        private void OnComboBoxChanged(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            var dataContext = comboBox.DataContext as Pizza;

            Pizza pizza = this.pizzaList.Find(i => i.PizzaID == dataContext.PizzaID);
            pizza.Size = (string)comboBox.SelectedValue;
            if(pizza.Size == "Small")
            {
                pizza.SmallSize = true;
                pizza.MediumSize = false;
                pizza.LargeSize = false;
            } else if (pizza.Size == "Medium")
            {
                pizza.SmallSize = false;
                pizza.MediumSize = true;
                pizza.LargeSize = false;
            } else
            {
                pizza.SmallSize = false;
                pizza.MediumSize = false;
                pizza.LargeSize = true;
            }
        }
    }
}
