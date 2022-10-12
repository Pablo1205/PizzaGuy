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
        public OrderDetails()
        {
            InitializeComponent();
        }

        private void goBack(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["CurrentCustomer"] = null;
            NavigationService.Navigate(new Order());
        }
    }
}
