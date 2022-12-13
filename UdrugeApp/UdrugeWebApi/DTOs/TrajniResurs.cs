using System.ComponentModel.DataAnnotations;
using DomainModels = UdrugeApp.Domain.Models;

namespace UdrugeWebApi.DTOs;

public class TrajniResurs : Resurs
{

    [Required(ErrorMessage = "InventarniBroj can't be null")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "InventarniBroj must be 6 characters")]
    public String InventarniBroj { get; set; } = String.Empty;
    
    //Trebam li ovdje settat default value u true?
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
            Naponema = resurs.Napomena,
            Naziv = resurs.Naziv
        };

    public static DomainModels.TrajniResurs ToDomain(this TrajniResurs resurs)
        => new DomainModels.TrajniResurs(
            resurs.Id,
            resurs.Naziv,
            resurs.Naponema,
            resurs.DatumNabave,
            //Kako cu dalje?? Jesam li ja to uopce dobro skuzio
            null,
            null,
            resurs.InventarniBroj,
            resurs.JeDostupno
        );
}