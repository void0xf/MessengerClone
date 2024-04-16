using MessengerClone.Store;
using MessengerClone.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MessengerClone.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChange += OnCurrentViewModelChange;
        }

        private void OnCurrentViewModelChange()
        {
            OnPropertyChanged(nameof(CurrentViewModel));    
        }

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
    }
}
