using BaseLibrary;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using UdrugeApp.Repositories;
using UdrugeWebApi.DTOs;

namespace UdrugeWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResursController : ControllerBase
{
    private readonly IResursRepository _resursRepository;
    private readonly IProstoriRepository _prostoriRepository;
    private readonly IUdrugeRepository _udrugeRepository;

    public ResursController(IResursRepository resursRepository, IProstoriRepository prostoriRepository, IUdrugeRepository udrugeRepository, IHttpClientFactory httpClientFactory)
    {
        _resursRepository = resursRepository;
        _prostoriRepository = prostoriRepository;
        _udrugeRepository = udrugeRepository;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Resurs>> GetAllResursi()
    {
        var resursResults = _resursRepository.GetAll()
            .Map(resurs => resurs.Select(DtoMapping.ToDto));

        return resursResults
            ? Ok(resursResults.Data)
            : Problem(resursResults.Message, statusCode: 500);
    }

    [HttpGet("TrajniResursi")]
    public ActionResult<IEnumerable<TrajniResurs>> GetAllTrajniResursi()
    {
        var resursResults = _resursRepository.GetAllTrajni()
            .Map(resurs => resurs.Select(DtoMapping.ToDto));

        return resursResults
            ? Ok(resursResults.Data)
            : Problem(resursResults.Message, statusCode: 500);
    }
    
    [HttpGet("PotrosniResursi")]
    public ActionResult<IEnumerable<PotrosniResurs>> GetAllPotrosniResursi()
    {
        var resursResults = _resursRepository.GetAllPotrosni()
            .Map(resurs => resurs.Select(DtoMapping.ToDto));

        return resursResults
            ? Ok(resursResults.Data)
            : Problem(resursResults.Message, statusCode: 500);
    }
    
    [HttpGet("Trajni/{id}")]
    public ActionResult<Resurs> GetTrajniResursById(int id)
    {
        var resursResults = _resursRepository.GetTrajni(id).Map(DtoMapping.ToDto);
    
        return resursResults switch
        {
            { IsSuccess: true } => Ok(resursResults.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(resursResults.Message, statusCode: 500)
        };
    }
    
    [HttpGet("Potrosni/{id}")]
    public ActionResult<Resurs> GetPotrosniResursById(int id)
    {
        var resursResults = _resursRepository.GetPotrosni(id).Map(DtoMapping.ToDto);
    
        return resursResults switch
        {
            { IsSuccess: true } => Ok(resursResults.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(resursResults.Message, statusCode: 500)
        };
    }
    
    [HttpPost("PotrosniResursi")]
    public ActionResult<PotrosniResurs> CreatePotrosniResurs(PotrosniResurs resurs)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var udruga = _udrugeRepository.Get(resurs.IdUdruge);
        var prostor = _prostoriRepository.Get(resurs.IdProstor);

        var domainResurs = resurs.ToDomain(udruga.Data, prostor.Data);
 
        var validationResult = domainResurs.IsValid();
        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }
    
        var result =
            domainResurs.IsValid()
            .Bind(() => _resursRepository.Insert(domainResurs));
    
        //Sto radi ovaj createdAtAction??
        return result
            ? CreatedAtAction("GetPotrosniResursById", new { id = resurs.Id }, resurs)
            : Problem(result.Message, statusCode: 500);
    }
    
    [HttpPost("TrajniResursi")]
    public ActionResult<TrajniResurs> CreateTrajniResurs(TrajniResurs resurs)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var udruga = _udrugeRepository.Get(resurs.IdUdruge);
        var prostor = _prostoriRepository.Get(resurs.IdProstor);
        
        var domainResurs = resurs.ToDomain(udruga.Data, prostor.Data);

        Console.WriteLine(resurs.IdProstor);
        Console.WriteLine(domainResurs.Napomena);
        
        var validationResult = domainResurs.IsValid();
        if (!validationResult)
        {
            Console.WriteLine(validationResult.Message);
            return Problem(validationResult.Message, statusCode: 500);
        }
        
        var result =
            domainResurs.IsValid()
                .Bind(() => _resursRepository.Insert(domainResurs));
        
        Console.WriteLine(domainResurs.Prostor);
    
        //Sto radi ovaj createdAtAction??
        return result
            ? CreatedAtAction("GetTrajniResursById", new { id = resurs.Id }, resurs)
            : Problem(result.Message, statusCode: 500);
    }
    
    [HttpPut("TrajniResurs/{id}")]
    public IActionResult EditTrajniResurs(int id, TrajniResurs resurs)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (id != resurs.Id)
        {
            return BadRequest();
        }
        
        if (!_resursRepository.ExistsTrajni(id))
        {
            return NotFound();
        }
    
        var udruga = _udrugeRepository.Get(resurs.IdUdruge);
        var prostor = _prostoriRepository.Get(resurs.IdProstor);
        
        var domainResurs = resurs.ToDomain(udruga.Data, prostor.Data);

        var result =
            domainResurs.IsValid()
            .Bind(() => _resursRepository.Update(domainResurs));
    
        return result
            ? AcceptedAtAction("EditTrajniResurs", resurs)
            : Problem(result.Message, statusCode: 500);
    }
    
    [HttpPut("PotrosniResurs/{id}")]
    public IActionResult EditPotrosniResurs(int id, PotrosniResurs resurs)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Console.WriteLine(resurs.Id);
        Console.WriteLine(id);
        if (id != resurs.Id)
        {
            return BadRequest();
        }
        
        if (!_resursRepository.ExistsPotrosni(id))
        {
            return NotFound();
        }
        var udruga = _udrugeRepository.Get(resurs.IdUdruge);
        var prostor = _prostoriRepository.Get(resurs.IdProstor);
        
        var domainResurs = resurs.ToDomain(udruga.Data, prostor.Data);
    
        var result =
            domainResurs.IsValid()
                .Bind(() => _resursRepository.Update(domainResurs));
        return result
            ? AcceptedAtAction("EditPotrosniResurs", resurs)
            : Problem(result.Message, statusCode: 500);
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteResurs(int id)
    {
        if (!_resursRepository.Exists(id))
            return NotFound();
    
        var deleteResult = _resursRepository.Remove(id);
        return deleteResult
            ? NoContent()
            : Problem(deleteResult.Message, statusCode: 500);
    }

    [HttpPost("PosudiResurs")]
    public ActionResult<String> PosudiResurs(ZahtjevDetails zahtjevDetails)
    {
        var resursi = _resursRepository.PosudiResurs(zahtjevDetails.NazivMaterijalnaPotreba);
        return Ok(resursi.Data);
       
    }

}