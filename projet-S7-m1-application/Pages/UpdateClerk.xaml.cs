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
            getClerkInfo(); 
        }

        private void ConfirmClerk(object sender, RoutedEventArgs e)
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

            this.clerk = Application.Current.Properties["CurrentClerk"] as Clerk;

            Database database = new Database();
            MySqlConnection conn = database.conn;

            string sql = "UPDATE Clerk SET Clerk.fname='" + FirstName + "' , Clerk.lname='" + LastName + "'  WHERE Clerk.idClerk=" + this.clerk.idClerk;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            Application.Current.Properties["CurrentClerk"] = null;
            NavigationService.Navigate(new People());
        }

        private void getClerkInfo()
        {
            Clerk clerk = Application.Current.Properties["CurrentClerk"] as Clerk;
            this.clerk = clerk;
            LastNameInput.Text = this.clerk.GetLastName();
            FirstNameInput.Text = this.clerk.GetFirstName();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Application.Current.Properties["ClerkClerk"] = null;
            NavigationService.Navigate(new People());
        }
    }
}
