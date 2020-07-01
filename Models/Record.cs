using System;
using System.ComponentModel.DataAnnotations;
using Top_lista_vremena.Utilities;

namespace Top_lista_vremena.Models
{
    public class Record
    {
        public int? ID { get; set; }

        [Required(ErrorMessage = "Ime je obavezno polje"), MaxLength(50, ErrorMessage = "Ime sadrži više od 50 slova"), MinLength(2, ErrorMessage = "Ime sadrži manje od 2 slova")]
        [Display(Name = "Ime")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno polje"), MaxLength(50, ErrorMessage = "Prezime sadrži više od 50 slova"), MinLength(2, ErrorMessage = "Prezime sadrži manje od 2 slova")]
        [Display(Name = "Prezime")]
        public string Surname { get; set; }

        [Required(ErrorMessage ="Email je obavezno polje"), DataType(DataType.EmailAddress)]
        [EmailAddress]
        //[ValidEmailDomainAtrribute(allowedDomain: "google.com",ErrorMessage ="Email nije u traženoj domeni")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vrijeme je obavezno polje"), DataType(DataType.Time)]
        [Display(Name = "Vrijeme")]
        public TimeSpan Time { get; set; }

        public bool Approved { get; set; }
    }
}
