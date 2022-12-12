using BaseLibrary;
using UdrugeApp.Commons;

namespace UdrugeApp.Domain.Models;
public class VoditeljiUdruge : Entity<int>
{
    private int _IdUdruge;
    private int _IdClan;
    private string _Pozicija;
    private DateTime? _NaPozicijiDo;


    public int IdUdruge { get => _IdUdruge; set => _IdUdruge = value; }
    public int IdClan { get => _IdClan; set => _IdClan = value; }
    public string Pozicija { get => _Pozicija; set => _Pozicija = value; }
    public DateTime? NaPozicijiDo { get => _NaPozicijiDo; set => _NaPozicijiDo = value; }


    public VoditeljiUdruge(int IdUdruge, int idClan, string Pozicija, DateTime? NaPozicijiDo) : base(IdUdruge)
    {
        if (string.IsNullOrEmpty(Pozicija))
        {
            throw new ArgumentException($"'{nameof(Pozicija)}' cannot be null or empty.", nameof(Pozicija));
        }

        if (string.IsNullOrEmpty(IdUdruge.ToString()))
        {
            throw new ArgumentException($"'{nameof(IdUdruge)}' cannot be null or empty.", nameof(IdUdruge));
        }

        if (string.IsNullOrEmpty(IdClan.ToString()))
        {
            throw new ArgumentException($"'{nameof(idClan)}' cannot be null or empty.", nameof(idClan));
        }

        _IdUdruge = IdUdruge;
        _IdClan = idClan;
        _Pozicija = Pozicija;
        _NaPozicijiDo = NaPozicijiDo;

    }

    public override bool Equals(object? obj)
    {
        return obj is not null &&
                obj is VoditeljiUdruge VoditeljiUdruge &&
               _IdUdruge == VoditeljiUdruge._IdUdruge &&
               _IdClan == VoditeljiUdruge._IdClan &&
               _Pozicija == VoditeljiUdruge._Pozicija &&
               _NaPozicijiDo == VoditeljiUdruge._NaPozicijiDo;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_IdUdruge, _IdClan, _Pozicija, _NaPozicijiDo);
    }

    public override Result IsValid()
        => Validation.Validate(
            (() => _Pozicija.Length <= 10, "Pozicija lenght must be less than 10 characters"),
            (() => !string.IsNullOrEmpty(_Pozicija.Trim()), "Pozicija can't be null, empty, or whitespace")
            );

}