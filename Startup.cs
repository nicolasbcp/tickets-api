using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Tickets_API.Data;
using Tickets_API.Repositories;
using Tickets_API.Repositories.Interfaces;

namespace Tickets_API
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
            services.AddControllers().AddNewtonsoftJson();

            services.AddDbContext<ApplicationDbContext>
                (options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<ICasaDeShowRepository, CasaDeShowRepository>();
            services.AddTransient<IEventoRepository, EventoRepository>();
            services.AddTransient<IGeneroMusicalRepository, GeneroMusicalRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IVendaRepository, VendaRepository>();

            services.AddResponseCompression();

            services.AddSwaggerGen
            (c => {
                c.SwaggerDoc
                ("v1", new OpenApiInfo {
                    Version = "v1", Title = "GFT Tickets", Description = "API de Gerenciamento do GFT Tickets.",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseResponseCompression();

            app.UseSwagger();

            app.UseSwaggerUI
                (c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tickets API"); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
