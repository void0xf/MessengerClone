using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MessengerClone.DbModels;
using MessengerClone.Services;
using MessengerClone.Store;

namespace MessengerClone.ViewModels
{
    public class MessengerViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _searchPeople;
        private ObservableCollection<User> _sidebarUsersToDisplay;
        private User _selectedUserFromSidebar;
        private UserService _userService;

        public event PropertyChangedEventHandler PropertyChanged;

        public MessengerViewModel(NavigationStore navigationStore, Func<SignUpViewModel> createViewModel)
        {
            _userService = new UserService();
            UpdateSidebarUsers();
            _selectedUserFromSidebar = _sidebarUsersToDisplay.FirstOrDefault();
        }

        public string SearchPeople
        {
            get { return _searchPeople; }
            set
            {
                if (_searchPeople != value)
                {
                    _searchPeople = value;
                    OnPropertyChanged(nameof(SearchPeople));
                    UpdateSidebarUsers();
                }
            }
        }
        public User SelectedUserFromSiebar
        {
            get { return _selectedUserFromSidebar; }
            set
            {
                _selectedUserFromSidebar = value;
                OnPropertyChanged(nameof(SearchPeople));
            }
        }

        public ObservableCollection<User> SidebarUsersToDisplay
        {
            get { return _sidebarUsersToDisplay; }
            set
            {
                if (_sidebarUsersToDisplay != value)
                {
                    _sidebarUsersToDisplay = value;
                    OnPropertyChanged(nameof(SidebarUsersToDisplay));
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateSidebarUsers()
        {
            if (!string.IsNullOrEmpty(SearchPeople))
            {
                SidebarUsersToDisplay = new ObservableCollection<User>(_userService.SearchForUsers(SearchPeople));
            }
            else
            {
                var ActiveConversationsParcipants = _userService.GetConversationParcipants(UserStore.Instance.CurrentUser);
                SidebarUsersToDisplay = new ObservableCollection<User>(ActiveConversationsParcipants);
            }
        }
    }
}
