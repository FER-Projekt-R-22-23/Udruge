using Microsoft.AspNetCore.Mvc;
using UdrugeApp.Repositories;
using UdrugeWebApi.DTOs;
using BaseLibrary;

namespace UdrugeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProstoriController : ControllerBase
    {
        private readonly IProstoriRepository _ProstoriRepository;

        public ProstoriController(IProstoriRepository context)
        {
            _ProstoriRepository = context;
        }

        // GET: api/Prostori
        [HttpGet]
        public ActionResult<IEnumerable<Prostori>> GetProstori([FromQuery]int[] ids)
        {
            var ProstoriResults = 
                (ids.Length == 0
                    ? _ProstoriRepository.GetAll()
                    : _ProstoriRepository.GetByIds(ids))
                .Map(Prostori => Prostori.Select(DtoMapping.ToDto));

            return ProstoriResults
                ? Ok(ProstoriResults.Data)
                : Problem(ProstoriResults.Message, statusCode: 500);
        }

        // GET: api/Prostori/5
        [HttpGet("{id}")]
        public ActionResult<Prostori> GetProstori(int id)
        {
            var ProstoriResult = _ProstoriRepository.Get(id).Map(DtoMapping.ToDto);

            return ProstoriResult switch
            {
                { IsSuccess: true } => Ok(ProstoriResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(ProstoriResult.Message, statusCode: 500)
            };
        }

        // PUT: api/Prostori/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult EditProstori(int id, Prostori Prostori)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != Prostori.Id)
            {
                return BadRequest();
            }

            if (!_ProstoriRepository.Exists(id))
            {
                return NotFound();
            }

            var domainProstori = Prostori.ToDomain();

            var result =
                domainProstori.IsValid()
                .Bind(() => _ProstoriRepository.Update(domainProstori));

            return result
                ? AcceptedAtAction("EditProstori", Prostori)
                : Problem(result.Message, statusCode: 500);
        }

        // POST: api/Prostori
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Prostori> CreateProstori(Prostori Prostori)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainProstori = Prostori.ToDomain();

            var result = 
                domainProstori.IsValid()
                .Bind(() => _ProstoriRepository.Insert(domainProstori));

            return result
                ? CreatedAtAction("GetProstori", new { id = Prostori.Id }, Prostori)
                : Problem(result.Message, statusCode: 500);
        }

        // DELETE: api/Prostori/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProstori(int id)
        {
            if (!_ProstoriRepository.Exists(id))
                return NotFound();

            var deleteResult = _ProstoriRepository.Remove(id);
            return deleteResult
                ? NoContent()
                : Problem(deleteResult.Message, statusCode: 500);
        }
    }
}