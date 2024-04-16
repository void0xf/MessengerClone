using MessengerClone.DbModels;
using MessengerClone.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClone.Services
{
    class ConversationServices
    {
        private readonly MessengerDbContext _dbContext;
        private readonly UserService _userService;
        public ConversationServices()
        {
            _dbContext = new MessengerDbContext();
            _userService = new UserService(); 
        }
        public int GetConversationIdFromGuestParticipantId(int guestId)
        {
            var currentUserConversations = _userService.GetActiveConversations(UserStore.Instance.CurrentUser);

            var guestUserConversations = _userService.GetActiveConversations(_userService.GetUserFromId(guestId));

            if (currentUserConversations == null || guestUserConversations == null)
            {
                return 0; 
            }

            var commonConversation = currentUserConversations
                .Where(c => guestUserConversations.Any(g => g.ConversationId == c.ConversationId))
                .FirstOrDefault();

            return commonConversation?.ConversationId ?? 0;
        }
        public int CreateConversation(int userId1, int userId2)
        {
                var newConversation = new Conversation
                {
                    IsGroup = false, 
                    Title = "Default",
                    theme = "Default",  
                    emoji = "Default",  
                    Participants = new List<Participant>()
                };

                newConversation.Participants.Add(new Participant { UserId = userId1, Conversation = newConversation, ConversationId = newConversation.ConversationId, nickname="", role="" });
                newConversation.Participants.Add(new Participant { UserId = userId2, Conversation = newConversation , ConversationId = newConversation.ConversationId, nickname="", role="" });

                _dbContext.Conversations.Add(newConversation);
                _dbContext.SaveChanges();

                 

                return newConversation.ConversationId;  
        }

    }
}
