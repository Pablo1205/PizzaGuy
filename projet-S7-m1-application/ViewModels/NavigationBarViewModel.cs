using projet_S7_m1_application.Commands;
using projet_S7_m1_application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace projet_S7_m1_application.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateHomeCommand { get; }

        public ICommand NewOrderViewModel { get; }

        public NavigationBarViewModel(NavigationService<HomeViewModel> homeNavigationService)
        {
            //NavigateHomeCommand = new NavigateCommand<HomeViewModel>(homeNavigationService);
        }
    }
}
