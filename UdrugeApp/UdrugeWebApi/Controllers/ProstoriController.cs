using Microsoft.AspNetCore.Mvc;
using UdrugeApp.Repositories;
using UdrugeWebApi.DTOs;
using BaseLibrary;
using Microsoft.EntityFrameworkCore;


namespace UdrugeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProstoriController : ControllerBase
    {
        private readonly IProstoriRepository _prostoriRepository;

        public ProstoriController(IProstoriRepository context)
        {
            _prostoriRepository = context;
        }

        // GET: api/Prostori
        [HttpGet]
        public ActionResult<IEnumerable<Prostori>> GetProstori([FromQuery]int[] ids)
        {
            var prostoriResults = 
                (ids.Length == 0
                    ? _prostoriRepository.GetAll()
                    : _prostoriRepository.GetByIds(ids))
                .Map(prostori => prostori.Select(DtoMapping.ToDto));

            return prostoriResults
                ? Ok(prostoriResults.Data)
                : Problem(prostoriResults.Message, statusCode: 500);
        }

        // GET: api/Prostori/5
        [HttpGet("{id}")]
        public ActionResult<Prostori> GetProstori(int id)
        {
            var prostoriResult = _prostoriRepository.Get(id).Map(DtoMapping.ToDto);

            return prostoriResult switch
            {
                { IsSuccess: true } => Ok(prostoriResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(prostoriResult.Message, statusCode: 500)
            };
        }

        // PUT: api/Prostori/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult EditProstori(int id, Prostori prostori)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != prostori.Id)
            {
                return BadRequest();
            }

            if (!_prostoriRepository.Exists(id))
            {
                return NotFound();
            }

            var domainProstori = prostori.ToDomain();

            var result =
                domainProstori.IsValid()
                .Bind(() => _prostoriRepository.Update(domainProstori));

            return result
                ? AcceptedAtAction("EditProstori", prostori)
                : Problem(result.Message, statusCode: 500);
        }

        // POST: api/Prostori
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Prostori> CreateProstori(Prostori prostori)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainProstori = prostori.ToDomain();

            var result = 
                domainProstori.IsValid()
                .Bind(() => _prostoriRepository.Insert(domainProstori));

            return result
                ? CreatedAtAction("GetProstori", new { id = prostori.Id }, prostori)
                : Problem(result.Message, statusCode: 500);
        }

        // DELETE: api/Prostori/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProstori(int id)
        {
            if (!_prostoriRepository.Exists(id))
                return NotFound();

            var deleteResult = _prostoriRepository.Remove(id);
            return deleteResult
                ? NoContent()
                : Problem(deleteResult.Message, statusCode: 500);
        }
    }
}