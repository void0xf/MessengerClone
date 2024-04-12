using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClone.DbModels
{
    public class Status
    {
        public int StatusId { get; set; }

        public string StatusInfo { get; set; }

        public DateTime LastOnlineTimestamp { get; set; }

        // Navigation property for users with this status
        public virtual ICollection<User> Users { get; set; }

    }
}
