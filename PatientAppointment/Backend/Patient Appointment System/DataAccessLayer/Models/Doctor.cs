using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Doctors
    {
        [Key]
        public int Doctor_ID { get; set; }
        public string Doctor_Name { get; set; } = string.Empty;
        public string Doctor_Gender { get; set; } = string.Empty;
        public DateTime Doctor_DateOfBirth { get; set; }
        public string Doctor_Email { get; set; } = string.Empty;
        public string Doctor_Phone { get; set; } = string.Empty;
        public string Doctor_Location { get; set; } = string.Empty;
        public string Doctor_Specialization { get; set; } = string.Empty;
        public string Doctor_Password { get; set; } = string.Empty;
        public string Doctor_Status { get; set; } = "pending";

    }
}
