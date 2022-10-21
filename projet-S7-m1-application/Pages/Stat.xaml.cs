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
    /// Logique d'interaction pour Stat.xaml
    /// </summary>
    public partial class Stat : Page
    {
        List<NumberOfOrdersByWorker> OrdersByClerk = new List<NumberOfOrdersByWorker>();
        List<NumberOfOrdersByWorker> OrdersByDeliverer = new List<NumberOfOrdersByWorker>();
        List<CustomerOrder> OrdersByDateTime = new List<CustomerOrder>();
        public Stat()
        {
            InitializeComponent();

            Database database = new Database();
            MySqlConnection conn = database.conn;

            //get nb of orders by clerk
            string sqlNOOBC = "SELECT Clerk.fname, Clerk.lname ,CustomerOrder.customerOrderID, COUNT(Clerk.fname) AS NOfOrders FROM CustomerOrder JOIN Clerk ON CustomerOrder.idClerk=Clerk.idClerk GROUP BY Clerk.fname, Clerk.lname";
            MySqlCommand cmdNOOBC = new MySqlCommand(sqlNOOBC, conn);
            MySqlDataReader rdrNOOBC = cmdNOOBC.ExecuteReader();


            while (rdrNOOBC.Read())
            {
                this.OrdersByClerk.Add(new NumberOfOrdersByWorker(rdrNOOBC[0].ToString(), rdrNOOBC[1].ToString(), (int)rdrNOOBC[2], int.Parse(rdrNOOBC[3].ToString())));
                //Console.WriteLine(rdrNOOBC[0].ToString());
            }
            noobc.ItemsSource = this.OrdersByClerk;
            rdrNOOBC.Close();

            //get nb of orders by deliverer
            string sqlNOOBD = "SELECT Deliverer.fname, Deliverer.lname ,CustomerOrder.customerOrderID, COUNT(Deliverer.fname) AS NOfOrders FROM CustomerOrder JOIN Deliverer ON CustomerOrder.idDeliverer=Deliverer.idDeliverer GROUP BY Deliverer.fname, Deliverer.lname";
            MySqlCommand cmdNOOBD = new MySqlCommand(sqlNOOBD, conn);
            MySqlDataReader rdrNOOBD = cmdNOOBD.ExecuteReader();


            while (rdrNOOBD.Read())
            {
                this.OrdersByDeliverer.Add(new NumberOfOrdersByWorker(rdrNOOBD[0].ToString(), rdrNOOBD[1].ToString(), (int)rdrNOOBD[2], int.Parse(rdrNOOBD[3].ToString())));
                //Console.WriteLine(rdrNOOBC[0].ToString());
            }
            noobd.ItemsSource = this.OrdersByDeliverer;
            rdrNOOBD.Close();

            //get orders by time period
            /*string sqlOBTP = "SELECT CustomerOrder.orderDate FROM CustomerOrder WHERE orderDate LIKE '% " + 20 + ":__:__'";
            MySqlCommand cmdOBTP = new MySqlCommand(sqlOBTP, conn);
            MySqlDataReader rdrOBTP = cmdOBTP.ExecuteReader();


            while (rdrOBTP.Read())
            {
                this.OrdersByDateTime.Add(new CustomerOrder(rdrOBTP[0].ToString()));
                //Console.WriteLine(rdrNOOBC[0].ToString());
            }
            timegrid.ItemsSource = this.OrdersByDateTime;
            rdrOBTP.Close();*/

            database.CloseConnection();
        }
        private void OnComboBoxChanged(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            int s = 0;
            string selected = (string)comboBox.SelectedValue;
            if (selected != null) selected = selected.Substring(0, 2);
            if (selected != null) s = int.Parse(selected);
            Console.WriteLine(s);
        }
    }
}
