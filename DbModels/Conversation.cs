
namespace MessengerClone.DbModels
{
    public class Conversation
    {
        public int ConversationId { get; set; }

        public string Title { get; set; }

        public bool IsGroup { get; set; }

        public string theme { get; set; }  

        public string emoji { get; set; }

        // Navigation property for participants in the conversation
        public virtual ICollection<Participant> Participants { get; set; }

        // Navigation property for messages in the conversation
        public virtual ICollection<Message> Messages { get; set; }
    }
}
