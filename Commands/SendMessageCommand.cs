﻿using MessengerClone.Services;
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
        public SendMessageCommand(MessengerViewModel vm) 
        { 
            _viewModel = vm;
            _viewModel.PropertyChanged += OnViewModelPropertyChagned;
            _messageServices = new MessageServices();
            _conversationServices = new ConversationServices();
        } 


        public override void Execute(object parameter)
        {
            var ChatingUser = _viewModel.SelectedUserFromSidebar;

            int conversationId = _conversationServices.GetConversationIdFromGuestParticipantId(ChatingUser.ID);
            if (conversationId == 0)
            {
                conversationId = _conversationServices.CreateConversation(UserStore.Instance.CurrentUser.ID, ChatingUser.ID);
            }
            _messageServices.SaveMessageInDb(_viewModel.MessageToSend, conversationId, UserStore.Instance.CurrentUser.ID);
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
