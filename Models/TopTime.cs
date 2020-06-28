using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Top_lista_vremena.Models
{
    public class TopTime
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Ime mora biti uneseno"), MaxLength(50, ErrorMessage = "Ime sadrži više od 50 slova"), MinLength(2,ErrorMessage = "Ime sadrži manje od 2 slova")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Prezime mora biti uneseno"), MaxLength(50, ErrorMessage = "Prezime sadrži više od 50 slova"), MinLength(2, ErrorMessage = "Prezime sadrži manje od 2 slova")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Vrijeme mora biti uneseno"), DataType(DataType.Time)]
        public TimeSpan? Time { get; set; }
    }
}
