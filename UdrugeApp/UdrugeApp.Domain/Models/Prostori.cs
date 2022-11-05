using BaseLibrary;
using UdrugeApp.Commons;

namespace UdrugeApp.Domain.Models;
public class Prostori
{
    private int _Id;
    private int _IdUdruge;
    private string _Adresa;
    private string _Namjena;
    private string _Dodijelio;
    private DateTime? _DodjeljenoDo;

    public int Id { get => _Id; set => _Id = value; }
    public int IdUdruge { get => _IdUdruge; set => _IdUdruge = value; }
    public string Adresa { get => _Adresa; set => _Adresa = value; }
    public string Namjena { get => _Namjena; set => _Namjena = value; }
    public string Dodijelio { get => _Dodijelio; set => _Dodijelio = value; }
    public DateTime? DodjeljenoDo { get => _DodjeljenoDo; set => _DodjeljenoDo = value; }

    public Prostori(int id, int idUdruge, string Adresa, string Namjena, string Dodijelio, DateTime? DodjeljenoDo)
    {
        if (string.IsNullOrEmpty(Adresa))
        {
            throw new ArgumentException($"'{nameof(Adresa)}' cannot be null or empty.", nameof(Adresa));
        }

        if (string.IsNullOrEmpty(Namjena))
        {
            throw new ArgumentException($"'{nameof(Namjena)}' cannot be null or empty.", nameof(Namjena));
        }
        if (int.IsNullOrEmpty(id))
        {
            throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
        }
        if (int.IsNullOrEmpty(idUdruge))
        {
            throw new ArgumentException($"'{nameof(idUdruge)}' cannot be null or empty.", nameof(idUdruge));
        }
        if (string.IsNullOrEmpty(Dodijelio))
        {
            throw new ArgumentException($"'{nameof(Dodijelio)}' cannot be null or empty.", nameof(Dodijelio));
        }

        _Id = id;
        _IdUdruge = idUdruge;
        _Adresa = Adresa;
        _Namjena = Namjena;
        _Dodijelio = Dodijelio;
        _DodjeljenoDo = DodjeljenoDo;
    }

    public override bool Equals(object? obj)
    {
        return  obj is not null &&
                obj is Prostori Prostori &&
               _Id == Prostori._Id &&
               _IdUdruge == Prostori._IdUdruge &&
               _Adresa == Prostori._Adresa &&
               _Namjena == Prostori._Namjena &&
               _Dodijelio == Prostori._Dodijelio &&
               _DodjeljenoDo == Prostori._DodjeljenoDo
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_Id,_IdUdruge, _Adresa, _Namjena, _Dodijelio, _DodjeljenoDo);
    }

    public override Result IsValid()
        => Validation.Validate(
            (() => _Adresa.Length <= 50, "Adresa lenght must be less than 50 characters"),
            (() => _Namjena.Length <= 50, "Namjena lenght must be less than 100 characters"),
            (() => _Dodijelio.Length <= 50, "Dodijelio lenght must be less than 50 characters"),
            (() => !string.IsNullOrEmpty(_Adresa.Trim()), "Adresa can't be null, empty, or whitespace"),
            (() => !string.IsNullOrEmpty(_Namjena.Trim()), "Namjena can't be null, empty, or whitespace")
            );
}