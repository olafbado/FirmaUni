using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Data.Data.CMS
{
    public class Strona
    {
        [Key]//to co niżej będzie kluczem podsatwowym tabeli
        public int IdStrony { get; set; }

        [Required(ErrorMessage = "Tytuł odnośnika jest wymagany")]//to co niżej jest wymagane
        [MaxLength(10, ErrorMessage = "Link może zawierac max 10 znakow")]//maksymalny rozmiar
        [Display(Name = "Tytuł odnośnika")]//tak ma nazywać się pole widoczne na widoku
        public required string LinkTytul { get; set; }

        [Required(ErrorMessage = "Tytuł strony jest wymagany")]
        [MaxLength(50, ErrorMessage = "Tytuł może zawierac max 50 znakow")]
        [Display(Name = "Tytuł strony")]
        public required string Tytul { get; set; }

        [Display(Name = "Treść")]
        public required string Tresc { get; set; }

        [Display(Name = "Pozycja wyswietlania")]
        [Required(ErrorMessage = "Wpisz pozycje wyswietlania")]
        public int Pozycja { get; set; }
    }
}
