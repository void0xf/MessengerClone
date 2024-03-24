using MessengerClone.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MessengerClone.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainViewModel()
        {
            CurrentViewModel = new WelcomeViewModel();
        }
    }
}
