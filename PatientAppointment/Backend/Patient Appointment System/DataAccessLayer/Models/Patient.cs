using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Patients
    {
        public int Patient_ID { get; set; }
        public string Patient_Name { get; set; } = string.Empty;
        public string Patient_Gender { get; set; } = string.Empty;
        public DateTime Patient_DateOfBirth { get; set; }
        public string Patient_Email { get; set; } = string.Empty;
        public string Patient_Phone { get; set; } = string.Empty;
        public string Patient_Location { get; set; } = string.Empty;
        public string Patient_Password { get; set; } = string.Empty;
        public string Patient_Status { get; set; } = "pending";
        public bool IsFirstLogin { get; set; }
    }
}
