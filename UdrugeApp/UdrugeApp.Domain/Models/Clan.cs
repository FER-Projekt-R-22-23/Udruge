using BaseLibrary;

namespace UdrugeApp.Domain.Models;

public class Clan : ValueObject
{

    private int _id;
    private string _ime;
    private string _prezime;
    private string? _rang;

    private readonly List<Clanarina>? _clanarine;
    
    public int Id { get => _id; set => _id = value; }
    public string Ime { get => _ime; set => _ime = value; }
    public string Prezime { get => _prezime; set => _prezime = value; }
    public string Rang { get => _rang; set => _rang = value; }

    public IReadOnlyList<Clanarina> Clanarina => _clanarine.ToList();

    private Clan(int id, string ime, string prezime)
    {
        if (string.IsNullOrEmpty(ime))
        {
            throw new ArgumentException($"'{nameof(ime)}' cannot be null or empty.", nameof(ime));
        }

        if (string.IsNullOrEmpty(prezime))
        {
            throw new ArgumentException($"'{nameof(prezime)}' cannot be null or empty.", nameof(prezime));
        }

        _id = id;
        _ime = ime;
        _prezime = prezime;
        
    }

    public Clan(int id, string ime, string prezime, IEnumerable<Clanarina>? clanarine = null) : this(id, ime, prezime)
    {
        _clanarine = clanarine?.ToList() ?? new List<Clanarina>();
    }
    
    public Clan(int id, string ime, string prezime, string? rang = null) : this(id, ime, prezime)
    {
        _rang = rang == null ? "nema" : rang;
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
        return _clanarine != null || _rang != null
            ? Results.OnSuccess()
            : Results.OnFailure();
    }
}