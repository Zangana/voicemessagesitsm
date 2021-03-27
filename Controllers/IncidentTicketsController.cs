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
    public class IncidentTicketsController : ControllerBase
    {
        private readonly voicemsgitsmContext _context;

        public IncidentTicketsController(voicemsgitsmContext context)
        {
            _context = context;
        }

        // GET: api/IncidentTickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentTicket>>> GetIncidentTickets()
        {
            return await _context.IncidentTickets.ToListAsync();
        }

        // GET: api/IncidentTickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncidentTicket>> GetIncidentTicket(int id)
        {
            var incidentTicket = await _context.IncidentTickets.FindAsync(id);

            if (incidentTicket == null)
            {
                return NotFound();
            }

            return incidentTicket;
        }

        // PUT: api/IncidentTickets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncidentTicket(int id, IncidentTicket incidentTicket)
        {
            if (id != incidentTicket.TicId)
            {
                return BadRequest();
            }

            _context.Entry(incidentTicket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentTicketExists(id))
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

        // POST: api/IncidentTickets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IncidentTicket>> PostIncidentTicket(IncidentTicket incidentTicket)
        {
            _context.IncidentTickets.Add(incidentTicket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncidentTicket", new { id = incidentTicket.TicId }, incidentTicket);
        }

        // DELETE: api/IncidentTickets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IncidentTicket>> DeleteIncidentTicket(int id)
        {
            var incidentTicket = await _context.IncidentTickets.FindAsync(id);
            if (incidentTicket == null)
            {
                return NotFound();
            }

            _context.IncidentTickets.Remove(incidentTicket);
            await _context.SaveChangesAsync();

            return incidentTicket;
        }

        private bool IncidentTicketExists(int id)
        {
            return _context.IncidentTickets.Any(e => e.TicId == id);
        }
    }
}
