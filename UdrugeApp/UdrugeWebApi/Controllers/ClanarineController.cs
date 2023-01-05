using BaseLibrary;
using Microsoft.AspNetCore.Mvc;
using UdrugeApp.Providers;
using UdrugeApp.Providers.Http.DTOs;
using UdrugeWebApi.DTOs;
using DtoMapping = UdrugeWebApi.DTOs.DtoMapping;

namespace UdrugeWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClanarineController : ControllerBase
{

    private readonly IClanstvoProvider _clanstvoProvider;

    public ClanarineController(IClanstvoProvider clanstvoProvider)
    {
        _clanstvoProvider = clanstvoProvider;
    }
    
    [HttpGet("Neplacene")]
    public ActionResult<IEnumerable<NeplaceneClanarine>> GetNeplaceneClanarine([FromQuery]int[] listOfIds)
    {
        var clanoviResult = _clanstvoProvider.GetDidntPay(listOfIds.ToList())
            .Map(c => c.Select(clanarina => clanarina.ToDto()));
        
        Console.WriteLine(clanoviResult.Data);
        
        return clanoviResult
            ? Ok(clanoviResult.Data)
            : Problem(clanoviResult.Message, statusCode: 500);
    }
    

}