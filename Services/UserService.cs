using System;
using System.Collections.Generic;
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
    }
}
