using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Projekat_WEB2_backend.Infrastructure;
using Projekat_WEB2_backend.Infrastructure.Configurations;
using Projekat_WEB2_backend.Interfaces;
using Projekat_WEB2_backend.Mapping;
using Projekat_WEB2_backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Projekat_WEB2_backend
{
    public class Startup
    {
        private readonly string _cors = "cors";
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
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });


            //Dodajemo semu autentifikacije i podesavamo da se radi o JWT beareru
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters //Podesavamo parametre za validaciju pristiglih tokena
               {
                   ValidateIssuer = true, //Validira izdavaoca tokena
                   ValidateAudience = false, //Kazemo da ne validira primaoce tokena
                   ValidateLifetime = true,//Validira trajanje tokena
                   ValidateIssuerSigningKey = true, //validira potpis token, ovo je jako vazno!
                   ValidIssuer = Configuration["ValidIssuer"], //odredjujemo koji server je validni izdavalac
                   ClockSkew = TimeSpan.Zero, //ovo u stvari proverava da li je isteklo vazenje tokena
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))//navodimo privatni kljuc kojim su potpisani nasi tokeni
               };
           });


            services.AddCors(options =>
            {
                var reactApp = Configuration["ReactApp"];
                options.AddPolicy(name: _cors, builder =>
                {
                    builder.WithOrigins(reactApp)
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

            var emailVerifyConfiguration = Configuration
                 .GetSection("EmailVerifyConfiguration")
                 .Get<EmailVerifyConfiguration>();
            services.AddSingleton(emailVerifyConfiguration);


            services.AddScoped<IArtikalService, ArtikalService>();
            services.AddScoped<IKorisnikService, KorisnikService>();
            services.AddScoped<IPorudzbinaService, PorudzbinaService>();
            services.AddScoped<IEmailVerifyService, EmailVerifyService>();
            


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

            app.UseCors(_cors);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
