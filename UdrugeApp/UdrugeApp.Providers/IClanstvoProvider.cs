using BaseLibrary;
using UdrugeApp.Domain.Models;

namespace UdrugeApp.Providers;

public interface IClanstvoProvider
{
    public Task<Result<IEnumerable<Clan>>> GetDidntPay(IEnumerable<int> ids);
    
    public Task<Result<IEnumerable<Clan>>> GetRangovi(IEnumerable<int> ids);
}