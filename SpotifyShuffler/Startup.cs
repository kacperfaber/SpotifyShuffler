using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpotifyShuffler.Database.Contexts;
using SpotifyShuffler.Database.Models;

namespace SpotifyShuffler
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SpotifyContext>(builder => builder.UseSqlite("Data Source=app.db;"));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SpotifyContext>();
                
            services.AddMvc(mvc => mvc.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMvc(x => x.MapRoute("default", "{Controller}/{Action}"));

            app.UseHttpsRedirection();
        }
    }
}