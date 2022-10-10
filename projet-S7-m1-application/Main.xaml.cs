using MySql.Data.MySqlClient;
using projet_S7_m1_application.Classes;
using projet_S7_m1_application.Pages;
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
using System.Windows.Shapes;

namespace projet_S7_m1_application
{
    /// <summary>
    /// Logique d'interaction pour Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            GetAvailablePizza();
            GetAvailableDrink();
            InitializeComponent();
        }

        //database.conn

        private void GetAvailablePizza()
        {
            Database newDatabse = new Database();
            MySqlConnection conn = newDatabse.conn;
            string sql = "SELECT * from Pizza";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            List<Pizza> listPizza = new List<Pizza>();
            while (rdr.Read())
            {
                Pizza pizza = new Pizza((int)rdr[0],rdr[1].ToString(), rdr[2].ToString(), rdr[3].ToString());
                listPizza.Add(pizza);
            }
            rdr.Close();
            Application.Current.Properties["AvailablePizza"] = listPizza;
            listPizza.ForEach(pizza =>
            {
                Console.WriteLine(pizza.GetName());
            });
            newDatabse.CloseConnection();
        }
        private void GetAvailableDrink()
        {
            Database newDatabse = new Database();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ShowOrderMenu(object sender, RoutedEventArgs e)
        {
            return;
        }
        private void ShowStatMenu(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = sidebar.SelectedItem as Path;
            if(selected.Name == "iconOrder")
            {
                navFrame.Navigate(new Order());
            } else if (selected.Name == "iconStat")
            {
                navFrame.Navigate(new Stat());
            } else if(selected.Name == "iconChat")
            {
                navFrame.Navigate(new Chat());
            } else if (selected.Name == "iconPeople")
            {
                navFrame.Navigate(new People());
            } else if (selected.Name == "iconNewOrder")
            {
                Customer customer = Application.Current.Properties["CurrentCustomer"] as Customer;
                if(customer != null)
                {
                    navFrame.Navigate(new CreateOrder());
                } else
                {
                    navFrame.Navigate(new NewOrder());
                }
            }
        }
    }
}
