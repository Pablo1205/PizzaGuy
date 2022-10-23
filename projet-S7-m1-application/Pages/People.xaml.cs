using MySql.Data.MySqlClient;
using projet_S7_m1_application.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace projet_S7_m1_application.Pages
{
    /// <summary>
    /// Logique d'interaction pour People.xaml
    /// </summary>
    public partial class People : Page
    {
        private Customer customer;
        List<Customer> allCustomers = new List<Customer>();
        List<Clerk> allClerks = new List<Clerk>();
        List<Deliverer> allDeliverers = new List<Deliverer>();
        public People()
        {
            InitializeComponent();
            customers.ItemsSource = this.allCustomers;
            workforceclerks.ItemsSource = this.allClerks;
            workforcedeliverers.ItemsSource = this.allDeliverers;
        }

        // remove customer
        private void Button_Click_Remove_Customer(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Removed");
            var result = ((Button)sender).Tag;
            
            int r = int.Parse(result.ToString());
            Console.WriteLine(r);
            if (((Button)sender).IsEnabled == true)
            {
                Database database = new Database();
                MySqlConnection conn = database.conn;

                string sql = "DELETE FROM Customer WHERE Customer.customerID=" + r;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Close();
            }
            ((Button)sender).IsEnabled = false;
        }

        // remove clerk
        private void Button_Click_Remove_Clerk(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Removed");
            var result = ((Button)sender).Tag;

            int r = int.Parse(result.ToString());
            Console.WriteLine(r);
            if (((Button)sender).IsEnabled == true)
            {
                Database database = new Database();
                MySqlConnection conn = database.conn;

                string sql = "DELETE FROM Clerk WHERE Clerk.idClerk=" + r;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Close();
            }
            ((Button)sender).IsEnabled = false;
        }

        // remove deliverer
        private void Button_Click_Remove_Deliverer(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Removed");
            var result = ((Button)sender).Tag;

            int r = int.Parse(result.ToString());
           // Console.WriteLine(r);
            if (((Button)sender).IsEnabled == true)
            {
                Database database = new Database();
                MySqlConnection conn = database.conn;

                string sql = "DELETE FROM Deliverer WHERE Deliverer.idDeliverer=" + r;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Close();
            }
            ((Button)sender).IsEnabled = false;
        }

        // update customer
        private void Button_Click_Update_Customer(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Updated");
            var res = ((Button)sender).Tag;
            int r = int.Parse(res.ToString());
            int i = 0;
            Database database = new Database();
            MySqlConnection conn = database.conn;

            string sql = "SELECT Customer.customerID FROM Customer WHERE Customer.customerID=" + r;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if ((int)rdr[0] == null) 
                {
                    ((Button)sender).IsEnabled = false;
                }
                else
                {
                    var CustomerIDSelected = ((Button)sender).Tag;
                    Customer result = this.allCustomers.Find(x => x.CustomerID == (int)CustomerIDSelected);
                    Application.Current.Properties["CurrentCustomer"] = result;
                    NavigationService.Navigate(new UpdateCustomer());
                }
            }
            rdr.Close();
        }

        // update clerk
        private void Button_Click_Update_Clerk(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Updated");
            var res = ((Button)sender).Tag;
            int r = int.Parse(res.ToString());
            int i = 0;
            Database database = new Database();
            MySqlConnection conn = database.conn;

            string sql = "SELECT Clerk.idClerk FROM Clerk WHERE Clerk.idClerk=" + r;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if ((int)rdr[0] == null)
                {
                    ((Button)sender).IsEnabled = false;
                }
                else
                {
                    var ClerkIDSelected = ((Button)sender).Tag;
                    Clerk result = this.allClerks.Find(x => x.idClerk == (int)ClerkIDSelected);
                    Application.Current.Properties["CurrentClerk"] = result;
                    NavigationService.Navigate(new UpdateClerk());
                }
            }
            rdr.Close();
        }

        // update deliverer
        private void Button_Click_Update_Deliverer(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Updated");
            var res = ((Button)sender).Tag;
            int r = int.Parse(res.ToString());
            int i = 0;
            Database database = new Database();
            MySqlConnection conn = database.conn;

            string sql = "SELECT Deliverer.idDeliverer FROM Deliverer WHERE Deliverer.idDeliverer=" + r;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                if ((int)rdr[0] == null)
                {
                    ((Button)sender).IsEnabled = false;
                }
                else
                {
                    var DelivererIDSelected = ((Button)sender).Tag;
                    Deliverer result = this.allDeliverers.Find(x => x.idDeliverer == (int)DelivererIDSelected);
                    Application.Current.Properties["CurrentDeliverer"] = result;
                    NavigationService.Navigate(new UpdateDeliverer());
                }
            }
            rdr.Close();
        }

        //show customers by
        private void OnComboBoxChanged_C(object sender, RoutedEventArgs e)
        {

            ComboBox comboBox = sender as ComboBox;
            string selected = (string)comboBox.SelectedValue;
            if (selected != null)
            {
                this.allCustomers.Clear();

                Database database = new Database();
                MySqlConnection conn = database.conn;

                string sql = "";

                if (selected == "By alphabetical order - First Name")
                {
                    sql = "SELECT Customer.*, SUM(CustomerOrder.price) AS spent FROM Customer JOIN CustomerOrder ON CustomerOrder.customerID = Customer.customerID GROUP BY Customer.customerID ORDER BY Customer.FirstName";
                }
                else if (selected == "By alphabetical order - Last Name")
                {
                    sql = "SELECT Customer.*, SUM(CustomerOrder.price) AS spent FROM Customer JOIN CustomerOrder ON CustomerOrder.customerID = Customer.customerID GROUP BY Customer.customerID ORDER BY Customer.LastName";
                }
                else if (selected == "By city")
                {
                    sql = "SELECT Customer.*, SUM(CustomerOrder.price) AS spent FROM Customer JOIN CustomerOrder ON CustomerOrder.customerID = Customer.customerID GROUP BY Customer.customerID ORDER BY Customer.Town";
                }
                else if (selected == "By amount of cumulative purchases")
                {
                    sql = "SELECT Customer.*, SUM(CustomerOrder.price) AS spent FROM Customer JOIN CustomerOrder ON CustomerOrder.customerID = Customer.customerID GROUP BY Customer.customerID ORDER BY spent DESC";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    this.allCustomers.Add(new Customer() { CustomerID = (int)rdr[0], FirstName = rdr[1].ToString(), LastName = rdr[2].ToString(), PhoneNumber = rdr[3].ToString(), City = rdr[4].ToString(), Street = rdr[5].ToString(), PostalCode = rdr[6].ToString(), Spent = int.Parse(rdr[7].ToString()) });
                }
                customers.Items.Refresh();
                rdr.Close();

                database.CloseConnection();
            }
        }

        //show clerks by
        private void OnComboBoxChanged_WC(object sender, RoutedEventArgs e)
        {

            ComboBox comboBox = sender as ComboBox;
            string selected = (string)comboBox.SelectedValue;
            if (selected != null)
            {
                this.allClerks.Clear();

                Database database = new Database();
                MySqlConnection conn = database.conn;

                string sql = "";

                if (selected == "By alphabetical order - First Name")
                {
                    sql = "SELECT * FROM Clerk ORDER BY Clerk.fname";
                }
                else if (selected == "By alphabetical order - Last Name")
                {
                    sql = "SELECT * FROM Clerk ORDER BY Clerk.lname";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    this.allClerks.Add(new Clerk() { idClerk= (int)rdr[0] , fname = rdr[1].ToString(), lname = rdr[2].ToString()});
                }
                workforceclerks.Items.Refresh();
                rdr.Close();

                database.CloseConnection();
            }
        }

        //show workers by
        private void OnComboBoxChanged_WD(object sender, RoutedEventArgs e)
        {

            ComboBox comboBox = sender as ComboBox;
            string selected = (string)comboBox.SelectedValue;
            if (selected != null)
            {
                this.allDeliverers.Clear();

                Database database = new Database();
                MySqlConnection conn = database.conn;

                string sql = "";

                if (selected == "By alphabetical order - First Name")
                {
                    sql = "SELECT * FROM Deliverer ORDER BY Deliverer.fname";
                }
                else if (selected == "By alphabetical order - Last Name")
                {
                    sql = "SELECT* FROM Deliverer ORDER BY Deliverer.lname; ";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    this.allDeliverers.Add(new Deliverer() { idDeliverer = (int)rdr[0], fname = rdr[1].ToString(), lname = rdr[2].ToString() });
                }
                workforcedeliverers.Items.Refresh();
                rdr.Close();

                database.CloseConnection();
            }
        }
    }
}
