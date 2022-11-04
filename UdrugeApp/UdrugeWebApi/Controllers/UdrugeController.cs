using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdrugeWebApi.Data;
using UdrugeWebApi.Data.DbModels;

namespace UdrugeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UdrugeController : ControllerBase
    {
        private readonly UdrugeContext _context;

        public UdrugeController(UdrugeContext context)
        {
            _context = context;
        }

        // GET: api/Udruge
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Udruge>>> GetUdruge()
        {
            return Ok(await _context.Udruge.ToListAsync());
        }

        // // GET: api/Udruge/5
        // [HttpGet("{id}")]
        // public async Task<ActionResult<Udruge>> GetUdruge(int id)
        // {
        //     var udruge = await _context.Udruge.FindAsync(id);
        //
        //     if (udruge == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return udruge;
        // }
        //
        // // PUT: api/Udruge/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutUdruge(int id, Udruge udruge)
        // {
        //     if (id != udruge.IdUdruge)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(udruge).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!UdrugeExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        //
        //     return NoContent();
        // }
        //
        // // POST: api/Udruge
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Udruge>> PostUdruge(Udruge udruge)
        // {
        //     _context.Udruge.Add(udruge);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetUdruge", new { id = udruge.IdUdruge }, udruge);
        // }
        //
        // // DELETE: api/Udruge/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteUdruge(int id)
        // {
        //     var udruge = await _context.Udruge.FindAsync(id);
        //     if (udruge == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.Udruge.Remove(udruge);
        //     await _context.SaveChangesAsync();
        //
        //     return NoContent();
        // }
        //
        // private bool UdrugeExists(int id)
        // {
        //     return _context.Udruge.Any(e => e.IdUdruge == id);
        // }
    }
}
