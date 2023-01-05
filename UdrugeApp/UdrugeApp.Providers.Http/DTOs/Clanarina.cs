namespace UdrugeApp.Providers.Http.DTOs
{
    public class Clanarina
    {
        public int Id { get; set; }
        public bool Placenost { get; set; }
        
        public decimal Iznos { get; set; }
        public int Godina { get; set; }
        public int ClanId { get; set; }
        
        public DateTime? Datum { get; set; }
    }
    
    public static partial class DtoMapping
    {
        // public static Clanarina ToDto(this Domain.Models.Clanarina clanarina)
        //     => new Clanarina()
        //     {
        //         Placenost = clanarina.Placenost,
        //         Iznos = clanarina.Iznos,
        //         Godina = clanarina.Godina,
        //     };

        public static Domain.Models.Clanarina ToDomain(this Clanarina clanarina)
            => new Domain.Models.Clanarina(
                clanarina.Iznos,
                clanarina.Godina
            );
    }
    
}
