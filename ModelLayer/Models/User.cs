using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Models
{
    public class User
    {
        public int Id { get; set; } // This should be auto-generated
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
