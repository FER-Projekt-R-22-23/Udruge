public class ZahtjevDetails
{
    public int IdZahtjev { get; set; }
    public int IdMaterijalnaPotreba { get; set; }
    public string NazivMaterijalnaPotreba { get; set; } = String.Empty;
    public double Kolicina { get; set; }
    public string MjernaJedinica { get; set; } = String.Empty;
    public string Kooridinate { get; set; }
    public int Organizator { get; set; }
}