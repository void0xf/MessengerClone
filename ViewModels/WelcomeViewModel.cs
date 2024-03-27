using MessengerClone.Commands;
using MessengerClone.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MessengerClone.ViewModels
{
    public class WelcomeViewModel : ViewModelBase   
    {
        public ICommand SwitchToSignUpCommand { get; }
        public ICommand SwitchToSignInCommand { get; }

        public WelcomeViewModel(NavigationStore navigationStore, Func<SignUpViewModel> createSingUpViewModel, Func<SignInViewModel> createSignInViewModel)
        {
           SwitchToSignUpCommand = new NavigateCommand(navigationStore, createSingUpViewModel);
           SwitchToSignInCommand = new NavigateCommand(navigationStore, createSignInViewModel);
        }
    }
}
