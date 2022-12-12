using System;
using System.ComponentModel.DataAnnotations;
using DomainModels = UdrugeApp.Domain.Models;

namespace UdrugeWebApi.DTOs;

public class VoditeljiUdruge
{
    public int _Id { get; set; }
    public int _IdClan { get; set; }

    [Required(ErrorMessage = "Pozicija can't be empty", AllowEmptyStrings = false)]
    [StringLength(10, ErrorMessage = "Pozicija can't be longer than 10 characters")]
    public string _Pozicija { get; set; } = string.Empty;

    [DataType(DataType.DateTime)]
    public DateTime? _NaPozicijiDo { get; set; }
}

public static partial class DtoMapping
{
    public static VoditeljiUdruge ToDto(this DomainModels.VoditeljiUdruge VoditeljiUdruge)
        => new VoditeljiUdruge()
        {
            _Id = VoditeljiUdruge.Id,
            _IdClan = VoditeljiUdruge.IdClan,
            _Pozicija = VoditeljiUdruge.Pozicija,
            _NaPozicijiDo = VoditeljiUdruge.NaPozicijiDo
        };

    public static DomainModels.VoditeljiUdruge ToDomain(this VoditeljiUdruge VoditeljiUdruge)
        => new DomainModels.VoditeljiUdruge(
            VoditeljiUdruge._Id,
            VoditeljiUdruge._IdClan,
            VoditeljiUdruge._Pozicija,
            VoditeljiUdruge._NaPozicijiDo
            );
}