using System.ComponentModel.DataAnnotations;
using UdrugeApp.Domain.Models;

namespace UdrugeWebApi.DTOs;

public class ClanoviRang
{
    public int Id { get; set; }

    [Required(ErrorMessage = "First name can't be null")]
    [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters")]
    public string Ime { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name can't be null")]
    [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters")]
    public string Prezime { get; set; } = string.Empty;
    
    public string NazivRanga { get; set; } = string.Empty;

}
public static partial class DtoMapping
{
    public static ClanoviRang ToRangDto(this Clan clan)
     => new ClanoviRang()
     {
         Id = clan.Id,
         Ime = clan.Ime,
         Prezime = clan.Prezime,
         NazivRanga = clan.Rang
     };

    // public static Domain.Models.Clan ToDomain(this ClanRangZasluga clan)
    //     => new Domain.Models.Clan(
    //         clan.Id,
    //         clan.Ime,
    //         clan.Prezime,
    //         clan.NazivRanga
    //     ); 
}