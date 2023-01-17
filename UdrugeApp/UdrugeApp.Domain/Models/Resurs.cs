using System.Data;
using BaseLibrary;
using UdrugeApp.Commons;

namespace UdrugeApp.Domain.Models;

public class Resurs : Entity<int>
{

    private string _naziv;
    private string? _napomena;
    private DateTime _datumNabave;
    private Udruge _udruga;
    private Prostori _prostor;
    
    public string Naziv { get => _naziv; set => _naziv = value; }
    public string Napomena { get => _napomena; set => _napomena = value; }
    public DateTime DatumNabave { get => _datumNabave; set => _datumNabave = value; }
    public Udruge Udruga { get => _udruga; set => _udruga = value; }
    public Prostori Prostor { get => _prostor; set => _prostor = value; }

    public Resurs(int id, string naziv, string? napomena, DateTime datumNabave, Udruge udruga,
        Prostori prostor) : base(id)
    {
        if (string.IsNullOrEmpty(naziv))
        {
            throw new ArgumentException($"'{nameof(naziv)}' cannot be null or empty.", nameof(naziv));
        }
        if (datumNabave == DateTime.MinValue)
        {
            throw new ArgumentException($"'{nameof(datumNabave)}' cannot be null or empty.", nameof(datumNabave));
        }
        if (udruga == null)
        {
            throw new ArgumentException($"'{nameof(udruga)}' cannot be null.", nameof(udruga));
        }
        if (prostor == null)
        {
            throw new ArgumentException($"'{nameof(prostor)}' cannot be null.", nameof(prostor));
        }

        _naziv = naziv;
        _napomena = napomena is not null ? napomena : String.Empty;
        _datumNabave = datumNabave;
        _udruga = udruga;
        _prostor = prostor;
    }

    public void ChangeUdruga(Udruge udruga)
    {
        _udruga = udruga;
    }

    public void ChangeProstor(Prostori prostor)
    {
        _prostor = prostor;
    }

    //Kako validateat DateTime? udrugu i prostor?
    public override Result IsValid()
        => Validation.Validate(
            (() => _naziv.Length <= 50, "First name lenght must be less than 50 characters"),
            (() => _napomena?.Length <= 50, "Napomena lenght must be less than 50 characters"), 
            (() => !string.IsNullOrEmpty(_naziv.Trim()), "Naziv can't be null, empty, or whitespace")
        );

    public override bool Equals(object? obj)
    {
        return  obj is not null &&
                obj is Resurs resurs &&
                _id == resurs._id &&
                _naziv == resurs._naziv &&
                _datumNabave == resurs._datumNabave &&
                _udruga.Equals(resurs._udruga) &&
                _prostor.Equals(resurs._prostor);
    }


    public override int GetHashCode()
    {
        return HashCode.Combine(_id, _naziv, _napomena, _datumNabave, _udruga, _prostor);
    }
}