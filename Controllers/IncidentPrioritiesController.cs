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
    public class IncidentPrioritiesController : ControllerBase
    {
        private readonly voicemsgitsmContext _context;

        public IncidentPrioritiesController(voicemsgitsmContext context)
        {
            _context = context;
        }

        // GET: api/IncidentPriorities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentPriority>>> GetIncidentPriorities()
        {
            return await _context.IncidentPriorities.ToListAsync();
        }

        // GET: api/IncidentPriorities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncidentPriority>> GetIncidentPriority(int id)
        {
            var incidentPriority = await _context.IncidentPriorities.FindAsync(id);

            if (incidentPriority == null)
            {
                return NotFound();
            }

            return incidentPriority;
        }

        // PUT: api/IncidentPriorities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncidentPriority(int id, IncidentPriority incidentPriority)
        {
            if (id != incidentPriority.PriId)
            {
                return BadRequest();
            }

            _context.Entry(incidentPriority).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentPriorityExists(id))
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

        // POST: api/IncidentPriorities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IncidentPriority>> PostIncidentPriority(IncidentPriority incidentPriority)
        {
            _context.IncidentPriorities.Add(incidentPriority);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncidentPriority", new { id = incidentPriority.PriId }, incidentPriority);
        }

        // DELETE: api/IncidentPriorities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IncidentPriority>> DeleteIncidentPriority(int id)
        {
            var incidentPriority = await _context.IncidentPriorities.FindAsync(id);
            if (incidentPriority == null)
            {
                return NotFound();
            }

            _context.IncidentPriorities.Remove(incidentPriority);
            await _context.SaveChangesAsync();

            return incidentPriority;
        }

        private bool IncidentPriorityExists(int id)
        {
            return _context.IncidentPriorities.Any(e => e.PriId == id);
        }
    }
}
