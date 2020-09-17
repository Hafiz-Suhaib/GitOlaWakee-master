using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class Appointment
    {
        [Key]
        public int AppoinmentId { get; set; }
        public string AppointmentCode { get; set; }
        public int? LawyerAddressId { get; set; }
        public virtual LawyerAddress LawyerAddress { get; set; }

        public int CaseCategoryId { get; set; }
        public virtual CaseCategory CaseCategory { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public float CaseCharges { get; set; }
        public DateTime ScheduleDate { get; set; }
        public float Rating { get; set; }
        public string AppoinmentType { get; set; }
        public string AppoinmentStatus { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
    }
}
