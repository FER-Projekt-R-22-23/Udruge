using BaseLibrary;

namespace UdrugeApp.Domain.Models;

public class Clanarina : ValueObject
{
    private bool _placenost;
    private decimal _iznos;
    private int _godina;
    private int _clanId;



    public bool Placenost { get => _placenost; set => _placenost = value; }
    public decimal Iznos { get => _iznos; set => _iznos = value; }
    public int Godina { get => _godina; set => _godina = value; }
    public int ClanId { get => _clanId; set => _clanId = value; }

    public Clanarina(bool placenost, decimal iznos, int godina, int clanId)
    {
        _placenost = placenost;
        _iznos = iznos;
        _godina = godina;
        _clanId = clanId;
    }

    public override Result IsValid()
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        return obj is not null &&
               obj is Clanarina clanarina &&
               _placenost == clanarina._placenost &&
               _iznos == clanarina._iznos &&
               _godina == clanarina._godina &&
               _clanId == clanarina._clanId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_placenost, _iznos, _godina, _clanId);
    }
}