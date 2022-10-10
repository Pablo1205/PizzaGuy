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
        public CreateOrder()
        {
            InitializeComponent();
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
    }
}
