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
    public class SignInViewModel : ViewModelBase
    {
		private string _email;
		public string Email
		{
			get
			{
				return _email;
			}
			set
			{
				_email = value;
				OnPropertyChanged(nameof(Email));
			}
		}
		
        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public ICommand SumbitCommand { get; }
        public ICommand NavigateToSignUpCommand { get; }
        public ICommand NavigateToMessengerApp { get;}
        public SignInViewModel(NavigationStore navigationStore, Func<SignUpViewModel> createViewModel)
        {
            Func<MessengerViewModel> app = () => { return new MessengerViewModel(navigationStore, createViewModel); };
            NavigateToSignUpCommand = new NavigateCommand(navigationStore, createViewModel);
            NavigateToMessengerApp = new NavigateCommand(navigationStore, app);
            SumbitCommand = new SignInCommand(this, NavigateToMessengerApp);
        }
    }



}
