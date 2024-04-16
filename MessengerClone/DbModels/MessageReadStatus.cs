using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClone.DbModels
{
    public class MessageReadStatus
    {
        public int ID { get; set; }
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public bool IsRead { get; set; }

        public virtual Message Message { get; set; }
        public virtual User User { get; set; }

    }
}
