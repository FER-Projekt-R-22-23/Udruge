using BaseLibrary;
using UdrugeApp.Domain.Models;

namespace UdrugeApp.Repositories;

public interface IPotrosniResursRepository : IRepository<int, PotrosniResurs>
{
    //napravit dohvacanje do odredenog roka trajanja? ili da ih sortira po roku trajanja?

    public Result<IEnumerable<PotrosniResurs>> GetByUdruge(Udruge udruga);
    
    public Result<IEnumerable<PotrosniResurs>> GetByProstori(Prostori prostor);
    
    public Result<IEnumerable<PotrosniResurs>> GetByNaziv(string naziv);

}