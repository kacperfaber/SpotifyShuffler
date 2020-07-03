using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpotifyShuffler.Database.Contexts;
using SpotifyShuffler.Database.Models;

namespace SpotifyShuffler
{
    public class Startup
    {
        public IConfiguration Configuration;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SpotifyContext>(builder => builder.UseSqlite("Data Source=app.db;"));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<SpotifyContext>();

            services
                .AddAuthentication()
                .AddSpotify(opts =>
                {
                    opts.ClientId = Configuration["Authentication:Spotify:ClientId"];
                    opts.ClientSecret = Configuration["Authentication:Spotify:ClientSecret"];
                    
                    opts.Scope.Add("playlist-modify-private");
                    opts.Scope.Add("playlist-modify-public");
                    opts.Scope.Add("user-read-email");
                    opts.Scope.Add("user-read-private");
                });
                
            services.AddMvc(mvc => mvc.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();

            app.UseMvc(x => x.MapRoute("default", "{Controller}/{Action}"));

            app.UseHttpsRedirection();
        }
    }
}