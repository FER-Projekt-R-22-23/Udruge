using System.ComponentModel.DataAnnotations;
using DomainModels = UdrugeApp.Domain.Models;

namespace UdrugeWebApi.DTOs;

public class TrajniResurs : Resurs
{

    [Required(ErrorMessage = "InventarniBroj can't be null")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "InventarniBroj must be 6 characters")]
    public String InventarniBroj { get; set; } = String.Empty;
    
    public bool JeDostupno { get; set; }
    
}

public static partial class DtoMapping
{
    public static TrajniResurs ToDto(this DomainModels.TrajniResurs resurs)
        => new TrajniResurs()
        {
            Id = resurs.Id,
            IdUdruge = resurs.Udruga.IdUdruge,
            IdProstor = resurs.Prostor.Id,
            DatumNabave = resurs.DatumNabave,
            InventarniBroj = resurs.InventarniBroj,
            JeDostupno = resurs.JeDostupno,
            Napomena = resurs.Napomena,
            Naziv = resurs.Naziv
        };

    public static DomainModels.TrajniResurs ToDomain(this TrajniResurs resurs, DomainModels.Udruge udruga, DomainModels.Prostori prostor)
        => new DomainModels.TrajniResurs(
            resurs.Id,
            resurs.Naziv,
            resurs.Napomena,
            resurs.DatumNabave,
            udruga,
            prostor,
            resurs.InventarniBroj,
            resurs.JeDostupno
        );
}