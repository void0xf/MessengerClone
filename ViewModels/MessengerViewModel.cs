using MessengerClone.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MessengerClone.ViewModels
{
    public class MessengerViewModel: ViewModelBase
    {
        public class User
        {
            public string Name { get; set; }
            public string LastMessage { get; set; }
            public string Photo { get; set; }
        }

        public class Message
        {
            public string Content { get; set; }
            public User Sender { get; set; }
            public DateTime Timestamp { get; set; }
        }
        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
        public ObservableCollection<Message> SelectedMessages { get; set; }
        private User _selectedUser;
        public User SelectedUser { 
            get { return _selectedUser; } 
            set { _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                UpdateMessages();
                }
            }

        public void UpdateMessages()
        {
            SelectedMessages.Clear();

            if (SelectedUser != null && Messages != null)
            {
                var selectedMessages = Messages.Where(msg => msg.Sender == SelectedUser);
                foreach (var message in selectedMessages)
                {
                    SelectedMessages.Add(message);
                }
            }
        }

        public MessengerViewModel(NavigationStore navigationStore, Func<SignUpViewModel> createViewModel)
        {
            Users = new ObservableCollection<User>();
            SelectedMessages = new ObservableCollection<Message>();
            Users.Add(new User {Name="John", LastMessage="LastMessage", Photo="https://xd.com"});
            Users.Add(new User {Name="Anna", LastMessage="LastMessage2", Photo="https://xd.com"});
            SelectedUser = Users[0];
            Messages = new ObservableCollection<Message>();
            Messages.Add(new Message{Content="Hello World", Sender=Users[0], Timestamp=DateTime.Now});
            Messages.Add(new Message{Content="hello world2", Sender=Users[1], Timestamp=DateTime.Today});
        }
}
}
