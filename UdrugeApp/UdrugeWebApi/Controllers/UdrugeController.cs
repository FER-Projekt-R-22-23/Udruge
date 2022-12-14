using ExampleApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using UdrugeWebApi.DTOs;
using UdrugeApp.Repositories.SqlServer;
using UdrugeApp.Repositories;
using BaseLibrary;
using System.Data;
using System;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExampleWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UdrugeController : ControllerBase
{
    private readonly IUdrugeRepository _udrugeRepository;

    public UdrugeController(IUdrugeRepository udrugeRepository)
    {
        _udrugeRepository = udrugeRepository;
    }

    // GET: api/Udruge
    [HttpGet]
    public ActionResult<IEnumerable<Udruge>> GetAllUdruge()
    {
        var udrugeResults = _udrugeRepository.GetAll()
            .Map(udruge => udruge.Select(DtoMapping.ToDto));

        return udrugeResults
            ? Ok(udrugeResults.Data)
            : Problem(udrugeResults.Message, statusCode: 500);
    }

    // GET: api/Udruge/5
    [HttpGet("{id}")]
    public ActionResult<Udruge> GetUdruge(int id)
    {
        var udrugeResult = _udrugeRepository.Get(id).Map(DtoMapping.ToDto);

        return udrugeResult switch
        {
            { IsSuccess: true } => Ok(udrugeResult.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(udrugeResult.Message, statusCode: 500)
        };
    }


    // PUT: api/Udruge/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public IActionResult EditUdruge(int id, Udruge udruge)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != udruge.IdUdruge)
        {
            return BadRequest();
        }

        if (!_udrugeRepository.Exists(id))
        {
            return NotFound();
        }

        var domainUdruge = udruge.ToDomain();

        var result =
            domainUdruge.IsValid()
            .Bind(() => _udrugeRepository.Update(domainUdruge));

        return result
            ? AcceptedAtAction("EditUdruge", udruge)
            : Problem(result.Message, statusCode: 500);
    }

    // POST: api/Udruge
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public ActionResult<Udruge> CreateUdruge([FromBody] Udruge udruge)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var domainUdruge = udruge.ToDomain();

        var validationResult = domainUdruge.IsValid();
        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }

        var result =
            domainUdruge.IsValid()
            .Bind(() => _udrugeRepository.Insert(domainUdruge));

        return result
            ? CreatedAtAction("PostUdruge", new { id = udruge.IdUdruge}, udruge)
            : Problem(result.Message, statusCode: 500);
    }

    // DELETE: api/Udruge/5
    [HttpDelete("{id}")]
    public IActionResult DeleteUdruge(int id)
    {
        if (!_udrugeRepository.Exists(id))
            return NotFound();

        var deleteResult = _udrugeRepository.Remove(id);
        return deleteResult
            ? NoContent()
            : Problem(deleteResult.Message, statusCode: 500);
    }
}
