using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ItstmVoiceMessages.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using System.Diagnostics;
using System;
using System.Net;

namespace ItstmVoiceMessages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentManagementsController : ControllerBase
    {
        private readonly voicemsgitsmContext _context;
        private IHostingEnvironment env;
        private readonly IMapper _mapper;
        public IncidentManagementsController(voicemsgitsmContext context, IHostingEnvironment _env, IMapper mapper)
        {
            env = _env;
            _context = context;
            _mapper = mapper;

        }

        // GET: api/IncidentManagements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncidentManagement>>> GetIncidentManagements()
        {
            
            return await _context.IncidentManagements.Include(u => u.Descriptions)
                
                .Include(u => u.Voicemessages).AsSplitQuery().ToListAsync();
        }

        // GET: api/IncidentManagements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IncidentManagement>> GetIncidentManagement(int id)
        {
            var incidentManagement = await _context.IncidentManagements.FindAsync(id);

            if (incidentManagement == null)
            {
                return NotFound();
            }

            return incidentManagement;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="incidentManagement"></param>
        /// <returns></returns>
        
        [HttpPatch("UpdateIncident/{id}")]
        public async Task<IActionResult> PatchIncidentManagementAsync(int id,[FromBody] JsonPatchDocument<IncidentManagement> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var entity = _context.IncidentManagements.Include(u => u.Descriptions).AsSplitQuery().Include(u => u.Voicemessages).AsSplitQuery().FirstOrDefault(incidentmanagement => incidentmanagement.IncidentId == id);

            if (entity == null)
            {
                return NotFound();
            }
            patchDoc.ApplyTo(entity, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _context.SaveChangesAsync();
            return Ok(entity);

        }
        // PUT: api/IncidentManagements/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncidentManagement(int id, IncidentManagement incidentManagement)
        {
            if (id != incidentManagement.IncidentId)
            {
                return BadRequest();
            }

            _context.Entry(incidentManagement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentManagementExists(id))
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
        //// POST: api/IncidentManagements
        //[HttpPost]
        //public async Task<ActionResult<IncidentManagement>> addValueToIncident([FromBody] IncidentManagement incidentManagement)
        //{

        //    _context.IncidentManagements.Add(incidentManagement);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetIncidentManagement", new { id = incidentManagement.IncidentId }, incidentManagement);

        //}
        // POST: api/IncidentManagements
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IncidentManagement>> PostIncidentManagement(IncidentManagement incidentManagement)
        {

            Description description = new Description { IncidentManagement = incidentManagement.IncidentId };
            //incidentManagement.Descriptions.Add(incidentManagement);
            _context.IncidentManagements.Add(incidentManagement);

            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncidentManagement", new { id = incidentManagement.IncidentId }, incidentManagement);
        }
        
        /// <summary>
        /// api/incidentmanagements/fileupload
        /// </summary>
        /// <param name="postedFiles"></param>
        /// <returns></returns>
        [HttpPost("FileUpload")]
        public async Task<ActionResult<IncidentManagement>> IndexAsync(List<IFormFile> postedFiles)
        {
            try
            {

                string pathWfilename = "";
                string filenameOfTheFile = "";
                long size = postedFiles.Sum(f => f.Length);
                string wwwPath = this.env.WebRootPath;
                string contentPath = this.env.ContentRootPath;

                string path = Path.Combine(this.env.ContentRootPath, "Uploads");
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
                IncidentManagement incidentManagement = new IncidentManagement();
                Voicemessage voicemessage = new Voicemessage();
                voicemessage.Voice = pathWfilename;
                voicemessage.VoiceName = filenameOfTheFile;
                incidentManagement.Voicemessages.Add(voicemessage);

                _context.IncidentManagements.Add(incidentManagement);
               // _context.SaveChanges();
                await _context.SaveChangesAsync();

                return Redirect("https://localhost:5001/api/incidentmanagements/GetVoicePlay/index");
                //return CreatedAtAction("GetIncidentManagement", new { voicemessages = incidentManagement.Voicemessages,   id = incidentManagement.IncidentId }, incidentManagement);
               // return Ok(new { pathWfilename });
                //return NoContent();
            }catch(Exception e)
            {
                Debug.Print(e.Message);
                return NoContent();
            }
            
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetHtml")]
        [Produces("text/html")]
        public ActionResult Get()
        {

            var webroot = env.WebRootPath;
            var htmlPath = System.IO.Path.Combine("C:\\Users\\WAZA\\Desktop\\ukh\\thesis\\app\\ItstmVoiceMessages\\templates\\index.html");

            //var fileContent = System.IO.File.ReadAllText(htmlPath) ;

            return PhysicalFile(htmlPath, "text/html");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetVoicePlay/{nameOfFile}")]
        [Produces("text/html")]
        public ActionResult<string> GetHtmlFile(string nameOfFile)
        {

            string path = Path.Combine(this.env.ContentRootPath, "View/webView");

            var webroot = env.WebRootPath;
           // var htmlPath = System.IO.Path.Combine("C:\\Users\\WAZA\\Desktop\\ukh\\thesis\\app\\ItstmVoiceMessages\\View\\webView\\voiceplay.html");

            var fileContent = System.IO.File.ReadAllText(Path.Combine(path, nameOfFile + ".html")) ;
            
            return new ContentResult {
                Content = fileContent,
            ContentType = "text/html"
                    };

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("app.js")]
        public IActionResult GetRecordJavascript()
        {
            string path = Path.Combine(this.env.ContentRootPath, "View/webView/js");
            var fileContent = System.IO.File.ReadAllText(Path.Combine(path, "app.js"));

            return Content(fileContent, "text/javascript");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetVoicePlay/style.css")]
        public IActionResult GetStyleOfRecordAsync()
        {
            string path = Path.Combine(this.env.ContentRootPath, "View/webView");
            var fileContent = System.IO.File.ReadAllText(Path.Combine(path, "style.css"));

            return Content(fileContent, "text/css");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetVoicePlay/login-page.css")]
        public IActionResult GetStyleOfLoginAsync()
        {
            string path = Path.Combine(this.env.ContentRootPath, "View/webView");
            var fileContent = System.IO.File.ReadAllText(Path.Combine(path, "login-page.css"));
            return Content(fileContent, "text/css");
        }

        [HttpGet("GetVoicePlay/login-page.js")]
        public IActionResult GetLoginJavaScriptAsync()
        {
            string path = Path.Combine(this.env.ContentRootPath, "View/webView");
            var fileContent = System.IO.File.ReadAllText(Path.Combine(path, "login-page.js"));
            return Content(fileContent, "text/javascript");
        }
        // DELETE: api/IncidentManagements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IncidentManagement>> DeleteIncidentManagement(int id)
        {
            var incidentManagement = await _context.IncidentManagements.FindAsync(id);
            if (incidentManagement == null)
            {
                return NotFound();
            }

            _context.IncidentManagements.Remove(incidentManagement);
            await _context.SaveChangesAsync();

            return incidentManagement;
        }

        private bool IncidentManagementExists(int id)
        {
            return _context.IncidentManagements.Any(e => e.IncidentId == id);
        }
    }
}
