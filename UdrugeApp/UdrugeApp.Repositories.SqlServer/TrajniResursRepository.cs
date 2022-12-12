using BaseLibrary;
using Microsoft.EntityFrameworkCore;
using UdrugeApp.DataAccess.SqlServer.Data;
using UdrugeApp.Domain.Models;

namespace UdrugeApp.Repositories.SqlServer;

public class TrajniResursRepository : ITrajniResursRepository
{

    private readonly UdrugeContext _dbContext;

    public TrajniResursRepository(UdrugeContext dbContex)
    {
        _dbContext = dbContex;
    }
    
      public Result<TrajniResurs> Get(int id)
    {
        try
        {
            var model = _dbContext.TrajniResursi.AsNoTracking()
                .FirstOrDefault(resurs => resurs.IdResursa.Equals(id))?.ToDomain();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<TrajniResurs>($"No trajni resurs with id {id} found");
        }
        catch (Exception e)
        {
            return Results.OnException<TrajniResurs>(e);
        }
    }

    public Result<IEnumerable<TrajniResurs>> GetAll()
    {
        try
        {
            var models = _dbContext.TrajniResursi.AsNoTracking().Select(Mapping.ToDomain);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<TrajniResurs>>(e);
        }
    }

    public Result Insert(TrajniResurs model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.TrajniResursi.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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

    public Result Update(TrajniResurs model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            // detach
            if (_dbContext.TrajniResursi.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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

    public Result Remove(int id)
    {
        try
        {
            var model = _dbContext.TrajniResursi
                .AsNoTracking()
                .FirstOrDefault(resurs => resurs.IdResursa.Equals(id));

            if (model is not null)
            {
                _dbContext.TrajniResursi.Remove(model);

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

    public bool Exists(TrajniResurs model)
    {
        try
        {
            return _dbContext.TrajniResursi.AsNoTracking().Contains(model.ToDbModel());
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
            var model = _dbContext.TrajniResursi.AsNoTracking().FirstOrDefault(resurs => resurs.IdResursa.Equals(id));
            return model is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public Result<TrajniResurs> GetByInventarniBroj(string inventarniBroj)
    {
        throw new NotImplementedException();
    }

    public Result<IEnumerable<TrajniResurs>> GetByUdruge(Udruge udruga)
    {
        throw new NotImplementedException();
    }

    public Result<IEnumerable<TrajniResurs>> GetByProstor(Prostori prostor)
    {
        throw new NotImplementedException();
    }

    public Result<IEnumerable<TrajniResurs>> GetByNaziv(string naziv)
    {
        throw new NotImplementedException();
    }
}