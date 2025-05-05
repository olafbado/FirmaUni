using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Data.Data.Sklep
{
    public class Koszyk
    {
        [Key]
        public int IdKoszyka { get; set; }

        public DateTime DataUtworzenia { get; set; } = DateTime.UtcNow;

        [Required]
        public string UzytkownikId { get; set; } = string.Empty;

        public ICollection<PozycjaKoszyka> Pozycje { get; set; } = new List<PozycjaKoszyka>();

        public Uzytkownik? Uzytkownik { get; set; }
    }
}
