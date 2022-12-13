using System.ComponentModel.DataAnnotations;

namespace UdrugeWebApi.DTOs;

public abstract class Resurs
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "IdUdruge can't be null")]
    public int IdUdruge { get; set; }
    
    [Required(ErrorMessage = "IdProstor can't be null")]
    public int IdProstor { get; set; }

    [Required(ErrorMessage = "Naziv can't be null")]
    [StringLength(50, ErrorMessage = "Naziv can't be longer than 50 characters")]
    public string Naziv { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "DatumNabave name can't be null")]
    [DataType(DataType.DateTime)]
    public DateTime DatumNabave { get; set; }
    
    [StringLength(100, ErrorMessage = "Napomena can't be longer than 50 characters")]
    public string? Naponema { get; set; } = string.Empty;
    
}