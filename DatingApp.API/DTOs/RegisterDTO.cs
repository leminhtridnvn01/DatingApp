using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace asp_net_core.DatingApp.DatingApp.API.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(32)]
        public string Username {get; set;}
        
        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        public string Password {get; set;}
    }
}