using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
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
    /// Logique d'interaction pour CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Page
    {
        private Customer customer;
        private List<Pizza> pizzaList = new List<Pizza>();
        private List<Drink> drinkList = new List<Drink>();
        private int totalPrice = 0;

        public CreateOrder()
        {
            InitializeComponent();
            getCustomerInfo();
            GetPizzaList();
            GetDrinkList();
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
                this.pizzaList.Add(new Pizza((int)rdr[0], rdr[1].ToString(), rdr[2].ToString(),(int)rdr[3], 0));
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
            totalPriceText.Text = this.totalPrice.ToString();

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
            totalPriceText.Text = this.totalPrice.ToString();

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

            PizzaList.Items.Refresh();
            this.totalPrice += dataContext.Price;
            totalPriceText.Text = this.totalPrice.ToString();


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
            this.totalPrice -= dataContext.Price;
            totalPriceText.Text = this.totalPrice.ToString();
            PizzaList.Items.Refresh();
        }
    }
}
