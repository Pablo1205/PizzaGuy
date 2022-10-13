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
        public OrderDetails()
        {
            InitializeComponent();
            this.customerOrder = Application.Current.Properties["CustomerOrder"] as CustomerOrder;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["CustomerOrder"] = null;
            NavigationService.Navigate(new Order());
        }
    }
}
