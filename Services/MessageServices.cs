using MessengerClone.Commands;
using MessengerClone.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClone.Services
{
    class MessageServices
    {
        private readonly MessengerDbContext _dbContext;
        public MessageServices()
        { 
            _dbContext = new MessengerDbContext();
        }

        public bool SaveMessageInDb(string messageContent, int conversationID, int SenderID, int ReplyToMessageID=0)
        {
            _dbContext?.Messages.Add(new Message()
            {
                Content = messageContent,
                ConversationID = conversationID,
                SenderId = SenderID,
                ReplytoMessageID = ReplyToMessageID,
                Timestamp = DateTime.Now,
            });
            var res = _dbContext?.SaveChanges();
            return res > 0;
        }
        public List<Message> GetMessagesForConversation(int conversationId)
        {
            // Retrieve all messages for the specified conversation ID
            var messages = _dbContext.Messages
                                      .Where(m => m.ConversationID == conversationId)
                                      .ToList();

            return messages;
        }
    }
}
