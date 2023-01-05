using BaseLibrary;
using Microsoft.AspNetCore.Mvc;
using UdrugeApp.Providers;
using UdrugeWebApi.DTOs;

namespace UdrugeWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClanstvoController : ControllerBase
{

    private readonly IClanstvoProvider _clanstvoProvider;

    public ClanstvoController(IClanstvoProvider clanstvoProvider)
    {
        _clanstvoProvider = clanstvoProvider;
    }
    
    [HttpGet("Neplacene")]
    public ActionResult<IEnumerable<NeplaceneClanarine>> GetNeplaceneClanarine([FromQuery]int[] listOfIds)
    {
        var clanoviResult = _clanstvoProvider.GetDidntPay(listOfIds.ToList())
            .Map(c => c.Select(clanarina => clanarina.ToDto()));

        return clanoviResult
            ? Ok(clanoviResult.Data)
            : Problem(clanoviResult.Message, statusCode: 404);
    }
    
    [HttpGet("Rangovi")]
    public ActionResult<IEnumerable<ClanoviRang>> GetRangovi([FromQuery]int[] listOfIds)
    {
        var clanoviResult = _clanstvoProvider.GetRangovi(listOfIds.ToList())
            .Map(c => c.Select(clanarina => clanarina.ToRangDto()));

        return clanoviResult
            ? Ok(clanoviResult.Data)
            : Problem(clanoviResult.Message, statusCode: 404);
    }
    
}