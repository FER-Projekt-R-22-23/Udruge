using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
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
            Udruga = resurs.Udruga.Naziv,
            IdProstor = resurs.Prostor.Id,
            Prostor = resurs.Prostor.Adresa,
            DatumNabave = resurs.DatumNabave,
            RokTrajanja= resurs.RokTrajanja,
            Napomena = resurs.Napomena,
            Naziv = resurs.Naziv
        };

    public static DomainModels.PotrosniResurs ToDomain(this PotrosniResurs resurs, DomainModels.Udruge udruga, DomainModels.Prostori prostor)
        => new DomainModels.PotrosniResurs(
            resurs.Id,
            resurs.Naziv,
            resurs.Napomena,
            resurs.DatumNabave,
            udruga,
            prostor,
            resurs.RokTrajanja
        );
}