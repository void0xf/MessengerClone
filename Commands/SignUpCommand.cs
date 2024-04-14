using MessengerClone.Services;
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
        private readonly AuthService _authservice;
        public SignUpCommand(SignUpViewModel signUpViewModel) 
        {
            if(signUpViewModel != null) 
            { 
                _signUpviewModel = signUpViewModel;
                signUpViewModel.PropertyChanged += OnViewModelPropertyChagned;
                _authservice = new AuthService();

            }
        }

        private void OnViewModelPropertyChagned(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(SignUpViewModel.Name) ||
                e.PropertyName == nameof(SignUpViewModel.LastName) ||
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
                !string.IsNullOrEmpty(_signUpviewModel.Name) && 
                !string.IsNullOrEmpty(_signUpviewModel.LastName) &&
                !string.IsNullOrEmpty(_signUpviewModel.Password) &&
                !string.IsNullOrEmpty(_signUpviewModel.ConfirmPassword) &&
                _signUpviewModel.Password == _signUpviewModel.ConfirmPassword &&
                base.CanExecute(parameter);
        }

        public async override void Execute(object parameter)
        {
            var res = await _authservice.CreatNewUser(_signUpviewModel.Email, _signUpviewModel.Name, _signUpviewModel.LastName, _signUpviewModel.Password);
        }

    }
}
