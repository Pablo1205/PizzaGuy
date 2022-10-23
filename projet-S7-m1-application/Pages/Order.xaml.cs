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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace projet_S7_m1_application
{
    /// <summary>
    /// Logique d'interaction pour Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        List<CustomerOrder> listOrder = new List<CustomerOrder>();

        public Order()
        {
            InitializeComponent();

            Database database = new Database();
            MySqlConnection conn = database.conn;

            string sql = "SELECT * FROM CustomerOrder ORDER BY orderDate DESC";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            
            while (rdr.Read())
            {
                this.listOrder.Add(new CustomerOrder((int)rdr[0], rdr[2].ToString(), (int)rdr[1]) { price= (int)rdr[6] });
            }
            allOrders.ItemsSource = this.listOrder;
            rdr.Close();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var CustomerOrderIDSelected = ((Button)sender).Tag;
            CustomerOrder result = this.listOrder.Find(x => x.CustomerOrderID == (int)CustomerOrderIDSelected);
            Application.Current.Properties["CurrentOrder"] = result;
            NavigationService.Navigate(new OrderDetails()); 
        }
    }
}
