using BaseLibrary;
using System;
using UdrugeApp.Domain.Models;
using DbModels = UdrugeApp.DataAccess.SqlServer.Data.DbModels;

namespace UdrugeApp.Repositories.SqlServer;

public static class Mapping
{
    public static Udruge ToDomain(this DbModels.Udruge udruge)
       => new Udruge(
           udruge.IdUdruge,
           udruge.Oib,
           udruge.Naziv,
           udruge.Sjedište,
           udruge.BrMob,
           udruge.Mail
    );

    public static DbModels.Udruge ToDbModel(this Udruge udruge)
        => new DbModels.Udruge()
        {
            IdUdruge = udruge.IdUdruge,
            Oib = udruge.OIB,
            Naziv = udruge.Naziv,
            Sjedište = udruge.Sjediste,
            BrMob = udruge.BrMob,
            Mail = udruge.Mail
            
        };

    public static Prostori ToDomain(this DbModels.Prostori prostori)
       => new Prostori(
           prostori.IdProstor,
           prostori.IdUdruge,
           prostori.Adresa,
           prostori.Namjena,
           prostori.Dodijelio,
           prostori.DodjeljenoDo
           
    );

    public static DbModels.Prostori ToDbModel(this Prostori prostori)
        => new DbModels.Prostori()
        {
            IdProstor = prostori.Id,
            IdUdruge = prostori.IdUdruge,
            Adresa = prostori.Adresa,
            Namjena = prostori.Namjena,
            Dodijelio = prostori.Dodijelio,
            DodjeljenoDo = prostori.DodjeljenoDo
        };


}