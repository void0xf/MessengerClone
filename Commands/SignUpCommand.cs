using MessengerClone.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClone.Commands
{
    class SignUpCommand : CommandBase
    {
   
        private readonly SignUpViewModel _signUpviewModel;
        public SignUpCommand(SignUpViewModel signUpViewModel) 
        {
            if(signUpViewModel != null) 
            { 
                _signUpviewModel = signUpViewModel;
                signUpViewModel.PropertyChanged += OnViewModelPropertyChagned;

            }
        }

        private void OnViewModelPropertyChagned(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SignUpViewModel.Email) ||
                e.PropertyName == nameof(SignUpViewModel.Username) ||
                e.PropertyName == nameof(SignUpViewModel.Password) ||
                e.PropertyName == nameof(SignUpViewModel.ConfirmPassword)
                
                )
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return 
                !string.IsNullOrEmpty(_signUpviewModel.Email) && 
                !string.IsNullOrEmpty(_signUpviewModel.Username) &&
                !string.IsNullOrEmpty(_signUpviewModel.Password) &&
                !string.IsNullOrEmpty(_signUpviewModel.ConfirmPassword) &&
                base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            Debug.WriteLine(nameof(SignUpViewModel.Email));
            //SomeStuff to Sign Up User
        }

    }
}
