namespace Firma.Portal.ViewModel;

public class PozycjaKoszykaVM
{
    public int TowarId { get; set; }
    public string TowarNazwa { get; set; } = "";
    public string FotoUrl { get; set; } = "";
    public decimal CenaJednostkowa { get; set; }
    public int Ilosc { get; set; }
}
