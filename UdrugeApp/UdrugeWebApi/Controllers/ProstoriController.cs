using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdrugeApp.Data;
using UdrugeApp.Data.DbModels;

namespace UdrugeApp.UdrugeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProstoriController : ControllerBase
    {
        private readonly ExampleContext _context;

        public ProstoriController(ExampleContext context)
        {
            _context = context;
        }

        // GET: api/Prostori
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prostori>>> GetAllProstori()
        {
            return await _context.Prostori.ToListAsync();
        }

        // GET: api/Prostori/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prostori>> GetProstori(int id)
        {
            var Prostori = await _context.Prostori.FindAsync(id);

            if (Prostori == null)
            {
                return NotFound();
            }

            return Prostori;
        }

        // PUT: api/Prostori/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProstori(int id, Prostori Prostori)
        {
            if (id != Prostori.Id)
            {
                return BadRequest();
            }

            _context.Entry(Prostori).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProstoriExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Prostori
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prostori>> CreateProstori(Prostori Prostori)
        {
            _context.Prostori.Add(Prostori);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProstori", new { id = Prostori.Id }, Prostori);
        }

        // DELETE: api/Prostori/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProstori(int id)
        {
            var Prostori = await _context.Prostori.FindAsync(id);
            if (Prostori == null)
            {
                return NotFound();
            }

            _context.Prostori.Remove(Prostori);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProstoriExists(int id)
        {
            return _context.Prostori.Any(e => e.Id == id);
        }
    }
}
