using BaseLibrary;

namespace UdrugeApp.Domain.Models;

public class Clan : ValueObject
{
    
    private string _ime;
    private string _prezime;

    private readonly List<Clanarina> _clanarine;
    
    public string Ime { get => _ime; set => _ime = value; }
    public string Prezime { get => _prezime; set => _prezime = value; }

    public IReadOnlyList<Clanarina> Clanarina => _clanarine.ToList();

    public Clan(string ime, string prezime, IEnumerable<Clanarina>? clanarine = null)
    {
        if (string.IsNullOrEmpty(ime))
        {
            throw new ArgumentException($"'{nameof(ime)}' cannot be null or empty.", nameof(ime));
        }

        if (string.IsNullOrEmpty(prezime))
        {
            throw new ArgumentException($"'{nameof(prezime)}' cannot be null or empty.", nameof(prezime));
        }
        
        _ime = ime;
        _prezime = prezime;
        _clanarine = clanarine?.ToList() ?? new List<Clanarina>();
    }

    public override bool Equals(object? obj)
    {
        return obj is not null &&
                obj is Clan clan &&
                _ime == clan._ime &&
                _prezime == clan._prezime &&
                _clanarine.SequenceEqual(clan._clanarine);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_ime, _prezime, _clanarine);
    }
    
    public override Result IsValid()
    {
        throw new NotImplementedException();
    }
}