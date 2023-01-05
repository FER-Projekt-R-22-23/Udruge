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
        var clanoviResult = _httpClient.GetFromJsonAsync<IEnumerable<ClanNijePlatio>>("api/Clan/NisuPlatili");

        if (clanoviResult.Result != null)
        {
            var clanovi = clanoviResult.Result.Select(c => ClanNijePlatio.DtoMapping.ToDomain(c));

            return Results.OnSuccess(clanovi);
        }

        return Results.OnFailure<IEnumerable<Clan>>("Clanovi nemaju neplacenih clanarina");
    }
    
}