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
    public class IncidentLoggingsController : ControllerBase
    {
        private readonly voicemsgitsmContext _context;

        public IncidentLoggingsController(voicemsgitsmContext context)
        {
            _context = context;
        }

        // GET: api/IncidentLoggings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentLogging>>> GetIncidentLoggings()
        {
            return await _context.IncidentLoggings.ToListAsync();
        }

        // GET: api/IncidentLoggings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncidentLogging>> GetIncidentLogging(int id)
        {
            var incidentLogging = await _context.IncidentLoggings.FindAsync(id);

            if (incidentLogging == null)
            {
                return NotFound();
            }

            return incidentLogging;
        }

        // PUT: api/IncidentLoggings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncidentLogging(int id, IncidentLogging incidentLogging)
        {
            if (id != incidentLogging.LogId)
            {
                return BadRequest();
            }

            _context.Entry(incidentLogging).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentLoggingExists(id))
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

        // POST: api/IncidentLoggings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IncidentLogging>> PostIncidentLogging(IncidentLogging incidentLogging)
        {
            _context.IncidentLoggings.Add(incidentLogging);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncidentLogging", new { id = incidentLogging.LogId }, incidentLogging);
        }

        // DELETE: api/IncidentLoggings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IncidentLogging>> DeleteIncidentLogging(int id)
        {
            var incidentLogging = await _context.IncidentLoggings.FindAsync(id);
            if (incidentLogging == null)
            {
                return NotFound();
            }

            _context.IncidentLoggings.Remove(incidentLogging);
            await _context.SaveChangesAsync();

            return incidentLogging;
        }

        private bool IncidentLoggingExists(int id)
        {
            return _context.IncidentLoggings.Any(e => e.LogId == id);
        }
    }
}
