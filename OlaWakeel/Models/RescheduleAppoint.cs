using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class RescheduleAppoint
    {
        [Key]
        public int RescheduleAppoint_Id { get; set; }
        public int AppointmentId { get; set; }
        public DateTime RescheduleDate { get; set; }
        public string TimeTo { get; set; }
        public string TimeFrom { get; set; }
        public float CaseCharges { get; set; }
    }
}
