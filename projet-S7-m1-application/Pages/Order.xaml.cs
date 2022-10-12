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

        public Order()
        {
            InitializeComponent();
            Database database = new Database();
            MySqlConnection conn = database.conn;

            string sql = "SELECT * FROM CustomerOrder";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            List<CustomerOrder> listOrder = new List<CustomerOrder>();
            while (rdr.Read())
            {
                // create var customerOrder
                // List<Pizza> pizzaOrder = new List<Pizza>();
                //List<Drink> drinkOrder = new List<Drink>();
             
                listOrder.Add(new CustomerOrder((int)rdr[0], rdr[2].ToString(), (int)rdr[1]));
                
                //this.id = rdr[1].ToString();
                //this.customerOrderID = rdr[2].ToString();
                // this.quantity = rdr[3].ToString();

                // Order order = new Order((int)rdr[0], rdr[1].ToString(), (int)rdr[2]);
                // listOrder.Add(order);


            }
            allOrders.ItemsSource = listOrder;
            rdr.Close();
        }
    }
}
