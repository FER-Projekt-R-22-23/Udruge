using System.ComponentModel.DataAnnotations;
using DomainModels = UdrugeApp.Domain.Models;

namespace UdrugeWebApi.DTOs;

public class Prostori
{
    public int Id { get; set; }
    public int IdUdruge { get; set; }

    [Required(ErrorMessage = "Adresa can't be null")]
    [StringLength(50, ErrorMessage = "Adresa can't be longer than 50 characters")]
    public string Adresa { get; set; } = string.Empty;

    [Required(ErrorMessage = "Namjena can't be null")]
    [StringLength(100, ErrorMessage = "Namjena can't be longer than 100 characters")]
    public string Namjena { get; set; } = string.Empty;

    [Required(ErrorMessage = "Dodijelio can't be null")]
    [StringLength(50, ErrorMessage = "Dodijelio can't be longer than 50 characters")]
    public string Dodijelio { get; set; } = string.Empty;


    [DataType(DataType.DateTime)]
    public DateTime? DodjeljenoDo { get; set; }
    public float? GeoDuzina { get; set; }
    public float? GeoSirina { get; set; }
}


public static partial class DtoMapping
{
    public static Prostori ToDto(this DomainModels.Prostori Prostori)
        => new Prostori()
        {
            Id = Prostori.Id,
            IdUdruge = Prostori.IdUdruge,
            Adresa = Prostori.Adresa,
            Namjena = Prostori.Namjena,
            Dodijelio = Prostori.Dodijelio,
            DodjeljenoDo = Prostori.DodjeljenoDo,
            GeoDuzina = Prostori.GeoDuzina,
            GeoSirina = Prostori.GeoSirina
        };

    public static DomainModels.Prostori ToDomain(this Prostori Prostori)
        => new DomainModels.Prostori(
            Prostori.Id,
            Prostori.IdUdruge,
            Prostori.Adresa,
            Prostori.Namjena,
            Prostori.Dodijelio,
            Prostori.DodjeljenoDo,
            Prostori.GeoDuzina,
            Prostori.GeoSirina
            );
}