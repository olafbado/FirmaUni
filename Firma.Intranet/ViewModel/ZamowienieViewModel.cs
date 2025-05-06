using System.ComponentModel.DataAnnotations;

namespace Firma.Intranet.Models
{
    public class PozycjaZamowieniaVM
    {
        public int TowarId { get; set; }
        public string TowarNazwa { get; set; } = "";
        public decimal CenaJednostkowa { get; set; }
        public int Ilosc { get; set; }
    }

    public class ZamowienieViewModel
    {
        public int IdZamowienia { get; set; }

        [Required]
        public string UzytkownikId { get; set; } = "";

        public List<PozycjaZamowieniaVM> Pozycje { get; set; } = new();
    }
}
