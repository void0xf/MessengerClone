using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClone.DbModels
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
}
