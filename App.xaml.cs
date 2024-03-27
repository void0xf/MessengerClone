using System.Configuration;
using System.Data;
using System.Windows;
using MessengerClone.Store;
using MessengerClone.ViewModels;

namespace MessengerClone
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
        }


        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = new WelcomeViewModel(_navigationStore, CreateSignUpViewModel, CreateSignInViewModel); 
            
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
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
