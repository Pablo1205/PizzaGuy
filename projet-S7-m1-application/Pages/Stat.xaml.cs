using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities;
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
        private int test = 0;
        public int avgPrice 
        { 
            get { return test; } 
            set { test = value; }
        }   

        public Stat()
        {
            InitializeComponent();
            timegrid.ItemsSource = this.OrdersByDateTime;
            this.DataContext = this;
            Database database = new Database();
            MySqlConnection conn = database.conn;

            //get nb of orders by clerk
            string sqlNOOBC = "SELECT Clerk.fname, Clerk.lname ,CustomerOrder.customerOrderID, COUNT(Clerk.fname) AS NOfOrders FROM CustomerOrder JOIN Clerk ON CustomerOrder.idClerk=Clerk.idClerk GROUP BY Clerk.fname, Clerk.lname";
            MySqlCommand cmdNOOBC = new MySqlCommand(sqlNOOBC, conn);
            MySqlDataReader rdrNOOBC = cmdNOOBC.ExecuteReader();


            while (rdrNOOBC.Read())
            {
                this.OrdersByClerk.Add(new NumberOfOrdersByWorker(rdrNOOBC[0].ToString(), rdrNOOBC[1].ToString(), (int)rdrNOOBC[2], int.Parse(rdrNOOBC[3].ToString())));
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
            }
            noobd.ItemsSource = this.OrdersByDeliverer;
            rdrNOOBD.Close();

            //show average order prices
            string sqlAOP = "SELECT CustomerOrder.price FROM CustomerOrder";
            MySqlCommand cmdAOP = new MySqlCommand(sqlAOP, conn);
            MySqlDataReader rdrAOP = cmdAOP.ExecuteReader();

            int nb = 0;
            int i = 0;
            while (rdrAOP.Read())
            {
                nb += (int)rdrAOP[0];
                i++;
            }
            this.avgPrice = nb / i; 
            rdrAOP.Close();



            database.CloseConnection();
        }

        //show orders by time period
        private void OnComboBoxChanged(object sender, RoutedEventArgs e)
        {
            
            ComboBox comboBox = sender as ComboBox;
            int s = 0;
            string selected = (string)comboBox.SelectedValue;
            if (selected != null)
            {
                this.OrdersByDateTime.Clear();
                selected = selected.Substring(0, 2);
                s = int.Parse(selected);
                //Console.WriteLine(s);

                Database database = new Database();
                MySqlConnection conn = database.conn;

                string sqlOBTP = "SELECT * FROM CustomerOrder WHERE orderDate LIKE '% " + s + ":__:__'";
                MySqlCommand cmdOBTP = new MySqlCommand(sqlOBTP, conn);
                MySqlDataReader rdrOBTP = cmdOBTP.ExecuteReader();

                while (rdrOBTP.Read())
                {
                    this.OrdersByDateTime.Add(new CustomerOrder((int)rdrOBTP[0], (int)rdrOBTP[1], rdrOBTP[2].ToString(), rdrOBTP[3].ToString(), (int)rdrOBTP[4], (int)rdrOBTP[5], (int)rdrOBTP[6]));
   
                }
                timegrid.Items.Refresh();
                rdrOBTP.Close();

                database.CloseConnection();
            }
        }
    }
}
