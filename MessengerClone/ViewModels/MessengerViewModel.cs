using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MessengerClone.Commands;
using MessengerClone.DbModels;
using MessengerClone.Services;
using MessengerClone.Store;
using Microsoft.AspNetCore.SignalR.Client;

namespace MessengerClone.ViewModels
{
    public class MessengerViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _searchPeople;
        private ObservableCollection<User> _sidebarUsersToDisplay;
        private ObservableCollection<Message> _messages;
        private ConversationServices _conversationServices;
        private User _selectedUserFromSidebar;
        private string _messageToSend;
        private UserService _userService;
        private MessageServices _messageServices;
        private SignalRChatService _signalRChatService;
        public ICommand SendMessage {get;}
        public int CurrentUserId => UserStore.Instance.CurrentUser.ID;
        private string _lastMessageText;


        public event PropertyChangedEventHandler PropertyChanged;

        public MessengerViewModel(NavigationStore navigationStore, Func<SignUpViewModel> createViewModel)
        {
            _userService = new UserService();
            _messageServices = new MessageServices();
            _conversationServices = new ConversationServices();   

            _signalRChatService = new SignalRChatService(HubConnectionStore.Instance.Connection);
            _signalRChatService.Connect().ContinueWith(task =>
               {
                   if (task.Exception != null)
                   {
                       Debug.WriteLine( "Unable to connect to color chat hub");
                   }
               });
            _signalRChatService.MessageReceived += ChatServices_MessageReceived;


            UpdateSidebarUsers();
            _selectedUserFromSidebar = _sidebarUsersToDisplay.FirstOrDefault();
            SendMessage = new SendMessageCommand(this, _signalRChatService);
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
        public User SelectedUserFromSidebar
        {
            get { return _selectedUserFromSidebar; }
            set
            {
                if (_selectedUserFromSidebar != value)
                {
                    _selectedUserFromSidebar = value;
                    LoadMessagesForSelectedUser(); // Load messages when a new user is selected
                    OnPropertyChanged(nameof(SelectedUserFromSidebar));
                }
            }
        }
        public string LastMessageText
        {
            get {return _lastMessageText;}
            set {

                }
        }

        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        private void LoadMessagesForSelectedUser()
        {
            if (_selectedUserFromSidebar != null)
            {

                int conversationId = _conversationServices.GetConversationIdFromGuestParticipantId(SelectedUserFromSidebar.ID);
                var messages = _messageServices.GetMessagesForConversation(conversationId);
                Messages = new ObservableCollection<Message>(messages);
            }
            else
            {
                Messages = new ObservableCollection<Message>();
            }
        }

        public string MessageToSend
        {
            get {return _messageToSend;}
            set { _messageToSend = value;
                    OnPropertyChanged(nameof(MessageToSend));}
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
        private void ChatServices_MessageReceived(Message message)
        {
            int conversationId = _conversationServices.GetConversationIdFromGuestParticipantId(SelectedUserFromSidebar.ID);
            var messages = _messageServices.GetMessagesForConversation(conversationId);
            Messages = new ObservableCollection<Message>(messages);

        }
    }
}
