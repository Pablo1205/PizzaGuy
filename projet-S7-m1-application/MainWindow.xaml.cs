using projet_S7_m1_application.Pages;
using projet_S7_m1_application.ViewModels;
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
using System.Windows.Shapes;

namespace projet_S7_m1_application
{
    /// <summary>
    /// Logique d'interaction pour Main.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ShowOrderMenu(object sender, RoutedEventArgs e)
        {
            return;
        }
        private void ShowStatMenu(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void Sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            return;
            //var selected = sidebar.SelectedItem as Path;
            /*
            if(selected.Name == "iconOrder")
            {
                navFrame.Navigate(new Order());
            } else if (selected.Name == "iconStat")
            {
                navFrame.Navigate(new Stat());
            } else if(selected.Name == "iconChat")
            {
                navFrame.Navigate(new Chat());
            } else if (selected.Name == "iconPeople")
            {
                navFrame.Navigate(new People());
            } else if (selected.Name == "iconNewOrder")
            {
                navFrame.Navigate(new NewOrder());
            }
            */
        }
    }
}
