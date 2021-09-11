using ItstmVoiceMessages.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ItstmVoiceMessages
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(); // Add corse
            services.AddControllers().AddNewtonsoftJson( options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<voicemsgitsmContext>(Options => Options.UseSqlServer("Name=ItsmDb"));
            services.AddMvcCore(options => { options.ReturnHttpNotAcceptable = true; });
            services.AddMvc(optioins => { optioins.ReturnHttpNotAcceptable = true; });
            services.AddAutoMapper(typeof(Startup));



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            //localhost:5000/Uploads 
            //The path to get the files from Uploads file.
            app.UseStaticFiles(new StaticFileOptions() { 
               
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Uploads")),
                RequestPath = new PathString("/Uploads")
                
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"View")),
                RequestPath = new PathString("//View//webView")
            });
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //   FileProvider = new PhysicalFileProvider("C:\\Users\\WAZA\\Desktop\\ukh\\thesis\\app\\ItstmVoiceMessages\\Uploads"),
            //   RequestPath = "/voice"
            //});
            app.UseRouting();
            //Accept any CORS 
            app.UseCors(x => x
             .AllowAnyMethod()
             .AllowAnyHeader()
             .SetIsOriginAllowed(origin => true)
             .AllowCredentials());
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
