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
    /// Logique d'interaction pour UpdateClerk.xaml
    /// </summary>
    public partial class UpdateClerk : Page
    {
        Clerk clerk;
        private string test = "";
        public string name
        {
            get { return test; }
            set { test = value; }
        }
        public UpdateClerk()
        {
            InitializeComponent();
            this.DataContext = this;
            this.clerk = Application.Current.Properties["CurrentClerk"] as Clerk;

            Database database = new Database();
            MySqlConnection conn = database.conn;

            string sql = "SELECT * FROM Clerk WHERE Clerk.idClerk=" + this.clerk.idClerk;
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
            Application.Current.Properties["CustomerClerk"] = null;
            NavigationService.Navigate(new People());
        }
    }
}
