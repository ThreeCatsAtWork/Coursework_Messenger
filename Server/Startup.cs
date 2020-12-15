using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace Server
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
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder.WithOrigins(Program.ip).AllowAnyHeader().AllowAnyMethod());                    
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Microsoft.Extensions.Hosting.IHostApplicationLifetime applicationLifetime,
                      ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("CorsApi");
            applicationLifetime.ApplicationStarted.Register(OnApplicationStarted);
            applicationLifetime.ApplicationStopping.Register(OnApplicationStopping);
            applicationLifetime.ApplicationStopped.Register(OnApplicationStopped);


      app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
      protected void OnApplicationStarted()
      {
        try
			  {
          using (var reader = new StreamReader("ip.txt"))
          {
            Program.ip = reader.ReadToEnd();
          }
        }
        catch
			  {
          Program.ip = "http://localhost:5000";
          Console.WriteLine("Wrong ip in file");
			  }        
      }

      protected void OnApplicationStopping()
      {
        Program.Sessions.SaveToFile();
        using (var writer = new StreamWriter("ip.txt"))
        { 
          writer.Write(Program.ip);      
        }
        Console.WriteLine(Program.ip);
        MessagesClass.SaveHistoryToFile();
      }

      protected void OnApplicationStopped()
      {
       
      }

  }
}
