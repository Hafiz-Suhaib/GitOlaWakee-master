using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class LawyerTiming
    {
        [Key]
        public int LawyerTimingId { get; set; }
        public string Day { get; set; }
        //slotType InPerson or Virtual
        public string SlotType { get; set; }
        public string AppoinmentFee { get; set; }
        //address id ani ha
        public int? LawyerAddressId { get; set; }
        //public int? LawyerAddress_Id { get; set; }
        public virtual LawyerAddress LawyerAddress { get; set; }
        public string Location { get; set; }
        public float Charges { get; set; }
        //check is used for fees enable disbale in edit mode
        public bool Check { get; set; }
        //check2 is used for location enable disbale in edit mode
        public bool Check2 { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
        public int LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }

    }
}
