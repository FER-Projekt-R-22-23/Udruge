using System.ComponentModel.DataAnnotations;
using DomainModels = UdrugeApp.Domain.Models;

namespace UdrugeWebApi.DTOs;

public class Resurs
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "IdUdruge can't be null")]
    public int IdUdruge { get; set; }
    
    public string Udruga { get; set; }
    
    [Required(ErrorMessage = "IdProstor can't be null")]
    public int IdProstor { get; set; }
    
    public string Prostor { get; set; }

    [Required(ErrorMessage = "Naziv can't be null")]
    [StringLength(50, ErrorMessage = "Naziv can't be longer than 50 characters")]
    public string Naziv { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "DatumNabave name can't be null")]
    [DataType(DataType.DateTime)]
    public DateTime DatumNabave { get; set; }
    
    [StringLength(100, ErrorMessage = "Napomena can't be longer than 50 characters")]
    public string? Napomena { get; set; } = string.Empty;
}

public static partial class DtoMapping
{
    public static Resurs ToDto(this DomainModels.Resurs resurs)
        => new Resurs()
        {
            Id = resurs.Id,
            IdUdruge = resurs.Udruga.IdUdruge,
            Udruga = resurs.Udruga.Naziv,
            IdProstor = resurs.Prostor.Id,
            Prostor = resurs.Prostor.Adresa,
            DatumNabave = resurs.DatumNabave,
            Napomena = resurs.Napomena,
            Naziv = resurs.Naziv
        };

    public static DomainModels.Resurs ToDomain(this Resurs resurs, DomainModels.Udruge udruga, DomainModels.Prostori prostor)
        => new DomainModels.Resurs(
            resurs.Id,
            resurs.Naziv,
            resurs.Napomena,
            resurs.DatumNabave,
            udruga,
            prostor
        );
}