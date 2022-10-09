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
    /// Logique d'interaction pour RegisterCustomer.xaml
    /// </summary>
    public partial class RegisterCustomer : Page
    {
        public RegisterCustomer()
        {
            InitializeComponent();
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
            } else if (FirstName.Length < 2)
            {
                TextError.Text = "First Name is incorrect";
                return;
            }
            else if (StreetText.Length == 0)
            {
                TextError.Text = "Street Name is incorrect";
                return;

            } else if (TownText.Length == 0)
            {
                TextError.Text = "Town is incorrect";
                return;
            } else if (PostalCodeText.Length == 0)
            {
                TextError.Text = "Postal code is incorrect";
                return;
            } else if (PhoneNumberText.Length == 0)
            {
                TextError.Text = "Phone number is incorrect";
                return; 
            }
            TextError.Text = "";
            Customer customer = new Classes.Customer(PhoneNumberText);
            if (customer.CheckIfUserIsInDatabase())
            {
                TextError.Text = "The customer already exists in the database";
                return;
            }
            else
            {
                customer.AddCustomerToDatabase(LastName, FirstName, StreetText, TownText, PostalCodeText);
            }       
        }
    }
}
