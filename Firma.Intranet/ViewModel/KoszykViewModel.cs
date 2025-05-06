using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Firma.Intranet.Models
{
    public class PozycjaKoszykaVM
    {
        public int TowarId { get; set; }
        public string TowarNazwa { get; set; } = "";
        public decimal CenaJednostkowa { get; set; }
        public int Ilosc { get; set; }
    }

    public class KoszykViewModel
    {
        public int IdKoszyka { get; set; }

        [Required]
        public string UzytkownikId { get; set; } = "";

        public List<PozycjaKoszykaVM> Pozycje { get; set; } = new();
    }
}
