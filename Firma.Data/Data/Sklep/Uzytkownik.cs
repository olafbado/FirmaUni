using System.ComponentModel.DataAnnotations;

namespace Firma.Data.Data.Sklep;

public class Uzytkownik
{
    [Key]
    public string UzytkownikId { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MaxLength(100)]
    public string Imie { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Nazwisko { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public ICollection<Koszyk> Koszyki { get; set; } = new List<Koszyk>();
    public ICollection<Zamowienie> Zamowienia { get; set; } = new List<Zamowienie>();
}
