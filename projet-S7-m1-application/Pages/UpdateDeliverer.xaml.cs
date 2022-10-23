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
    /// Logique d'interaction pour UpdateDeliverer.xaml
    /// </summary>
    public partial class UpdateDeliverer : Page
    {
        Deliverer deliverer;
        private string test = "";
        public string name
        {
            get { return test; }
            set { test = value; }
        }
        public UpdateDeliverer()
        {
            InitializeComponent();
            this.DataContext = this;
            this.deliverer = Application.Current.Properties["CurrentDeliverer"] as Deliverer;

            Database database = new Database();
            MySqlConnection conn = database.conn;

            string sql = "SELECT * FROM Deliverer WHERE Deliverer.idDeliverer=" + this.deliverer.idDeliverer;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                this.name = rdr[1].ToString() + " " + rdr[2].ToString();
            }
            rdr.Close();

            database.CloseConnection();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["CustomerCustomer"] = null;
            NavigationService.Navigate(new People());
        }
    }
}
