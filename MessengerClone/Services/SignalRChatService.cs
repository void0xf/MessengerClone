using MessengerClone.DbModels;
using MessengerClone.Store;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClone.Services
{
    public class SignalRChatService
    {
        private readonly HubConnection _connection;

        public event Action<Message> MessageReceived;

        public SignalRChatService(HubConnection connection)
        {
            _connection = HubConnectionStore.Instance.Connection;

            _connection.On<Message>("ReceiveMessage", (mess) => MessageReceived?.Invoke(mess));
        }

        public async Task Connect()
        {
            await _connection.StartAsync();
        }

        public async Task SendMessage(Message color)
        {
            await _connection.SendAsync("SendMessage", color);
        }
    }
}
