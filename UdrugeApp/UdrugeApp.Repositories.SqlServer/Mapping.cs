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
           udruge.Sjediste,
           udruge.BrMob,
           udruge.Mail
    );

    public static DbModels.Udruge ToDbModel(this Udruge udruge)
        => new DbModels.Udruge()
        {
            IdUdruge = udruge.IdUdruge,
            Oib = udruge.OIB,
            Naziv = udruge.Naziv,
            Sjediste = udruge.Sjediste,
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
    
    public static PotrosniResurs ToDomain(this DbModels.PotrosniResursi resurs)
        => new PotrosniResurs(
            resurs.IdResursa,
            resurs.IdResursaNavigation.Naziv,
            resurs.IdResursaNavigation.Napomena,
            resurs.IdResursaNavigation.DatumNabave,
            ToDomain(resurs.IdResursaNavigation.IdUdrugeNavigation),
            ToDomain(resurs.IdResursaNavigation.IdProstorNavigation),
            resurs.RokTrajanja
        );

    
    public static DbModels.PotrosniResursi ToDbModel(this PotrosniResurs resurs)
        => new DbModels.PotrosniResursi()
        {
            IdResursa = resurs.Id,
            RokTrajanja = resurs.RokTrajanja,
            IdResursaNavigation = new DbModels.Resursi()
            {
                DatumNabave = resurs.DatumNabave,
                IdProstor = resurs.Prostor.Id,
                IdResursa = resurs.Id,
                IdUdruge = resurs.Udruga.Id,
                Napomena = resurs.Napomena,
                Naziv = resurs.Naziv
            }
        };

    public static TrajniResurs ToDomain(this DbModels.TrajniResursi resurs)
    {
        return new TrajniResurs(
            resurs.IdResursa,
            resurs.IdResursaNavigation.Naziv,
            resurs.IdResursaNavigation.Napomena,
            resurs.IdResursaNavigation.DatumNabave,
            ToDomain(resurs.IdResursaNavigation.IdUdrugeNavigation),
            ToDomain(resurs.IdResursaNavigation.IdProstorNavigation),
            resurs.InventarniBroj,
            resurs.JeDostupno
        );
    }
    
    public static DbModels.Resursi ToDbModel(this Resurs resurs)
        => new DbModels.Resursi()
        {
            IdResursa = resurs.Id,
            DatumNabave = resurs.DatumNabave,
            IdProstor = resurs.Prostor.Id,
            IdUdruge = resurs.Udruga.Id,
            Napomena = resurs.Napomena,
            Naziv = resurs.Naziv
        };

    public static Resurs ToDomain(this DbModels.Resursi resurs)
    {
        return new Resurs(
            resurs.IdResursa,
            resurs.Naziv,
            resurs.Napomena,
            resurs.DatumNabave,
            ToDomain(resurs.IdUdrugeNavigation),
            ToDomain(resurs.IdProstorNavigation)
        );
    }

    public static DbModels.TrajniResursi ToDbModel(this TrajniResurs resurs)
        => new DbModels.TrajniResursi()
        {
            IdResursa = resurs.Id,
            IdResursaNavigation = new DbModels.Resursi()
            {
                DatumNabave = resurs.DatumNabave,
                IdProstor = resurs.Prostor.Id,
                IdResursa = resurs.Id,
                IdUdruge = resurs.Udruga.Id,
                Napomena = resurs.Napomena,
                Naziv = resurs.Naziv
            },
            InventarniBroj = resurs.InventarniBroj,
            JeDostupno = resurs.JeDostupno
        };
    
    


    public static VoditeljiUdruge ToDomain(this DbModels.VoditeljiUdruge voditelji)
       => new VoditeljiUdruge(
           voditelji.IdUdruge,
           voditelji.IdClan,
           voditelji.Pozicija,
           voditelji.NaPozicijiDo
    );

    public static DbModels.VoditeljiUdruge ToDbModel(this VoditeljiUdruge voditelji)
        => new DbModels.VoditeljiUdruge()
        {
            IdUdruge = voditelji.IdUdruge,
            IdClan = voditelji.IdClan,
            Pozicija = voditelji.Pozicija,
            NaPozicijiDo = voditelji.NaPozicijiDo
        };


}