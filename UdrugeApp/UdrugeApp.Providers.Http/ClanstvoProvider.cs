using System.Net.Http.Json;
using BaseLibrary;
using UdrugeApp.Domain.Models;
using UdrugeApp.Providers.Http.DTOs;

namespace UdrugeApp.Providers.Http;

public class ClanstvoProvider : IClanstvoProvider 
{
    private readonly HttpClient _httpClient;

    public ClanstvoProvider(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Clanstvo");
    }
    
    public Result<IEnumerable<Clan>> GetDidntPay(IEnumerable<int> ids)
    {

        string IdsU = String.Empty;
        
        foreach (var id in ids)
        {
            if (IdsU.Equals(String.Empty))
            {
                IdsU += "ids="+id;
            }
            IdsU += "&ids="+id;
        }
        
        var clanoviResult = _httpClient.GetFromJsonAsync<IEnumerable<ClanNijePlatio>>($"api/Clan/NisuPlatili?{IdsU}");

        if (clanoviResult.Result!.Any())
        {
            var clanovi = clanoviResult.Result!.Select(c => ClanNijePlatio.DtoMapping.ToDomain(c));

            return Results.OnSuccess(clanovi);
        }

        return Results.OnFailure<IEnumerable<Clan>>("Clanovi ne postoje");
    }

    public Result<IEnumerable<Clan>> GetRangovi(IEnumerable<int> ids)
    {
        string IdsU = String.Empty;
        
        foreach (var id in ids)
        {
            if (IdsU.Equals(String.Empty))
            {
                IdsU += "ids="+id;
            }
            IdsU += "&ids="+id;
        }
        
        var clanoviResult = _httpClient.GetFromJsonAsync<IEnumerable<ClanRangZasluga>>($"api/Clan/RangoviZasluga?{IdsU}");

        if (clanoviResult.Result!.Any())
        {
            var clanovi = clanoviResult.Result!.Select(c => DtoMapping.ToDomain(c));

            return Results.OnSuccess(clanovi);
        }

        return Results.OnFailure<IEnumerable<Clan>>("Clanovi ne postoje");
    }
}