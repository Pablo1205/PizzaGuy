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
    /// Logique d'interaction pour NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Page
    {
        public NewOrder()
        {
            InitializeComponent();
        }
        private void RegisterCustomerButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterCustomer());
            return;
        }
        private void ContinueCustomer(object sender, RoutedEventArgs e)
        {
            Customer customer = new Classes.Customer(PhoneNumberTextBox.Text);
            if(customer.CheckIfUserIsInDatabase())
            {
                Console.WriteLine("User exists");
                Application.Current.Properties["CurrentCustomer"] = customer;
                NavigationService.Navigate(new CreateOrder());
            } else
            {
                TextError.Text = "User does not exist";
                Console.WriteLine("User does not exist");
            }
            //PhoneNumberTextBox
            return;
        }
    }
}
