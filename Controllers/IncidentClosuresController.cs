using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ItstmVoiceMessages.Models;

namespace ItstmVoiceMessages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentClosuresController : ControllerBase
    {
        private readonly voicemsgitsmContext _context;

        public IncidentClosuresController(voicemsgitsmContext context)
        {
            _context = context;
        }

        // GET: api/IncidentClosures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentClosure>>> GetIncidentClosures()
        {
            return await _context.IncidentClosures.ToListAsync();
        }

        // GET: api/IncidentClosures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncidentClosure>> GetIncidentClosure(int id)
        {
            var incidentClosure = await _context.IncidentClosures.FindAsync(id);

            if (incidentClosure == null)
            {
                return NotFound();
            }

            return incidentClosure;
        }

        // PUT: api/IncidentClosures/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncidentClosure(int id, IncidentClosure incidentClosure)
        {
            if (id != incidentClosure.CloId)
            {
                return BadRequest();
            }

            _context.Entry(incidentClosure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentClosureExists(id))
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

        // POST: api/IncidentClosures
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IncidentClosure>> PostIncidentClosure(IncidentClosure incidentClosure)
        {
            _context.IncidentClosures.Add(incidentClosure);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncidentClosure", new { id = incidentClosure.CloId }, incidentClosure);
        }

        // DELETE: api/IncidentClosures/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IncidentClosure>> DeleteIncidentClosure(int id)
        {
            var incidentClosure = await _context.IncidentClosures.FindAsync(id);
            if (incidentClosure == null)
            {
                return NotFound();
            }

            _context.IncidentClosures.Remove(incidentClosure);
            await _context.SaveChangesAsync();

            return incidentClosure;
        }

        private bool IncidentClosureExists(int id)
        {
            return _context.IncidentClosures.Any(e => e.CloId == id);
        }
    }
}
