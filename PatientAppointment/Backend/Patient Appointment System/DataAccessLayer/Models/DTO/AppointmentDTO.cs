using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.DTO
{
    public class AppointmentDTO
    {
        public int Patient_ID { get; set; }
        public int Doctor_ID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public String AppointmentTime { get; set; } = string.Empty;
    }
}
