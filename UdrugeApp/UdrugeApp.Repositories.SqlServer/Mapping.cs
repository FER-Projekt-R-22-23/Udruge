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
           udruge.Sjedi�te,
           udruge.BrMob,
           udruge.Mail
    );

    public static DbModels.Udruge ToDbModel(this Udruge udruge)
        => new DbModels.Udruge()
        {
            IdUdruge = udruge.IdUdruge,
            Oib = udruge.OIB,
            Naziv = udruge.Naziv,
            Sjedi�te = udruge.Sjediste,
            BrMob = udruge.BrMob,
            Mail = udruge.Mail
            
        };


}