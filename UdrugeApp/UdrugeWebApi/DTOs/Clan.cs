using System.ComponentModel.DataAnnotations;

namespace UdrugeWebApi.DTOs
{
    public partial class Clan
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name can't be null")]
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters")]
        public string Ime { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name can't be null")]
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters")]
        public string Prezime { get; set; } = string.Empty;

        [DataType(DataType.DateTime)]
        public DateTime DatumRodenja { get; set; }
        public byte[]? Slika { get; set; }

        [Required(ErrorMessage = "Address can't be null")]
        [StringLength(50, ErrorMessage = "Address can't be longer than 50 characters")]
        public string Adresa { get; set; } = string.Empty;
        public bool ImaMaramu { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DatumMarama { get; set; }

        [StringLength(50)]
        public string? MjestoMarama { get; set; } = string.Empty;
    }
}

