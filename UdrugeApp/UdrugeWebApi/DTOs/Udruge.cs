using System;
using System.ComponentModel.DataAnnotations;
using DomainModels = UdrugeApp.Domain.Models;

namespace UdrugeWebApi.DTOs;

    public class Udruge
    {
    public int IdUdruge { get; set; }

    [Required(ErrorMessage = "OIB cannot be null")]
    [StringLength(11, ErrorMessage = "OIB length must be 11 characters")]
    public string OIB { get; set; } = string.Empty;

    [Required(ErrorMessage = "Naziv cannot be null")]
    [StringLength(30, ErrorMessage = "Naziv can't be longer than 30 characters")]
    public string Naziv { get; set; } = string.Empty;


    [Required(ErrorMessage = "Sjediste cannot be null")]
    [StringLength(60, ErrorMessage = "Sjediste can't be longer than 60 characters")]
    public string Sjediste { get; set; } = string.Empty;

    [Required(ErrorMessage = "BrMob cannot be null")]
    [StringLength(15, ErrorMessage = "BrMob can't be longer than 15 characters")]
    public string BrMob { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mail cannot be null")]
    [StringLength(50, ErrorMessage = "Mail can't be longer than 50 characters")]
    public string Mail { get; set; } = string.Empty;
    

    }

public static partial class DtoMapping
{
    public static Udruge ToDto(this DomainModels.Udruge udruge)
        => new Udruge()
        {
            IdUdruge = udruge.IdUdruge,
            OIB = udruge.OIB,
            Naziv = udruge.Naziv,
            Sjediste = udruge.Sjediste,
            BrMob = udruge.BrMob,
            Mail = udruge.Mail
        };

    public static DomainModels.Udruge ToDomain(this Udruge udruge)
        => new DomainModels.Udruge(
            udruge.IdUdruge,
            udruge.OIB,
            udruge.Naziv,
            udruge.Sjediste,
            udruge.BrMob,
            udruge.Mail
            );
}




