using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerClone.DbModels;

namespace MessengerClone.Services
{
    public class UserService
    {
        private readonly MessengerDbContext _dbContext;
        public UserService()
        {
            _dbContext = new MessengerDbContext();
        }
        public User GetUserFromEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(user => user.Email == email);
        }

        public User GetUserFromId(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.ID == id);
        }
        public List<Participant> GetActiveConversations(User user) 
        {
            var ActiveConversationList = _dbContext.Participants.Where(u => u.UserId == user.ID).ToList();
            Debug.WriteLine(_dbContext.Participants);
            if(ActiveConversationList.Any())
            {
                return ActiveConversationList;
            }
            return null;
        }
        public List<User> GetConversationParcipants(User user)
        {
            List<User> UsersList = new List<User>();  

            var ActiveConversationList = GetActiveConversations(user);
            var ActiveConversationIdsList = new List<int>();
            if(ActiveConversationList == null) return UsersList;
            foreach(var activeConversation in ActiveConversationList)
            {
                ActiveConversationIdsList.Add(activeConversation.ConversationId);
            }

            if(ActiveConversationList.Any()) 
            {
                var Parcipants = _dbContext.Participants.Where(p => ActiveConversationIdsList.Contains(p.ConversationId) && p.UserId != user.ID).ToList();
                foreach(var p in Parcipants)
                {
                    var RetrivedUser = GetUserFromId(p.UserId);
                    if(RetrivedUser != null)
                    {
                        UsersList.Add(RetrivedUser);
                    }
                }
            }
            return UsersList;
        }
        public List<User> SearchForUsers(string SearchTerm)
        {
            return _dbContext.Users.Where(u => u.Name.ToLower().Contains(SearchTerm.ToLower()) || u.Surname.ToLower().Contains(SearchTerm.ToLower())).ToList();
        }
    }
}
