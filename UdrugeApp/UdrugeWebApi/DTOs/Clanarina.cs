namespace UdrugeWebApi.DTOs
{
    public class Clanarina
    {
        public decimal Iznos { get; set; }
        public int Godina { get; set; }
    }
    
    public static partial class DtoMapping
    {
        public static Clanarina ToDto(this UdrugeApp.Domain.Models.Clanarina clanarina)
            => new Clanarina()
            {
                Iznos = clanarina.Iznos,
                Godina = clanarina.Godina,
            };

        // public static UdrugeApp.Domain.Models.Clanarina ToDomain(this Clanarina clanarina)
        //     => new UdrugeApp.Domain.Models.Clanarina(
        //         clanarina.Placenost,
        //         clanarina.Iznos,
        //         clanarina.Godina,
        //         clanarina.ClanId
        //     );
    }
    
}
