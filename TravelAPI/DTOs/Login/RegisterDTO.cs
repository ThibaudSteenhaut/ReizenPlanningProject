using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAPI.DTOs
{
    public class RegisterDTO : LoginDTO
    {
        [Required]
        [StringLength(200)]
        public String FirstName { get; set; }

        [Required]
        [StringLength(250)]
        public String LastName { get; set; }

        [Required]
        [Compare("Password")]
        public String PasswordConfirmation { get; set; }
    }
}
