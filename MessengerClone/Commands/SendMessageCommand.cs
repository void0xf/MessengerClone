using MessengerClone.DbModels;
using MessengerClone.Services;
using MessengerClone.Store;
using MessengerClone.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClone.Commands
{
    class SendMessageCommand : CommandBase
    {
        private readonly MessengerViewModel _viewModel;
        private readonly MessageServices _messageServices;
        private readonly ConversationServices _conversationServices;
        private readonly SignalRChatService _signalRChatService;
        public SendMessageCommand(MessengerViewModel vm, SignalRChatService chat) 
        { 
            _viewModel = vm;
            _viewModel.PropertyChanged += OnViewModelPropertyChagned;
            _messageServices = new MessageServices();
            _conversationServices = new ConversationServices();
            _signalRChatService = chat;

        } 


        public async override void Execute(object parameter)
        {
            var ChatingUser = _viewModel.SelectedUserFromSidebar;

            int conversationId = _conversationServices.GetConversationIdFromGuestParticipantId(ChatingUser.ID);
            if (conversationId == 0)
            {
                conversationId = _conversationServices.CreateConversation(UserStore.Instance.CurrentUser.ID, ChatingUser.ID);
            }
            var message = new Message()
            {
                Content = _viewModel.MessageToSend,
                ConversationID = conversationId,
                SenderId = UserStore.Instance.CurrentUser.ID,
                ReplytoMessageID = 0,
                Timestamp = DateTime.UtcNow,
            };
            _messageServices.SaveMessageInDb(_viewModel.MessageToSend, conversationId, UserStore.Instance.CurrentUser.ID);
            await _signalRChatService.SendMessage(message);
        }

        private void OnViewModelPropertyChagned(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MessengerViewModel.MessageToSend)
                )
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.MessageToSend);
        }
    }
}
