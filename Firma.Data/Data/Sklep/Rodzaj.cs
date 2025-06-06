﻿using System.ComponentModel.DataAnnotations;

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

        public ICollection<Towar> Towar { get; } = new List<Towar>();
    }
}
