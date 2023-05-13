using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Projekat_WEB2_backend.Infrastructure;
using Projekat_WEB2_backend.Interfaces;
using Projekat_WEB2_backend.Mapping;
using Projekat_WEB2_backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Projekat_WEB2_backend", Version = "v1" });
            });

            services.AddScoped<IArtikalService, ArtikalService>();
            services.AddScoped<IKorisnikService, KorisnikService>();
            services.AddScoped<IPorudzbinaService, PorudzbinaService>();

            services.AddDbContext<ProdavnicaDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ProdavnicaDatabase")));

            //dodavanje za konverziju enumeracija, sa fronta saljem enumeraciju kao string, on je konvertuje ispravno
            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                x.JsonSerializerOptions.IgnoreNullValues = true;
            });



            //Registracija mapera u kontejneru, zivotni vek singleton
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projekat_WEB2_backend v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
