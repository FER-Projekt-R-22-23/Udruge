using ExampleApp.Repositories;
using Microsoft.AspNetCore.Mvc;
using UdrugeWebApi.DTOs;
using UdrugeApp.Repositories.SqlServer;
using UdrugeApp.Repositories;
using BaseLibrary;
using System.Data;
using System;
namespace UdrugeWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VoditeljiUdrugeController : ControllerBase
    {

        private readonly IVoditeljiUdrugeRepository _VoditeljiUdrugeRepository;

        public VoditeljiUdrugeController(IVoditeljiUdrugeRepository context)
        {
            _VoditeljiUdrugeRepository = context;
        }

        // GET: api/VoditeljiUdruge/5
        [HttpGet("{id}")]
        public ActionResult<VoditeljiUdruge> GetVoditeljiUdruge(int id)
        {
            var VoditeljiUdrugeResult = _VoditeljiUdrugeRepository.Get(id).Map(DtoMapping.ToDto);

            return VoditeljiUdrugeResult switch
            {
                { IsSuccess: true } => Ok(VoditeljiUdrugeResult.Data),
                { IsFailure: true } => NotFound(),
                { IsException: true } or _ => Problem(VoditeljiUdrugeResult.Message, statusCode: 500)
            };
        }

        // PUT: api/VoditeljiUdruge/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult EditVoditeljiUdruge(int id, VoditeljiUdruge VoditeljiUdruge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != VoditeljiUdruge._IdUdruge)
            {
                return BadRequest();
            }

            if (!_VoditeljiUdrugeRepository.Exists(id))
            {
                return NotFound();
            }

            var domainVoditeljiUdruge = VoditeljiUdruge.ToDomain();

            var result =
                domainVoditeljiUdruge.IsValid()
                .Bind(() => _VoditeljiUdrugeRepository.Update(domainVoditeljiUdruge));

            return result
                ? AcceptedAtAction("EditVoditeljiUdruge", VoditeljiUdruge)
                : Problem(result.Message, statusCode: 500);
        }

        // POST: api/VoditeljiUdruge
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<VoditeljiUdruge> CreateVoditeljiUdruge(VoditeljiUdruge voditeljiUdruge)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var domainVoditeljiUdruge = voditeljiUdruge.ToDomain();

            var result =
                domainVoditeljiUdruge.IsValid()
                .Bind(() => _VoditeljiUdrugeRepository.Insert(domainVoditeljiUdruge));

            return result
                ? CreatedAtAction("GetVoditeljiUdruge", new { id = voditeljiUdruge._IdUdruge }, voditeljiUdruge)
                : Problem(result.Message, statusCode: 500);
        }


        // DELETE: api/VoditeljiUdruge/5
        [HttpDelete("{id}")]
        public IActionResult DeleteVoditeljiUdruge(int id)
        {
            if (!_VoditeljiUdrugeRepository.Exists(id))
                return NotFound();

            var deleteResult = _VoditeljiUdrugeRepository.Remove(id);
            return deleteResult
                ? NoContent()
                : Problem(deleteResult.Message, statusCode: 500);
        }
    }
}