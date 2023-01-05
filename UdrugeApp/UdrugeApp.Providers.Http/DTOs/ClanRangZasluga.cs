using System.ComponentModel.DataAnnotations;

namespace UdrugeApp.Providers.Http.DTOs
{
    public class ClanRangZasluga
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name can't be null")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters")]
        public string Ime { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name can't be null")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters")]
        public string Prezime { get; set; } = string.Empty;
        
        public int? RangId { get; set; }
        public string? NazivRanga { get; set; } = string.Empty;

    }
    public static partial class DtoMapping
    {
        //public static ClanRangZasluga ToAggregateDto3(this Domain.Models.Clan clan)
            // => new Clan_RangZasluga()
            // {
            //     Id = clan.Id,
            //     Ime = clan.Ime,
            //     Prezime = clan.Prezime,
            //     RangId = clan.DodjeleZasluga?.MaxBy(d => d.Datum)?.RangZasluga.Id,
            //     NazivRanga = clan.DodjeleZasluga?.MaxBy(d => d.Datum)?.RangZasluga.Naziv,
            //
            //     //DodjeleZasluga = clan.DodjeleZasluga == null
            //       //              ? new List<DodjelaZasluga>()
            //         //            : clan.DodjeleZasluga.Select(pr => pr.ToDto()).ToList(),
            // };

        public static Domain.Models.Clan ToDomain(this ClanRangZasluga clan)
            => new Domain.Models.Clan(
                clan.Id,
                clan.Ime,
                clan.Prezime,
                clan.NazivRanga
            );
    }
}

