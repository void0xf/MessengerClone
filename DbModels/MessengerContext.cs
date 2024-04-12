using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MessengerClone.DbModels
{
    class MessengerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<MessageReadStatus> MessageReadStatus { get; set; }
        public DbSet<MessageReactionStatus> MessageReactionStatus { get; set; } 
        
        public string TargetPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\MessengerApp";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!Directory.Exists(TargetPath)) 
            {
                Directory.CreateDirectory(TargetPath);
            }
            optionsBuilder.UseSqlite($"Data Source={TargetPath}\\Messenger.db");


        }
    }
}
