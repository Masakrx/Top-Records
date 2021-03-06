﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Top_Records.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Korisničko ime je obavezno polje"), MaxLength(50, ErrorMessage = "Korisničko ime sadrži više od 50 znakova"), MinLength(2, ErrorMessage = "Korisničko ime sadrži manje od 2 znaka")]
        [Display(Name = "Korisničko ime")]
        [Remote(action:"IsUsernameInUse", controller: "Account")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezno polje"), MaxLength(24, ErrorMessage = "Lozinka sadrži više od 24 znaka"), MinLength(6, ErrorMessage = "Lozinka sadrži manje od 6 znakova")]
        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Unesene lozinke se ne podudaraju"), MaxLength(24, ErrorMessage = "Lozinka sadrži više od 24 znaka"), MinLength(6, ErrorMessage = "Lozinka sadrži manje od 6 znakova")]
        [DataType(DataType.Password)]        
        [Display(Name ="Potvrdi lozinku")]
        [Compare("Password",ErrorMessage = "Unesene lozinke se ne podudaraju")]
        public string ConfirmPassword { get; set; }


    }
}
