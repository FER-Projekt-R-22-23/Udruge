using BaseLibrary;
using UdrugeApp.Domain.Models;

namespace UdrugeApp.Repositories;

/// <summary>
/// Facade interface for a Person repository
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TDomainModel"></typeparam>
public interface IProstoriRepository : IRepository<int, Prostori>
{

    /// <summary>
    /// Get prostori with the given IDs
    /// </summary>
    /// <param name="ids">Selection IDs</param>
    /// <returns>IEnumerable of Prostori with given IDs</returns>
    Result<IEnumerable<Prostori>> GetByIds(int[] ids);
}