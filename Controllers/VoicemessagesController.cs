using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ItstmVoiceMessages.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace ItstmVoiceMessages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoicemessagesController : ControllerBase
    {
        private readonly voicemsgitsmContext _context;
        private IHostingEnvironment Environment;


        public VoicemessagesController(voicemsgitsmContext context, IHostingEnvironment _environment)
        {
            _context = context;
            Environment = _environment;

        }

        // GET: api/Voicemessages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voicemessage>>> GetVoicemessages()
        {
            return await _context.Voicemessages.ToListAsync();
        }

        // GET: api/Voicemessages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voicemessage>> GetVoicemessage(int id)
        {
            var voicemessage = await _context.Voicemessages.FindAsync(id);

            if (voicemessage == null)
            {
                return NotFound();
            }

            return voicemessage;
        }

        // PUT: api/Voicemessages/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoicemessage(int id, Voicemessage voicemessage)
        {
            if (id != voicemessage.VoiId)
            {
                return BadRequest();
            }

            _context.Entry(voicemessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoicemessageExists(id))
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

        // POST: api/Voicemessages
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Voicemessage>> PostVoicemessage(Voicemessage voicemessage)
        {
            _context.Voicemessages.Add(voicemessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoicemessage", new { id = voicemessage.VoiId }, voicemessage);
        }

        ///////////////////////////////////////
        //api/Voicemessages/fileupload
        [HttpPost("FileUpload")]
        public async Task<IActionResult> IndexAsync(List<IFormFile> postedFiles)
        {
            string pathWfilename = "";
            string filenameOfTheFile = "";
            long size = postedFiles.Sum(f => f.Length);
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.ContentRootPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            pathWfilename += path;
            List<string> uploadedFiles = new List<string>();
            foreach (IFormFile postedFile in postedFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                pathWfilename += "\\";
                pathWfilename += fileName;
                filenameOfTheFile = fileName;
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                }
            }
            Voicemessage voicemessage = new Voicemessage();
            voicemessage.VoiceName = pathWfilename;
            voicemessage.Voice = filenameOfTheFile;

            _context.Voicemessages.Add(voicemessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoicemessage", new { id = voicemessage.VoiId }, voicemessage);
            //return Redirect("https://localhost:5001/api/IncidentManagements/gethtml");
            //return Ok(new { pathWfilename });
        }
        //////////////////////////////




        // DELETE: api/Voicemessages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Voicemessage>> DeleteVoicemessage(int id)
        {
            var voicemessage = await _context.Voicemessages.FindAsync(id);
            if (voicemessage == null)
            {
                return NotFound();
            }

            _context.Voicemessages.Remove(voicemessage);
            await _context.SaveChangesAsync();

            return voicemessage;
        }

        private bool VoicemessageExists(int id)
        {
            return _context.Voicemessages.Any(e => e.VoiId == id);
        }
    }
}
