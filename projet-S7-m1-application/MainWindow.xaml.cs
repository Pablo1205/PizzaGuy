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

namespace projet_S7_m1_application
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MessageBroker _messageBroker = new MessageBroker();
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.Properties["messageBroker"] = _messageBroker;

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void LoginButton(object sender, RoutedEventArgs e)
        {
            Main secondWindow = new Main();
            Console.WriteLine(Username.Text);
            if (Username.Text == "cuisine")
            {
                _messageBroker.ListenToCuisineEvent();
            } else if (Username.Text == "livreur")
            {
                _messageBroker.ListenToLivreurEvent();
            }
            

            this.Close();
            secondWindow.Show();
        }
    }
}
