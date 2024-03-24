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
		private int _username;
		public int Username
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

		private int _email;
		public int Email
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

        private int _password;
        public int Password
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

        private int _confirmpassword;
        public int ConfirmPassword
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
        ICommand RegisterCommand { get;}  
    }

}
