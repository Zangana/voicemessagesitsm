using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;


namespace ItstmVoiceMessages.Controllers
{
    public class FileUploadController : Controller
    {
        private IHostingEnvironment Environment;
        private readonly ILogger<FileUploadController> _logger;
        private readonly IConfiguration _config;

        public FileUploadController(IHostingEnvironment _environment, IConfiguration config, ILogger<FileUploadController> logger)
        {
            Environment = _environment;
            _logger = logger;
            _config = config;

        }

        [HttpPost("FileUpload")]
        public IActionResult Index(List<IFormFile> postedFiles)
        {
            string pathWfilename = "";
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
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                    uploadedFiles.Add(fileName);
                }
            }
            
            //return Redirect("https://localhost:5001/api/IncidentManagements/gethtml");
            return Ok(new {pathWfilename});
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="directory"></param>
        /// <returns></returns>
        //[Route("/Uploads/Voices")]
        [HttpGet("/Uploads/Voices")]
        public string GetVoice (string fileName, string directory = "")
        {
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;
            string path = Path.Combine(this.Environment.ContentRootPath, "Uploads");
            return path;
            //_logger.LogDebug("HelloVoice");
            //var fullPath = Path.Combine(_config["BasePath"], directory);
            //using (var provider = new PhysicalFileProvider(fullPath))
            //{
            //    var stream = provider.GetFileInfo(fileName).CreateReadStream();
            //    return File(stream, "voice/mp3");
            //}
        }
        
    }
}