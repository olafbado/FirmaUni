using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Data.Data.Sklep
{
    public class Rodzaj
    {
        [Key]
        public int IdRodzaju { get; set; }

        [Required(ErrorMessage = "Wpisz rodzaj towaru")]
        [MaxLength(30, ErrorMessage = "Rodzaj towaru może zawierac max 30 znakow")]
        public required string Nazwa { get; set; }

        public string Opis { get; set; } = string.Empty;

        //to jest obsługa relacji 1 do wielu (Towar ma jeden rodzaj, Rodzaj ma wiele towarow danego rodzaju)
        //po stronie wiele - Rodzaj ma wiele towarow danego rodzaju
        public ICollection<Towar> Towar { get; } = new List<Towar>();
    }
}
