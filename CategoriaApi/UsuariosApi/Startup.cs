using CategoriaApi.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApi.Data;
using UsuariosApi.Services;

namespace UsuariosApi
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
            //This is a configuration with database
            services.AddDbContext<UserDbContext>(options =>
            options.UseMySQL(Configuration.GetConnectionString("UsuarioConnection")));

            //configuration Length of password
            services.Configure<IdentityOptions>(opt =>
            opt.Password.RequiredLength = 8);

            //this is the identity configuration
            services.AddIdentity<CustomIdentityUser, IdentityRole<int>>(
                opt => opt.SignIn.RequireConfirmedEmail = true)
                .AddEntityFrameworkStores<UserDbContext>()
                 .AddDefaultTokenProviders();
            //this is the autormaper configure
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //this atribuition is informing to my app that i need some authorization
         


            services.AddScoped<CadastroService, CadastroService>();
            services.AddScoped<LoginService, LoginService>();
            services.AddScoped<TokenService, TokenService>();
            services.AddScoped<LogoutService, LogoutService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UsuariosApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UsuariosApi v1"));
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
