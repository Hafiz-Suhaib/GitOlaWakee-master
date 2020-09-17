using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public string NotificationType { get; set; }
        public int NotificationTypeId { get; set; }
        public int UserId { get; set; }
        public string Usertype { get; set; }
        public string NotificationMessage { get; set; }
        public string NotificationSubject { get; set; }
        public bool NotificationSeen { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }

    }
}
