using BaseLibrary;
using UdrugeApp.Commons;

namespace UdrugeApp.Domain.Models;
public class VoditeljiUdruge
{
    private int _Id;
    private int _IdClan;
    private string _Pozicija;
    private DateTime? _NaPozicijiDo;


    public int Id { get => _Id; set => _Id = value; }
    public int IdClan { get => _IdClan; set => _IdClan = value; }
    public string Pozicija { get => _Pozicija; set => _Pozicija = value; }
    public DateTime? NaPozicijiDo { get => _NaPozicijiDo; set => _NaPozicijiDo = value; }


    public VoditeljiUdruge(int id, int idClan, string Pozicija, DateTime? NaPozicijiDo)
    {
        if (string.IsNullOrEmpty(Pozicija))
        {
            throw new ArgumentException($"'{nameof(Pozicija)}' cannot be null or empty.", nameof(Pozicija));
        }

        if (string.IsNullOrEmpty(id.ToString()))
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
        }

        if (string.IsNullOrEmpty(IdClan.ToString()))
        {
            throw new ArgumentException($"'{nameof(idClan)}' cannot be null or empty.", nameof(idClan));
        }

        _Id = id;
        _IdClan = idClan;
        _Pozicija = Pozicija;
        _NaPozicijiDo = NaPozicijiDo;

    }

    public override bool Equals(object? obj)
    {
        return obj is not null &&
                obj is VoditeljiUdruge VoditeljiUdruge &&
               _Id == VoditeljiUdruge._Id &&
               _IdClan == VoditeljiUdruge._IdClan &&
               _Pozicija == VoditeljiUdruge._Pozicija &&
               _NaPozicijiDo == VoditeljiUdruge._NaPozicijiDo;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_Id, _IdClan, _Pozicija, _NaPozicijiDo);
    }

    public override Result IsValid()
        => Validation.Validate(
            (() => _Pozicija.Length <= 10, "Pozicija lenght must be less than 10 characters"),
            (() => !string.IsNullOrEmpty(_Pozicija.Trim()), "Pozicija can't be null, empty, or whitespace")
            );

}