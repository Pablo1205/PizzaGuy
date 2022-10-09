using projet_S7_m1_application.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projet_S7_m1_application.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ICommand NavigateCommand { get; }
        public HomeViewModel(NavigationStore navigationStore)
        {
            return;
        }
    }
}
