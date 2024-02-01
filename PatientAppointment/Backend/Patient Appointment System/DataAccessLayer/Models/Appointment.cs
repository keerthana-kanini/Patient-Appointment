using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Appointments
    {
        public int Appointment_ID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public String AppointmentTime { get; set; } = string.Empty;
        public string Status { get; set; } = "pending";

        public Doctors Doctor { get; set; }
        public Patients Patient { get; set; }
    }
}
