using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Top_lista_vremena.Models
{
    public class Role : IdentityRole
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
