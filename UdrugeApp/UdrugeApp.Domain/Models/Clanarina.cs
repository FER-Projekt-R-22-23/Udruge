using BaseLibrary;

namespace UdrugeApp.Domain.Models;

public class Clanarina : ValueObject
{
    private decimal _iznos;
    private int _godina;
    
    public decimal Iznos { get => _iznos; set => _iznos = value; }
    public int Godina { get => _godina; set => _godina = value; }

    public Clanarina(decimal iznos, int godina)
    {
        _iznos = iznos;
        _godina = godina;
    }

    public override Result IsValid()
    {
        return _iznos > 0
            ? Results.OnSuccess()
            : Results.OnFailure();
    }

    public override bool Equals(object? obj)
    {
        return obj is not null &&
               obj is Clanarina clanarina &&
               _iznos == clanarina._iznos &&
               _godina == clanarina._godina;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_iznos, _godina);
    }
}