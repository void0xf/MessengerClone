using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClone.DbModels
{
    public class Participant
    {
        public int Id { get; set; }
        public int ConversationId { get; set; }
        public int UserId { get; set; }

        public string nickname { get; set; }
        public string role { get; set; }

        public User User { get; set; }
        public Conversation Conversation { get; set; }
    }
}
