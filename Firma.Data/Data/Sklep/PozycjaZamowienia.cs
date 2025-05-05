using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Data.Data.Sklep
{
    public class PozycjaZamowienia
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Zamowienie")]
        public int ZamowienieId { get; set; }

        [ForeignKey("Towar")]
        public int TowarId { get; set; }

        public int Ilosc { get; set; }

        [Column(TypeName = "money")]
        public decimal CenaJednostkowa { get; set; }

        public Zamowienie? Zamowienie { get; set; }
        public Towar? Towar { get; set; }
    }
}
