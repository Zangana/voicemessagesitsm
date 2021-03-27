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
    public class IncidentCategoriesController : ControllerBase
    {
        private readonly voicemsgitsmContext _context;

        public IncidentCategoriesController(voicemsgitsmContext context)
        {
            _context = context;
        }

        // GET: api/IncidentCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentCategory>>> GetIncidentCategories()
        {
            return await _context.IncidentCategories.ToListAsync();
        }

        // GET: api/IncidentCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncidentCategory>> GetIncidentCategory(int id)
        {
            var incidentCategory = await _context.IncidentCategories.FindAsync(id);

            if (incidentCategory == null)
            {
                return NotFound();
            }

            return incidentCategory;
        }

        // PUT: api/IncidentCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncidentCategory(int id, IncidentCategory incidentCategory)
        {
            if (id != incidentCategory.CatId)
            {
                return BadRequest();
            }

            _context.Entry(incidentCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentCategoryExists(id))
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

        // POST: api/IncidentCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IncidentCategory>> PostIncidentCategory(IncidentCategory incidentCategory)
        {
            _context.IncidentCategories.Add(incidentCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncidentCategory", new { id = incidentCategory.CatId }, incidentCategory);
        }

        // DELETE: api/IncidentCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IncidentCategory>> DeleteIncidentCategory(int id)
        {
            var incidentCategory = await _context.IncidentCategories.FindAsync(id);
            if (incidentCategory == null)
            {
                return NotFound();
            }

            _context.IncidentCategories.Remove(incidentCategory);
            await _context.SaveChangesAsync();

            return incidentCategory;
        }

        private bool IncidentCategoryExists(int id)
        {
            return _context.IncidentCategories.Any(e => e.CatId == id);
        }
    }
}
