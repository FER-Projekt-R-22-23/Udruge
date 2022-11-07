using System.ComponentModel.DataAnnotations;
using DomainModels = ExampleApp.Domain.Models;

namespace ExampleWebApi.DTOs;

public class Role
{
    public int Id { get; set; }
    public int IdClan { get; set; }

    [Required(ErrorMessage = "Pozicija can't be empty", AllowEmptyStrings = false)]
    [StringLength(10, ErrorMessage = "Pozicija can't be longer than 10 characters")]
    public string Pozicija { get; set; } = string.Empty;

    [DataType(DataType.DateTime)]
    public DateTime? NaPozicijiDo { get; set; }
}

public static partial class DtoMapping
{
    public static VoditeljiUdruge ToDto(this DomainModels.VoditeljiUdruge VoditeljiUdruge)
        => new VoditeljiUdruge()
        {
            Id = VoditeljiUdruge.Id,
            IdClan = VoditeljiUdruge.IdClan,
            Pozicija = VoditeljiUdruge.Pozicija,
            NaPozicijiDo = VoditeljiUdruge.NaPozicijiDo
        };

    public static DomainModels.VoditeljiUdruge ToDomain(this VoditeljiUdruge VoditeljiUdruge)
        => new DomainModels.VoditeljiUdruge(
            VoditeljiUdruge.Id,
            VoditeljiUdruge.IdClan,
            VoditeljiUdruge.Pozicija,
            VoditeljiUdruge.NaPozicijiDo
            );
}