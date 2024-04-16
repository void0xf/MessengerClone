using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerClone.DbModels;

namespace MessengerClone.Store
{
    public class UserStore
    {
        private static readonly UserStore _instance = new UserStore();
        public static UserStore Instance => _instance;
        private User _currentUser;

        public User CurrentUser { get { return _currentUser; } set { _currentUser = value;} }
        
        private UserStore() { }
    }
}
