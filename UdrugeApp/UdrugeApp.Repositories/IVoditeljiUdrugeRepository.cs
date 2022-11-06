using UdrugeApp.Domain.Models;

namespace UdrugeApp.Repositories;

/// <summary>
/// Facade interface for a Person repository
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TDomainModel"></typeparam>
public interface IVoditeljiUdrugeRepository : IRepository<int, VoditeljiUdruge>
{
}