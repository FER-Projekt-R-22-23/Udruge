using BaseLibrary;
using Microsoft.AspNetCore.Mvc;
using UdrugeApp.Repositories;
using UdrugeWebApi.DTOs;

namespace UdrugeWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResursController : ControllerBase
{

    private readonly IPotrosniResursRepository _potrosniResursRepository;
    private readonly ITrajniResursRepository _trajniResursRepository;

    public ResursController(IPotrosniResursRepository potrosniResursRepository, ITrajniResursRepository trajniResursRepository)
    {
        _potrosniResursRepository = potrosniResursRepository;
        _trajniResursRepository = trajniResursRepository;
    }
    
    [HttpGet("TrajniResursi")]
    public ActionResult<IEnumerable<TrajniResurs>> GetAllTrajniResursi()
    {
        var resursResults = _trajniResursRepository.GetAll()
            .Map(resurs => resurs.Select(DtoMapping.ToDto));

        return resursResults
            ? Ok(resursResults.Data)
            : Problem(resursResults.Message, statusCode: 500);
    }
    
    [HttpGet("PotrosniResursi")]
    public ActionResult<IEnumerable<PotrosniResurs>> GetAllPotrosniResursi()
    {
        var resursResults = _potrosniResursRepository.GetAll()
            .Map(resurs => resurs.Select(DtoMapping.ToDto));

        return resursResults
            ? Ok(resursResults.Data)
            : Problem(resursResults.Message, statusCode: 500);
    }
    
    [HttpGet("TrajniResurs/{id}")]
    public ActionResult<TrajniResurs> GetTrajniResurs(int id)
    {
        var resursResults = _trajniResursRepository.Get(id).Map(DtoMapping.ToDto);
    
        return resursResults switch
        {
            { IsSuccess: true } => Ok(resursResults.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(resursResults.Message, statusCode: 500)
        };
    }
    
    [HttpGet("PotrosniResurs/{id}")]
    public ActionResult<PotrosniResurs> GetPotrosniResurs(int id)
    {
        var resursResults = _potrosniResursRepository.Get(id).Map(DtoMapping.ToDto);
    
        return resursResults switch
        {
            { IsSuccess: true } => Ok(resursResults.Data),
            { IsFailure: true } => NotFound(),
            { IsException: true } or _ => Problem(resursResults.Message, statusCode: 500)
        };
    }
    
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("PotrosniResursi")]
    public ActionResult<PotrosniResurs> CreatePotrosniResurs(PotrosniResurs resurs)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
    
        var domainResurs = resurs.ToDomain();
    
        var validationResult = domainResurs.IsValid();
        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }
    
        var result =
            domainResurs.IsValid()
            .Bind(() => _potrosniResursRepository.Insert(domainResurs));
    
        //Sto radi ovaj createdAtAction??
        return result
            ? CreatedAtAction("GetPotrosniResurs", new { id = resurs.Id }, resurs)
            : Problem(result.Message, statusCode: 500);
    }
    
    [HttpPost("TrajniResursi")]
    public ActionResult<TrajniResurs> CreateTrajniResurs(TrajniResurs resurs)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
    
        var domainResurs = resurs.ToDomain();
    
        var validationResult = domainResurs.IsValid();
        if (!validationResult)
        {
            return Problem(validationResult.Message, statusCode: 500);
        }
    
        var result =
            domainResurs.IsValid()
                .Bind(() => _trajniResursRepository.Insert(domainResurs));
    
        //Sto radi ovaj createdAtAction??
        return result
            ? CreatedAtAction("GetTrajniResurs", new { id = resurs.Id }, resurs)
            : Problem(result.Message, statusCode: 500);
    }
    
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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
    
        if (!_trajniResursRepository.Exists(id))
        {
            return NotFound();
        }
    
        var domainResurs = resurs.ToDomain();
        
        //Jel ovo ok ovako ili se to treba bolje napravit?
        // domainResurs.Udruga = _potrosniResursRepository.Get(domainResurs.Id).Data.Udruga;
    
        var result =
            domainResurs.IsValid()
            .Bind(() => _trajniResursRepository.Update(domainResurs));
    
        return result
            ? AcceptedAtAction("EditTrajniResurs", resurs)
            : Problem(result.Message, statusCode: 500);
    }
    
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("PotrosniResurs/{id}")]
    public IActionResult EditPotrosniResurs(int id, PotrosniResurs resurs)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
    
        if (id != resurs.Id)
        {
            return BadRequest();
        }
    
        if (!_potrosniResursRepository.Exists(id))
        {
            return NotFound();
        }
    
        var domainResurs = resurs.ToDomain();
    
        var result =
            domainResurs.IsValid()
                .Bind(() => _potrosniResursRepository.Update(domainResurs));
    
        return result
            ? AcceptedAtAction("EditPotrosniResurs", resurs)
            : Problem(result.Message, statusCode: 500);
    }
    
    [HttpPut("TrajniResurs/ChangeAvailability/{id}")]
    public IActionResult ChangeAvailability(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var resursToChangeResult = _trajniResursRepository.Get(id);
        
        if (resursToChangeResult.IsFailure)
        {
            return NotFound();
        }

        var resursToChange = resursToChangeResult.Data;
        resursToChange.ChangeAvailability();

        var result =
            resursToChange.IsValid()
                .Bind(() => _trajniResursRepository.Update(resursToChange));
    
        return result
            ? AcceptedAtAction("ChangeAvailability", resursToChange.JeDostupno)
            : Problem(result.Message, statusCode: 500);
    }

    // [HttpPost("AssignToRole/{personId}")]
    // public IActionResult AssignPersonToRole(int personId, RoleAssignment roleAssignment)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //
    //     var personResult = _potrosniResursRepository.GetAggregate(personId);
    //     if (personResult.IsFailure)
    //     {
    //         return NotFound();
    //     }
    //     if (personResult.IsException)
    //     {
    //         return Problem(personResult.Message, statusCode: 500);
    //     }
    //
    //     var person = personResult.Data;
    //
    //     var domainRoleAssignment = roleAssignment.ToDomain(personId);
    //     var validationResult = domainRoleAssignment.IsValid();
    //
    //     if (!validationResult)
    //     {
    //         return Problem(validationResult.Message, statusCode: 500);
    //     }
    //
    //     person.AssignRole(domainRoleAssignment);
    //
    //     var updateResult =
    //         person.IsValid()
    //         .Bind(() => _potrosniResursRepository.UpdateAggregate(person));
    //
    //     return updateResult
    //         ? Accepted()
    //         : Problem(updateResult.Message, statusCode: 500);
    // }
    //
    // [HttpPost("DismissFromRole/{personId}")]
    // public IActionResult DismissPersonFromRole(int personId, Role role)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //
    //     var personResult = _potrosniResursRepository.GetAggregate(personId);
    //     if (personResult.IsFailure)
    //     {
    //         return NotFound();
    //     }
    //     if (personResult.IsException)
    //     {
    //         return Problem(personResult.Message, statusCode: 500);
    //     }
    //
    //     var person = personResult.Data;
    //
    //     var domainRole = role.ToDomain();
    //
    //     if (!person.DismissFromRole(domainRole))
    //     {
    //         return NotFound($"Couldn't find role {role.Name} on person");
    //     }
    //
    //     var updateResult =
    //         person.IsValid()
    //         .Bind(() => _potrosniResursRepository.UpdateAggregate(person));
    //
    //     return updateResult
    //         ? Accepted()
    //         : Problem(updateResult.Message, statusCode: 500);
    // }
    //
    // // PUT: api/People/5
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // [HttpPut("{id}")]
    // public IActionResult EditPerson(int id, Person person)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //
    //     if (id != person.Id)
    //     {
    //         return BadRequest();
    //     }
    //
    //     if (!_potrosniResursRepository.Exists(id))
    //     {
    //         return NotFound();
    //     }
    //
    //     var domainPerson = person.ToDomain();
    //
    //     var result =
    //         domainPerson.IsValid()
    //         .Bind(() => _potrosniResursRepository.Update(domainPerson));
    //
    //     return result
    //         ? AcceptedAtAction("EditPerson", person)
    //         : Problem(result.Message, statusCode: 500);
    // }
    //
    // // POST: api/People
    // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // [HttpPost]
    // public ActionResult<Person> CreatePerson(Person person)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //
    //     var domainPerson = person.ToDomain();
    //
    //     var validationResult = domainPerson.IsValid();
    //     if (!validationResult)
    //     {
    //         return Problem(validationResult.Message, statusCode: 500);
    //     }
    //
    //     var result =
    //         domainPerson.IsValid()
    //         .Bind(() => _potrosniResursRepository.Insert(domainPerson));
    //
    //     return result
    //         ? CreatedAtAction("GetPerson", new { id = person.Id }, person)
    //         : Problem(result.Message, statusCode: 500);
    // }
    //
    // // DELETE: api/People/5
    // [HttpDelete("{id}")]
    // public IActionResult DeletePerson(int id)
    // {
    //     if (!_potrosniResursRepository.Exists(id))
    //         return NotFound();
    //
    //     var deleteResult = _potrosniResursRepository.Remove(id);
    //     return deleteResult
    //         ? NoContent()
    //         : Problem(deleteResult.Message, statusCode: 500);
    // }
}