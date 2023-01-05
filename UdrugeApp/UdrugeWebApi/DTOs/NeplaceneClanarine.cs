using System.ComponentModel.DataAnnotations;

namespace UdrugeWebApi.DTOs
{
    public  class NeplaceneClanarine
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name can't be null")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters")]
        public string Ime { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name can't be null")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters")]
        public string Prezime { get; set; } = string.Empty;

        public IEnumerable<Clanarina> Clanarina { get; set; } = Enumerable.Empty<Clanarina>();
    }
    
    public static partial class DtoMapping
    {
        public static NeplaceneClanarine ToDto(this UdrugeApp.Domain.Models.Clan clan)
            => new NeplaceneClanarine()
            {
                Id = clan.Id,
                Ime = clan.Ime,
                Prezime = clan.Prezime,
                Clanarina = clan.Clanarina.Any() ? clan.Clanarina.Select(pr => pr.ToDto()) : Enumerable.Empty<Clanarina>()
            };

        // public static UdrugeApp.Domain.Models.Clan ToDomain(NeplaceneClanarine clan)
        //     => new UdrugeApp.Domain.Models.Clan(
        //         clan.Ime,
        //         clan.Prezime,
        //         clan.Clanarina.Select(c => c.ToDomain())
        //     );
    }
    
}

