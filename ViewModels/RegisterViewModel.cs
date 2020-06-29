using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Top_lista_vremena.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Korisničko ime")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Lozinka")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Potvrdi lozinku")]
        [Compare("Password",ErrorMessage ="Lozinka i potvrda loznike nisu jednake.")]
        public string ConfirmPassword { get; set; }


    }
}
