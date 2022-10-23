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
            getDelivererInfo();
        }

        private void ConfirmDeliverer(object sender, RoutedEventArgs e)
        {
            string LastName = LastNameInput.Text;
            string FirstName = FirstNameInput.Text;

            if (LastName.Length < 2)
            {
                TextError.Text = "Last Name is incorrect";
                return;
            }
            else if (FirstName.Length < 2)
            {
                TextError.Text = "First Name is incorrect";
                return;
            }
            TextError.Text = "";

            this.deliverer = Application.Current.Properties["CurrentDeliverer"] as Deliverer;

            Database database = new Database();
            MySqlConnection conn = database.conn;

            string sql = "UPDATE Deliverer SET Deliverer.fname='" + FirstName + "' , Deliverer.lname='" + LastName + "'  WHERE Deliverer.idDeliverer=" + this.deliverer.idDeliverer;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            Application.Current.Properties["CurrentDeliverer"] = null;
            NavigationService.Navigate(new People());
        }

        private void getDelivererInfo()
        {
            Deliverer deliverer = Application.Current.Properties["CurrentDeliverer"] as Deliverer;
            this.deliverer = deliverer;
            LastNameInput.Text = this.deliverer.GetLastName();
            FirstNameInput.Text = this.deliverer.GetFirstName();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["CustomerCustomer"] = null;
            NavigationService.Navigate(new People());
        }
    }
}
