using Microsoft.AspNetCore.Mvc;
using UdrugeWebApi.DTOs;

namespace UdrugeWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClanarineController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public ClanarineController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Clanstvo");
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Clan>>> GetDidntPay()
    {
        var resursResults = await _httpClient.GetFromJsonAsync<IEnumerable<Clan>>("api/Clan/Neplacene");

        return Ok(resursResults);
    }
}