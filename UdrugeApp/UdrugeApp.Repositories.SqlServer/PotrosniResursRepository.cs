using BaseLibrary;
using Microsoft.EntityFrameworkCore;
using UdrugeApp.DataAccess.SqlServer.Data;
using UdrugeApp.Domain.Models;
using Exception = System.Exception;

namespace UdrugeApp.Repositories.SqlServer;

public class PotrosniResursRepository : IPotrosniResursRepository
{
    
    private readonly UdrugeContext _dbContext;

    public PotrosniResursRepository(UdrugeContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Result<PotrosniResurs> Get(int id)
    {
        try
        {
            var model = _dbContext.PotrosniResursi.AsNoTracking()
                .FirstOrDefault(resurs => resurs.IdResursa.Equals(id))?.ToDomain();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<PotrosniResurs>($"No potrosni resurs with id {id} found");
        }
        catch (Exception e)
        {
            return Results.OnException<PotrosniResurs>(e);
        }
    }

    public Result<IEnumerable<PotrosniResurs>> GetAll()
    {
        try
        {
            var models = _dbContext.PotrosniResursi.AsNoTracking().Select(Mapping.ToDomain);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<PotrosniResurs>>(e);
        }
    }

    public Result Insert(PotrosniResurs model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            if (_dbContext.PotrosniResursi.Add(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Added)
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

    public Result Update(PotrosniResurs model)
    {
        try
        {
            var dbModel = model.ToDbModel();
            // detach
            if (_dbContext.PotrosniResursi.Update(dbModel).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
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
            var model = _dbContext.PotrosniResursi
                .AsNoTracking()
                .FirstOrDefault(resurs => resurs.IdResursa.Equals(id));

            if (model is not null)
            {
                _dbContext.PotrosniResursi.Remove(model);

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

    public bool Exists(PotrosniResurs model)
    {
        try
        {
            return _dbContext.PotrosniResursi.AsNoTracking().Contains(model.ToDbModel());
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
            var model = _dbContext.PotrosniResursi.AsNoTracking().FirstOrDefault(resurs => resurs.IdResursa.Equals(id));
            return model is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    //Kako ove stvari funkcioniraju?

    // public Result<IEnumerable<PotrosniResurs>> GetByUdruge(Udruge udruga)
    // {
    //     try
    //     {
    //         //Kako ovo implementirati?
    //         var models = _dbContext.PotrosniResursi.AsNoTracking().Select();
    //
    //         return Results.OnSuccess(models);
    //     }
    //     catch (Exception e)
    //     {
    //         return Results.OnException<IEnumerable<PotrosniResurs>>(e);
    //     }
    // }

    // public Result<IEnumerable<PotrosniResurs>> GetByProstori(Prostori prostor)
    // {
    //     try
    //     {
    //         //Kako ovo implementirati?
    //         var models = _dbContext.PotrosniResursi.AsNoTracking().Select();
    //
    //         return Results.OnSuccess(models);
    //     }
    //     catch (Exception e)
    //     {
    //         return Results.OnException<IEnumerable<PotrosniResurs>>(e);
    //     }
    // }
    //
    // public Result<IEnumerable<PotrosniResurs>> GetByNaziv(string naziv)
    // {
    //     try
    //     {
    //         //Kako ovo implementirati?
    //         var models = _dbContext.PotrosniResursi.AsNoTracking().Select(resurs => resurs.IdResursaNavigation.Naziv.Equals(naziv)).ToDomain;
    //
    //         return Results.OnSuccess(models);
    //     }
    //     catch (Exception e)
    //     {
    //         return Results.OnException<IEnumerable<PotrosniResurs>>(e);
    //     }
    // }
    public Result<IEnumerable<PotrosniResurs>> GetByUdruge(Udruge udruga)
    {
        throw new NotImplementedException();
    }

    public Result<IEnumerable<PotrosniResurs>> GetByProstori(Prostori prostor)
    {
        throw new NotImplementedException();
    }

    public Result<IEnumerable<PotrosniResurs>> GetByNaziv(string naziv)
    {
        throw new NotImplementedException();
    }
}