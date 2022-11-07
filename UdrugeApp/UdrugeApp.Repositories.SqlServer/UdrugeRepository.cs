using UdrugeApp.DataAccess.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using UdrugeApp.Domain.Models;
using BaseLibrary;
using UdrugeApp.Repositories;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using UdrugeApp.Repositories.SqlServer;

namespace ExampleApp.Repositories.SqlServer;
public class UdrugeRepository : IUdrugeRepository
{
    private readonly ExampleDBContext _dbContext;

    public UdrugeRepository(ExampleDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(Udruge model)
    {
        try
        {
            return _dbContext.Udruge
                     .AsNoTracking()
                     .Contains(model.ToDbModel());
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Exists(int id)
    {
        try
        {
            var model = _dbContext.Udruge
                          .AsNoTracking()
                          .FirstOrDefault(Udruge => Udruge.Id.Equals(id));
            return model is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Result<Udruge> Get(int id)
    {
        try
        {
            var model = _dbContext.Udruge
                          .AsNoTracking()
                          .FirstOrDefault(Udruge => Udruge.Id.Equals(id))?
                          .ToDomain();

            return model is not null
            ? Results.OnSuccess(model)
                : Results.OnFailure<Udruge>($"No Udruge with id {id} found");
        }
        catch (Exception e)
        {
            return Results.OnException<Udruge>(e);
        }
    }

   

    public Result<IEnumerable<Udruge>> GetAll()
    {
        try
        {
            var models = _dbContext.Udruge
                           .AsNoTracking()
                           .Select(Mapping.ToDomain);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Udruge>>(e);
        }
    }

   

    public Result Insert(Udruge model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.People.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
            {
                var isSuccess = _dbContext.SaveChanges() > 0;

                // every Add attaches the entity object and EF begins tracking
                // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
                _dbContext.Entry(dbModel).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                return isSuccess
                    ? Results.OnSuccess()
                    : Results.OnFailure();
            }

            return Results.OnFailure();
        }
        catch (Exception e)
        {
            return Results.OnException(e);
        }
    }

    public Result Remove(int id)
    {
        try
        {
            var model = _dbContext.Prostori
                          .AsNoTracking()
                          .FirstOrDefault(Prostori => Prostori.Id.Equals(id));

            if (model is not null)
            {
                _dbContext.Prostori.Remove(model);

                return _dbContext.SaveChanges() > 0
                    ? Results.OnSuccess()
                    : Results.OnFailure();
            }
            return Results.OnFailure();
        }
        catch (Exception e)
        {
            return Results.OnException(e);
        }
    }



    public Result Update(Udruge model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            // detach
            if (_dbContext.Udruge.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
            {
                var isSuccess = _dbContext.SaveChanges() > 0;

                // every Update attaches the entity object and EF begins tracking
                // we detach the entity object from tracking, because this can cause problems when a repo is not set as a transient service
                _dbContext.Entry(dbModel).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                return isSuccess
                    ? Results.OnSuccess()
                    : Results.OnFailure();
            }

            return Results.OnFailure();
        }
        catch (Exception e)
        {
            return Results.OnException(e);
        }
    }

    
}