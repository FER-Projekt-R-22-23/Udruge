using BaseLibrary;
using UdrugeApp.Domain.Models;

namespace UdrugeApp.Repositories;

public interface ITrajniResursRepository : IRepository<int, TrajniResurs>
{ 
    
    //Ima li smisla ovdje dohvacanje po dostupnosti ili je bolje to filtrirat nakon dohvacanja?

    public Result<TrajniResurs> GetByInventarniBroj(string inventarniBroj);
    
    public Result<IEnumerable<TrajniResurs>> GetByUdruge(Udruge udruga);

    public Result<IEnumerable<TrajniResurs>> GetByProstor(Prostori prostor);

    public Result<IEnumerable<TrajniResurs>> GetByNaziv(string naziv);
}