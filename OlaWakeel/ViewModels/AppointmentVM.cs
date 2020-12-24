using OlaWakeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.ViewModels
{
    public class AppointmentVM:Lawyer
    {
        public Lawyer LawyerList { get; set; }
        public List<Appointment> AppointmentList { get; set; }
      
    }
}
