using System.ComponentModel.DataAnnotations;

namespace UdrugeApp.Providers.Http.DTOs
{
    public  class ClanNijePlatio
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "First name can't be null")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters")]
        public string Ime { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name can't be null")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters")]
        public string Prezime { get; set; } = string.Empty;

        public IEnumerable<Clanarina> Clanarina { get; set; } = Enumerable.Empty<Clanarina>();
        
        public static partial class DtoMapping
        {
            // public static ClanNijePlatio ToDto(Domain.Models.Clan clan)
                // => new ClanNijePlatio()
                // {
                //     Ime = clan.Ime,
                //     Prezime = clan.Prezime,
                //     Clanarina = clan.Clanarina.Select(pr => pr.ToDto()).ToList()
                // };

            public static Domain.Models.Clan ToDomain(ClanNijePlatio clan)
                => new Domain.Models.Clan(
                    clan.Id,
                    clan.Ime,
                    clan.Prezime,
                    clan.Clanarina.Select(c => c.ToDomain())
                );
        }
        
    }
}

