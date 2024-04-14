using MessengerClone.Services;
using MessengerClone.Store;
using MessengerClone.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MessengerClone.Commands;
public class SignInCommand : CommandBase
{
    private readonly SignInViewModel _viewSignInModel;
    private readonly AuthService _authService;
    private readonly UserService _userService;
    private readonly ICommand _navigateToApp;
    
    public SignInCommand(SignInViewModel signInViewModel, ICommand NavigateToApp)
    {
        _viewSignInModel = signInViewModel;
        _authService = new AuthService();
        signInViewModel.PropertyChanged += OnViewModelPropertyChagned;
        _navigateToApp = NavigateToApp;
        _userService = new UserService();
    }


    private void OnViewModelPropertyChagned(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SignInViewModel.Email) ||
            e.PropertyName == nameof(SignInViewModel.Password)
            )
        {
            OnCanExecuteChanged();
        }
    }

    public override bool CanExecute(object parameter)
    {
        return !string.IsNullOrEmpty(_viewSignInModel.Email) && !string.IsNullOrEmpty(_viewSignInModel.Password);
    }
    
    public async override void Execute(object parameter)
    {
        bool isUserValid = _authService.CheckCredentials(_viewSignInModel.Email, _viewSignInModel.Password);
        if (isUserValid)
        {
            UserStore.Instance.CurrentUser = _userService.GetUserFromEmail(_viewSignInModel.Email);  
            _navigateToApp.Execute(null);
        }
    }
}

