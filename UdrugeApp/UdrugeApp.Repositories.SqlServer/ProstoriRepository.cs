using UdrugeApp.DataAccess.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using UdrugeApp.Domain.Models;
using BaseLibrary;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;

namespace UdrugeApp.Repositories.SqlServer;
public class ProstoriRepository : IProstoriRepository
{
    private readonly ExampleDBContext _dbContext;

    public ProstoriRepository(ExampleDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool Exists(Prostori model)
    {
        try
        {
            return _dbContext.Prostori
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
            var model = _dbContext.Prostori
                          .AsNoTracking()
                          .FirstOrDefault(Prostori => Prostori.Id.Equals(id));
            return model is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Result<Prostori> Get(int id)
    {
        try
        {
            var model = _dbContext.Prostori
                          .AsNoTracking()
                          .FirstOrDefault(Prostori => Prostori.Id.Equals(id))?
                          .ToDomain();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<Prostori>($"No Prostori with id {id} found");
        }
        catch (Exception e)
        {
            return Results.OnException<Prostori>(e);
        }
    }
    
    public Result<IEnumerable<Prostori>> GetByIds(int[] ids)
    {
        try
        {
            var prostori = _dbContext.prostori
                                 .Where(r => ids.Contains(r.Id))
                                 .AsNoTracking()
                                 .Select(Mapping.ToDomain);



            return Results.OnSuccess(prostori);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Prostori>>(e);
        }
    }

    public Result<IEnumerable<Prostori>> GetAll()
    {
        try
        {
            var models = _dbContext.Prostori
                           .AsNoTracking()
                           .Select(Mapping.ToDomain);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Prostori>>(e);
        }
    }


    public Result Insert(Prostori model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.Prostori.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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

    public Result Update(Prostori model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            // detach
            if (_dbContext.Prostori.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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
