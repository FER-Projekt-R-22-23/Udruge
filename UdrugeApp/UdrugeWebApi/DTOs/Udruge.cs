using System;
using System.ComponentModel.DataAnnotations;
using DomainModels = UdrugeApp.Domain.Models;

namespace UdrugeWebApi.DTOs;

    public class Udruge
    {
    public int _IdUdruge { get; set; }

    [Required(ErrorMessage = "Id Udruge cannot be null")]

    public string _OIB { get; set; } = string.Empty;

    [Required(ErrorMessage = "OIB cannot be null")]
    [StringLength(11, ErrorMessage = "OIB length must be 11 characters")]

    public string _Naziv { get; set; } = string.Empty;
    [Required(ErrorMessage = "Naziv cannot be null")]
    [StringLength(30, ErrorMessage = "Naziv can't be longer than 30 characters")]

    public string _Sjediste { get; set; } = string.Empty;
    [Required(ErrorMessage = "Sjediste cannot be null")]
    [StringLength(60, ErrorMessage = "Sjediste can't be longer than 60 characters")]

    public string _BrMob { get; set; } = string.Empty;
    [Required(ErrorMessage = "BrMob cannot be null")]
    [StringLength(15, ErrorMessage = "BrMob can't be longer than 15 characters")]

    public string _Mail { get; set; } = string.Empty;
    [Required(ErrorMessage = "Mail cannot be null")]
    [StringLength(50, ErrorMessage = "Mail can't be longer than 50 characters")]

    }

    




public static partial class DtoMapping
{
    public static Udruge ToDto(this DomainModels.Udruge udruge)
        => new Udruge()
        {
            _IdUdruge = udruge.IdUdruge,
            _OIB = udruge.OIB,
            _Naziv = udruge.Naziv,
            _Sjediste = udruge.Sjediste,
            _BrMob = udruge.BrMob,
            _Mail = udruge.Mail
        };

    public static DomainModels.Udruge ToDomain(this Udruge udruge)
        => new DomainModels.Udruge(
            udruge._IdUdruge,
            udruge._OIB,
            udruge._Naziv,
            udruge._Sjediste,
            udruge._BrMob,
            udruge._Mail
            );
}




