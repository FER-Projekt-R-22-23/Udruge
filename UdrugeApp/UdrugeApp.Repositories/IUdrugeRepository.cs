using UdrugeApp.Domain.Models;

namespace UdrugeApp.Repositories;

/// <summary>
/// Facade interface for a Udruge repository
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TDomainModel"></typeparam>

    public interface IUdrugeRepository
        : IRepository<int, Udruge>,
          IAggregateRepository<int, Udruge>


    {
    }
