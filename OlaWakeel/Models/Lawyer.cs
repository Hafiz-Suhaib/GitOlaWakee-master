using OlaWakeel.Data.ApplicationUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class Lawyer
    {
        [Key]
        public int LawyerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Contact { get; set; }
        public string FirbaseToken { get; set; }
        public string Cnic { get; set; }
        public string Biography { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public float Rating { get; set; }
        public float VirtualChargesPkr { get; set; }
        public float VirtualChargesUs { get; set; }
        public int TotalExperience { get; set; }
        public string OnlineStatus { get; set; }
        public string ProfilePic { get; set; }
        public string CnicFrontPic { get; set; }
        public string CnicBackPic { get; set; }
        public string RecentDegreePic { get; set; }
        public int AppUserId { get; set; }
        //public int AppoinmentId { get; set; }
        //public virtual Appointment Appointments { get; set; }
        public virtual AppUser AppUser { get; set; }
        public bool ProfileVerified { get; set; }
        public bool Status { get; set; }
        public bool ProfileCompleted { get; set; }
        public List<LawyerQualification> LawyerQualifications { get; set; }
        public List<LawyerTiming> LawerTimings { get; set; }
        public List<LawyerExperience> LawyerExperiences { get; set; }
        public List<LawyerClient> LawyerClients { get; set; }
        //public List<LawyerSpecialization> LawyerSpecializations { get; set; }
        public List<LawyerCaseCategory> LawyerCaseCategories { get; set; }
        public List<LawyerLanguage> lawyerLanguages { get; set; }
        public List<LawyerLicense> LawyerLicenses { get; set; }
        public List<LawyerAddress> LawyerAddresses { get; set; }
        public List<LawyerCertificatePic> LawyerCertificatePics { get; set; }

        
    }
}
