using System.ComponentModel.DataAnnotations;
using DomainModels = UdrugeApp.Domain.Models;

namespace UdrugeWebApi.DTOs;

public class PotrosniResurs : Resurs
{

    [Required(ErrorMessage = "RokTrajanja can't be null")]
    [DataType(DataType.DateTime)]
    public DateTime RokTrajanja { get; set; }
    
}

public static partial class DtoMapping
{
    public static PotrosniResurs ToDto(this DomainModels.PotrosniResurs resurs)
        => new PotrosniResurs()
        {
            Id = resurs.Id,
            IdUdruge = resurs.Udruga.IdUdruge,
            IdProstor = resurs.Prostor.Id,
            DatumNabave = resurs.DatumNabave,
            RokTrajanja= resurs.RokTrajanja,
            Naponema = resurs.Napomena,
            Naziv = resurs.Naziv
        };

    public static DomainModels.PotrosniResurs ToDomain(this PotrosniResurs resurs)
        => new DomainModels.PotrosniResurs(
            resurs.Id,
            resurs.Naziv,
            resurs.Naponema,
            resurs.DatumNabave,
            //Kako cu dalje?? Jesam li ja to uopce dobro skuzio
            null,
            null,
            resurs.RokTrajanja
        );
}