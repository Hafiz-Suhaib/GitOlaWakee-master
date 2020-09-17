using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class Log
    {
        [Key]
        public int Log_Id { get; set; }
        public int Appointment_Id { get; set; }
        public virtual Appointment Appointment { get; set; }
        public int User_id { get; set; }
        public string User_Type { get; set; }
        public string Log_Decs { get; set; }
        public string Log_Status { get; set; }
        public DateTime LogDate { get; set; }
        public bool Status { get; set; }

    }
}
