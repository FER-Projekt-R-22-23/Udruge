using UdrugeApp.DataAccess.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using UdrugeApp.Domain.Models;
using BaseLibrary;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using UdrugeApp.Repositories;
using UdrugeApp.Repositories.SqlServer;

namespace ExampleApp.Repositories.SqlServer;
public class VoditeljiUdrugeRepository : IVoditeljiUdrugeRepository
{
    private readonly UdrugeContext _dbContext;

    public VoditeljiUdrugeRepository(UdrugeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(VoditeljiUdruge model)
    {
        try
        {
            return _dbContext.VoditeljiUdruge
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
            var model = _dbContext.VoditeljiUdruge
                          .AsNoTracking()
                          .FirstOrDefault(VoditeljiUdruge => VoditeljiUdruge.IdClan
                          .Equals(id));
            return model is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /*public bool Exists(int id)
    {
        try
        {
            var model = _dbContext.VoditeljiUdruge
                          .AsNoTracking()
                          .FirstOrDefault(VoditeljiUdruge => VoditeljiUdruge.Id.Equals(id));
            return model is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }*/

    public Result<VoditeljiUdruge> Get(int id)
    {
        try
        {
            var model = _dbContext.VoditeljiUdruge
                          .AsNoTracking()
                          .FirstOrDefault(VoditeljiUdruge => VoditeljiUdruge.IdClan.Equals(id))?
                          .ToDomain();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<VoditeljiUdruge>($"No VoditeljiUdruge with id {id} found");
        }
        catch (Exception e)
        {
            return Results.OnException<VoditeljiUdruge>(e);
        }
    }

    public Result<IEnumerable<VoditeljiUdruge>> GetAll()
    {
        try
        {
            var models = _dbContext.VoditeljiUdruge
                           .AsNoTracking()
                           .Select(Mapping.ToDomain);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<VoditeljiUdruge>>(e);
        }
    }

    public Result Insert(VoditeljiUdruge model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.VoditeljiUdruge.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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
            var model = _dbContext.VoditeljiUdruge
                          .AsNoTracking()
                          .FirstOrDefault(VoditeljiUdruge => VoditeljiUdruge.IdClan.Equals(id));

            if (model is not null)
            {
                _dbContext.VoditeljiUdruge.Remove(model);

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

    public Result Update(VoditeljiUdruge model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            // detach
            if (_dbContext.VoditeljiUdruge.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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









