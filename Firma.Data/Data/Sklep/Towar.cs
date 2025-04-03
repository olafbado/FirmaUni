using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Data.Data.Sklep
{
    public class Towar
    {
        [Key]
        public int IdTowaru { get; set; }

        [Required(ErrorMessage = "Wpisz kod")]
        public required string Kod { get; set; }

        [Required(ErrorMessage = "Wpisz nazwę towaru")]
        public required string Nazwa { get; set; }

        [Required(ErrorMessage = "Wpisz cenę towaru")]
        [Column(TypeName = "money")]
        public decimal Cena { get; set; }

        [Required(ErrorMessage = "Dodaj zdjecie")]
        [Display(Name = "Wybierz zdjecie")]
        public required string FotoUrl { get; set; }

        public string Opis { get; set; } = string.Empty;

        //to jest obsługa relacji 1 do wielu (Towar ma jeden rodzaj, Rodzaj ma wiele towarow danego rodzaju)
        //kod po stronie jeden - Towar ma jeden rodzaj
        [ForeignKey("Rodzaj")]
        public int IdRodzaju { get; set; }
        public Rodzaj? Rodzaj { get; set; }
    }
}
