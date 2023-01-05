using BaseLibrary;
using UdrugeApp.Domain.Models;

namespace UdrugeApp.Providers;

public interface IClanstvoProvider
{
    public Result<IEnumerable<Clan>> GetDidntPay(IEnumerable<int> ids);
    
    public Result<IEnumerable<Clan>> GetRangovi(IEnumerable<int> ids);
}