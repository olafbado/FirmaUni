using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Firma.Data.Data.Sklep
{
    public class PozycjaKoszyka
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Koszyk")]
        public int KoszykId { get; set; }

        [ForeignKey("Towar")]
        public int TowarId { get; set; }

        public int Ilosc { get; set; }

        public Koszyk? Koszyk { get; set; }
        public Towar? Towar { get; set; }
    }
}
