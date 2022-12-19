namespace UdrugeApp.Domain.Models;

public class TrajniResurs : Resurs
{

    private string _inventarniBroj;
    private bool _jeDostupno;
    
    public string InventarniBroj
    {
        get => _inventarniBroj;
        set => _inventarniBroj = value;
    }

    public bool JeDostupno
    {
        get => _jeDostupno;
        set => _jeDostupno = value;
    }
    
    public TrajniResurs(int id, string naziv, string? napomena, DateTime datumNabave, Udruge udruga, Prostori prostor, string inventarniBroj, bool jeDostupno) : base(id, naziv, napomena, datumNabave, udruga, prostor)
    {
        if (string.IsNullOrEmpty(naziv))
        {
            throw new ArgumentException($"'{nameof(naziv)}' cannot be null or empty.", nameof(naziv));
        }

        _inventarniBroj = inventarniBroj;
        _jeDostupno = jeDostupno;
    }
    
    //Radi li ovdje da je i resurs i potrosni resurs
    public override bool Equals(object? obj)
    {
        return base.Equals(obj) &&
               obj is TrajniResurs resurs &&
               _inventarniBroj.Equals(resurs._inventarniBroj) &&
               _jeDostupno == resurs._jeDostupno;
    }

    //Radi li se ovako hashcode izvedene klase?
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), _inventarniBroj, _jeDostupno);
    }
}