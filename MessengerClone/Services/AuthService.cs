using MessengerClone.DbModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClone.Services
{
    interface IAuthService
    {
        Task<bool> CreatNewUser(string Email, string Name, string LastName, string password);
        bool CheckCredentials(string Email, string Password);
    }

    public class AuthService : IAuthService
    {
        MessengerDbContext _messengerContext = new MessengerDbContext();
        HashService HashPassword= new HashService();

        public async Task<bool> CreatNewUser(string Email, string Name, string LastName, string password)
        {
            Status NewStatus = new Status()
            {
                StatusInfo = "Offline",
                LastOnlineTimestamp = DateTime.Now,
            };
            var res =_messengerContext.Users.Add(new User
            {
                Email = Email,
                DateOfBirth = DateTime.Now,
                Name = Name,
                Surname = LastName,
                ProfilePic = "Default",
                Status = NewStatus,
                Password = HashPassword.StringToHash(password)
            });;

            await _messengerContext.SaveChangesAsync();

            return true;

        }

        public bool CheckCredentials(string Email, string Password)
        {
            var user = _messengerContext.Users.FirstOrDefault(user => user.Email == Email); 
            if (user == null)
            {
                return false;
            }
            
            return HashPassword.VerifyString(Password, user.Password);
        }
    }
}
