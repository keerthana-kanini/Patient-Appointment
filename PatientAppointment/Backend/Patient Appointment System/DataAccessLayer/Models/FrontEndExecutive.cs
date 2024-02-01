using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class FrontEndExecutive
    {
        [Key]
        
        public int Executive_ID { get; set; }
        public string Executive_Name { get; set; } = string.Empty;
        public string Executive_Email { get; set; } = string.Empty;
        public string Executive_Password { get; set; } = string.Empty;
    }

}
