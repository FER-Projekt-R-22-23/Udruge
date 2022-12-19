using System.Reflection;
using BaseLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using UdrugeApp.DataAccess.SqlServer.Data;
using UdrugeApp.Domain.Models;

namespace UdrugeApp.Repositories.SqlServer;

public class ResursRepository : IResursRepository
{
    private readonly UdrugeContext _dbContext;

    public ResursRepository(UdrugeContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Result<Resurs> Get(int id)
    {
        try
        {
            var model = _dbContext.Resursi.AsNoTracking()
                .Include(d => d.IdUdrugeNavigation)
                .Include(d => d.IdProstorNavigation)
                .FirstOrDefault(resurs => resurs.IdResursa.Equals(id))?.ToDomain();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<Resurs>($"No resurs with id {id} found");
        }
        catch (Exception e)
        {
            return Results.OnException<Resurs>(e);
        }
    }

    public Result<IEnumerable<Resurs>> GetAll()
    {
        try
        {
            var models = _dbContext.Resursi.AsNoTracking()
                .Include(a => a.IdUdrugeNavigation)
                .Include(a => a.IdProstorNavigation)
                .Select(Mapping.ToDomain);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Resurs>>(e);
        }
    }
    
    public Result<IEnumerable<PotrosniResurs>> GetAllPotrosni()
    {
        try
        {
            var models = _dbContext.PotrosniResursi.AsNoTracking()
                .Include(d => d.IdResursaNavigation).ThenInclude(a => a.IdUdrugeNavigation)
                .Include(d => d.IdResursaNavigation).ThenInclude(a => a.IdProstorNavigation)
                .Select(Mapping.ToDomain);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<PotrosniResurs>>(e);
        }
    }
    
    public Result<IEnumerable<TrajniResurs>> GetAllTrajni()
    {
        try
        {
            var models = _dbContext.TrajniResursi.AsNoTracking()
                .Include(d => d.IdResursaNavigation).ThenInclude(a => a.IdUdrugeNavigation)
                .Include(d => d.IdResursaNavigation).ThenInclude(a => a.IdProstorNavigation)
                .Select(Mapping.ToDomain);

            return Results.OnSuccess(models);
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<TrajniResurs>>(e);
        }
    }

    public Result Insert(Resurs model)
    {
        throw new NotImplementedException();
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

    public Result Update(Resurs model)
    {
        throw new NotImplementedException();
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
            var model = _dbContext.Resursi
                .AsNoTracking()
                .FirstOrDefault(resurs => resurs.IdResursa.Equals(id));

            if (model is not null)
            {
                _dbContext.Resursi.Remove(model);

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

    public bool Exists(Resurs model)
    {
        try
        {
            return _dbContext.Resursi.AsNoTracking().Contains(model.ToDbModel());
        }
        catch (Exception)
        {
            return false;
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
            var model = _dbContext.Resursi.AsNoTracking().FirstOrDefault(resurs => resurs.IdResursa.Equals(id));
            return model is not null;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public bool ExistsTrajni(int id)
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
    
    public bool ExistsPotrosni(int id)
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

    public Result<IEnumerable<PotrosniResurs>> GetByRokTrajanja(DateTime rokTrajanja)
    {
        try
        {
            var models = _dbContext.PotrosniResursi.AsNoTracking()
                .Include(d => d.IdResursaNavigation).ThenInclude(a => a.IdUdrugeNavigation)
                .Include(d => d.IdResursaNavigation).ThenInclude(a => a.IdProstorNavigation)
                .Where(resurs => resurs.RokTrajanja < rokTrajanja).AsEnumerable()
                .Select(Mapping.ToDomain);

            return models.Any()
                ? Results.OnSuccess(models)
                : Results.OnFailure<IEnumerable<PotrosniResurs>>($"No resurs with rok trajanja before {rokTrajanja} found");
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<PotrosniResurs>>(e);
        }
    }

    public Result<TrajniResurs> GetByInventarniBroj(string inventarniBroj)
    {
        try
        {
            var model = _dbContext.TrajniResursi.AsNoTracking()
                .Include(d => d.IdResursaNavigation).ThenInclude(a => a.IdUdrugeNavigation)
                .Include(d => d.IdResursaNavigation).ThenInclude(a => a.IdProstorNavigation)
                .FirstOrDefault(resurs => resurs.InventarniBroj.Equals(inventarniBroj))?.ToDomain();

            return model is not null
                ? Results.OnSuccess(model)
                : Results.OnFailure<TrajniResurs>($"No resurs with inventarni broj {inventarniBroj} found");
        }
        catch (Exception e)
        {
            return Results.OnException<TrajniResurs>(e);
        }
    }

    public Result<IEnumerable<Resurs>> GetByIdUdruge(int id)
    {
        try
        {
            var models = _dbContext.Resursi.AsNoTracking()
                .Include(a => a.IdUdrugeNavigation)
                .Include(a => a.IdProstorNavigation)
                .Where(resurs => resurs.IdUdruge == id).AsEnumerable()
                .Select(Mapping.ToDomain);

            return models.Any()
                ? Results.OnSuccess(models)
                : Results.OnFailure<IEnumerable<Resurs>>($"Udruga with id: {id} has no resursi");
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Resurs>>(e);
        }
    }

    public Result<IEnumerable<Resurs>> GetByIdProstori(int id)
    {
        try
        {
            var models = _dbContext.Resursi.AsNoTracking()
                .Include(a => a.IdUdrugeNavigation)
                .Include(a => a.IdProstorNavigation)
                .Where(resurs => resurs.IdProstor == id).AsEnumerable()
                .Select(Mapping.ToDomain);

            return models.Any()
                ? Results.OnSuccess(models)
                : Results.OnFailure<IEnumerable<Resurs>>($"No resursi in prostor with id: {id}");
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Resurs>>(e);
        }
    }

    public Result<IEnumerable<Resurs>> GetByNaziv(string naziv)
    {
        try
        {
            var models = _dbContext.Resursi.AsNoTracking()
                .Include(a => a.IdUdrugeNavigation)
                .Include(a => a.IdProstorNavigation)
                .Where(resurs => resurs.Naziv.Equals(naziv)).AsEnumerable()
                .Select(Mapping.ToDomain);

            return models.Any()
                ? Results.OnSuccess(models)
                : Results.OnFailure<IEnumerable<Resurs>>($"No resursi named: {naziv}.");
        }
        catch (Exception e)
        {
            return Results.OnException<IEnumerable<Resurs>>(e);
        }
    }
}