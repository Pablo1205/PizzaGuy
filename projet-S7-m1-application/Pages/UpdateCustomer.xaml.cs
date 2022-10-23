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
    /// Logique d'interaction pour UpdateCustomer.xaml
    /// </summary>
    public partial class UpdateCustomer : Page
    {
        Customer customer;
        private string test1 = "";
        private string test2 = "";
        private string test3 = "";
        private string test4 = "";
        public string name
        {
            get { return test1; }
            set { test1 = value; }
        }

        public string phone
        {
            get { return test2; }
            set { test2 = value; }
        }

        public string street
        {
            get { return test3; }
            set { test3 = value; }
        }

        public string zip
        {
            get { return test4; }
            set { test4 = value; }
        }
        public UpdateCustomer()
        {
            InitializeComponent();
            getCustomerInfo();
        }

        private void ConfirmCustomer(object sender, RoutedEventArgs e)
        {
            string LastName = LastNameInput.Text;
            string FirstName = FirstNameInput.Text;
            string StreetText = Street.Text;
            string TownText = Town.Text;
            string PostalCodeText = postalCode.Text;
            string PhoneNumberText = PhoneNumber.Text;
            if (LastName.Length < 2)
            {
                TextError.Text = "Last Name is incorrect";
                return;
            }
            else if (FirstName.Length < 2)
            {
                TextError.Text = "First Name is incorrect";
                return;
            }
            else if (StreetText.Length == 0)
            {
                TextError.Text = "Street Name is incorrect";
                return;

            }
            else if (TownText.Length == 0)
            {
                TextError.Text = "Town is incorrect";
                return;
            }
            else if (PostalCodeText.Length == 0)
            {
                TextError.Text = "Postal code is incorrect";
                return;
            }
            else if (PhoneNumberText.Length == 0)
            {
                TextError.Text = "Phone number is incorrect";
                return;
            }
            TextError.Text = "";

            this.customer = Application.Current.Properties["CurrentCustomer"] as Customer;

            Database database = new Database();
            MySqlConnection conn = database.conn;

            string sql = "UPDATE Customer SET Customer.FirstName='" + FirstName + "' , Customer.LastName='" + LastName + "' , Customer.PhoneNumber='" + PhoneNumberText + "' , Customer.Town='" + TownText + "' , Customer.Street='" + StreetText + "' , Customer.PostalCode='" + PostalCodeText + "'  WHERE Customer.customerID=" + this.customer.CustomerID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            Application.Current.Properties["CurrentCustomer"] = null;
            NavigationService.Navigate(new People());
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

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["CustomerCustomer"] = null;
            NavigationService.Navigate(new People());
        }
    }
}
