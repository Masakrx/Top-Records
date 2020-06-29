﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Top_lista_vremena.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Korisničko ime mora biti uneseno")]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lozinka mora biti unesena")]
        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Zapamti me")]
        public bool RemeberMe { get; set; }
    }
}