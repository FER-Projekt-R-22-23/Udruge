using System.Net.Http.Json;
using BaseLibrary;
using Microsoft.Extensions.Http;
using UdrugeApp.Domain.Models;
using UdrugeApp.Providers.Http.DTOs;

namespace UdrugeApp.Providers.Http;

public class ClanstvoProvider : IClanstvoProvider 
{
    private readonly ClanstvoProviderOptions _options;

    public ClanstvoProvider(ClanstvoProviderOptions clanstvoProviderOptions)
    {
        _options = clanstvoProviderOptions;
    }
    
    public async Task<Result<IEnumerable<Clan>>> GetDidntPay(IEnumerable<int> ids)
    {

        string idsU = String.Empty;
        
        foreach (var id in ids)
        {
            if (idsU.Equals(String.Empty))
            {
                idsU += "ids="+id;
            }
            idsU += "&ids="+id;
        }

        IEnumerable<ClanNijePlatio>? clanoviResult;
        
        using (var httpClientHandler = new HttpClientHandler())
        {
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
            using (var httpClient = new HttpClient(httpClientHandler))
            {
                httpClient.BaseAddress = new Uri(_options.BaseUrl);
                clanoviResult = await httpClient.GetFromJsonAsync<IEnumerable<ClanNijePlatio>>($"api/Clan/NisuPlatili?{idsU}");
            }
        }
        
        if (clanoviResult!.Any())
        {
            var clanovi = clanoviResult!.Select(c => ClanNijePlatio.DtoMapping.ToDomain(c));

            return Results.OnSuccess(clanovi);
        }

        return Results.OnFailure<IEnumerable<Clan>>("Clanovi ne postoje");
    }

    public async Task<Result<IEnumerable<Clan>>> GetRangovi(IEnumerable<int> ids)
    {
        string idsU = String.Empty;
        
        foreach (var id in ids)
        {
            if (idsU.Equals(String.Empty))
            {
                idsU += "ids="+id;
            }
            idsU += "&ids="+id;
        }
        
        IEnumerable<ClanRangZasluga>? clanoviResult;
        
        using (var httpClientHandler = new HttpClientHandler())
        {
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;
            using (var httpClient = new HttpClient(httpClientHandler))
            {
                httpClient.BaseAddress = new Uri(_options.BaseUrl);
                clanoviResult = await httpClient.GetFromJsonAsync<IEnumerable<ClanRangZasluga>>($"api/Clan/RangoviZasluga?{idsU}");
            }
        }


        if (clanoviResult!.Any())
        {
            var clanovi = clanoviResult!.Select(c => DtoMapping.ToDomain(c));

            return Results.OnSuccess(clanovi);
        }

        return Results.OnFailure<IEnumerable<Clan>>("Clanovi ne postoje");
    }

}