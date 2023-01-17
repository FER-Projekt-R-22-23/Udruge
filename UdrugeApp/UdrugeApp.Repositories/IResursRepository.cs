using BaseLibrary;
using UdrugeApp.Domain.Models;

namespace UdrugeApp.Repositories;

public interface IResursRepository : IRepository<int, Resurs>
{
    public Result<TrajniResurs> GetTrajni(int id);
    public Result<PotrosniResurs> GetPotrosni(int id);
    public Result<IEnumerable<PotrosniResurs>> GetAllPotrosni();

    public Result<IEnumerable<TrajniResurs>> GetAllTrajni();

    public Result Insert(PotrosniResurs model);

    public Result Insert(TrajniResurs model);

    public Result Update(PotrosniResurs model);

    public Result Update(TrajniResurs model);

    public bool Exists(TrajniResurs model);

    public bool Exists(PotrosniResurs model);


    public bool ExistsTrajni(int id);
    
    public bool ExistsPotrosni(int id);
    
    public Result<IEnumerable<PotrosniResurs>> GetByRokTrajanja(DateTime rokTrajanja);
    
    public Result<TrajniResurs> GetByInventarniBroj(string inventarniBroj);
    
    public Result<IEnumerable<Resurs>> GetByIdUdruge(int id);
    
    public Result<IEnumerable<Resurs>> GetByIdProstori(int id);
    
    public Result<IEnumerable<Resurs>> GetByNaziv(string naziv);

    public Result<String> PosudiResurs(string naziv);
}