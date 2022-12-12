using BaseLibrary;
using UdrugeApp.Commons;

namespace UdrugeApp.Domain.Models;

public class PotrosniResurs : Resurs
{

    private DateTime _rokTrajanja;
    
    public DateTime RokTrajanja
    {
        get => _rokTrajanja;
        set => _rokTrajanja = value;
    }
    
    public PotrosniResurs(int id, string naziv, string? napomena, DateTime datumNabave, Udruge udruga, Prostori prostor, DateTime rokTrajanja) : base(id, naziv, napomena, datumNabave, udruga, prostor)
    {
        if (rokTrajanja == DateTime.MinValue)
        {
            throw new ArgumentException($"'{nameof(rokTrajanja)}' cannot be null or empty.", nameof(rokTrajanja));
        }

        _rokTrajanja = rokTrajanja;
    }
    
    //Kako validateat baznu klasu i ovu klasu nakon?
    // public override Result IsValid()
    //     => Validation.Validate(
    //         (() => _naziv.Length <= 50, "First name lenght must be less than 50 characters"),
    //         (() => _napomena?.Length <= 50, "Napomena lenght must be less than 50 characters"), 
    //         (() => !string.IsNullOrEmpty(_naziv.Trim()), "Naziv can't be null, empty, or whitespace")
    //     );

    //Radi li ovdje da je i resurs i potrosni resurs
    public override bool Equals(object? obj)
    {
        return base.Equals(obj) &&
               obj is PotrosniResurs resurs &&
               _rokTrajanja.Equals(resurs._rokTrajanja);
    }

    //Radi li se ovako hashcode izvedene klase?
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), _rokTrajanja);
    }
    
}