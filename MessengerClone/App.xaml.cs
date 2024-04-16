using System.Configuration;
using System.Data;
using System.Windows;
using MessengerClone.DbModels;
using MessengerClone.Store;
using MessengerClone.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;

namespace MessengerClone
{
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            //HubConnection connection = new HubConnectionBuilder().WithUrl("http://localhost:5045").Build();

            _navigationStore.CurrentViewModel = new WelcomeViewModel(_navigationStore, CreateSignUpViewModel, CreateSignInViewModel); 
            
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);

            using (var context = new MessengerDbContext())
            {
                context.Database.EnsureCreated();
            }
        }

        private SignUpViewModel CreateSignUpViewModel()
        {
            return new SignUpViewModel(_navigationStore, CreateSignInViewModel);
        }
        private SignInViewModel CreateSignInViewModel() 
        {
            return new SignInViewModel(_navigationStore, CreateSignUpViewModel);
        }
    }

}
