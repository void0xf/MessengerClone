using Microsoft.AspNetCore.SignalR;

namespace SignalRServer.Hubs
{
    public class Message
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int ReplytoMessageID { get; set; }
        public int ConversationID { get; set; }
        public Conversation Conversation { get; set; }
        public DateTime Timestamp { get; set; }

        public ICollection<MessageReadStatus> MessageReadStatuses { get; set; }
        public ICollection<MessageReactionStatus> MessageReactionStatuses { get; set; }
    }

    public class User() {};
    public class MessageReadStatus() { };
    public class MessageReactionStatus() { };
    public class Conversation() { };

    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }


}
