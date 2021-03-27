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
    public class IncidentResolutionsController : ControllerBase
    {
        private readonly voicemsgitsmContext _context;

        public IncidentResolutionsController(voicemsgitsmContext context)
        {
            _context = context;
        }

        // GET: api/IncidentResolutions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentResolution>>> GetIncidentResolutions()
        {
            return await _context.IncidentResolutions.ToListAsync();
        }

        // GET: api/IncidentResolutions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncidentResolution>> GetIncidentResolution(int id)
        {
            var incidentResolution = await _context.IncidentResolutions.FindAsync(id);

            if (incidentResolution == null)
            {
                return NotFound();
            }

            return incidentResolution;
        }

        // PUT: api/IncidentResolutions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncidentResolution(int id, IncidentResolution incidentResolution)
        {
            if (id != incidentResolution.ResId)
            {
                return BadRequest();
            }

            _context.Entry(incidentResolution).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentResolutionExists(id))
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

        // POST: api/IncidentResolutions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IncidentResolution>> PostIncidentResolution(IncidentResolution incidentResolution)
        {
            _context.IncidentResolutions.Add(incidentResolution);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncidentResolution", new { id = incidentResolution.ResId }, incidentResolution);
        }

        // DELETE: api/IncidentResolutions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IncidentResolution>> DeleteIncidentResolution(int id)
        {
            var incidentResolution = await _context.IncidentResolutions.FindAsync(id);
            if (incidentResolution == null)
            {
                return NotFound();
            }

            _context.IncidentResolutions.Remove(incidentResolution);
            await _context.SaveChangesAsync();

            return incidentResolution;
        }

        private bool IncidentResolutionExists(int id)
        {
            return _context.IncidentResolutions.Any(e => e.ResId == id);
        }
    }
}
