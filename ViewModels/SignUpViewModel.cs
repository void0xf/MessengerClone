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
    public class SignUpViewModel : ViewModelBase
    {
		private string _username;
		public string Username
		{
			get
			{
				return _username;
			}
			set
			{
				_username = value;
				OnPropertyChanged(nameof(Username));
			}
		}

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

        private string _confirmpassword;
        public string ConfirmPassword
        {
            get
            {
                return _confirmpassword;
            }
            set
            {
                _confirmpassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        public ICommand SignUpCommand { get;}  
        
        public bool ValidateInput()
        {
            return Password == ConfirmPassword;
        }
        
        public ICommand NavigateToSignInViewModel { get; }
        public SignUpViewModel(NavigationStore navigationStore, Func<SignInViewModel> createViewModel)
        {
           SignUpCommand = new SignUpCommand(this);
           NavigateToSignInViewModel = new NavigateCommand(navigationStore, createViewModel);
        }
    }

}
