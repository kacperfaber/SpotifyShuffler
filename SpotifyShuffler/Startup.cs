using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Spotify;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interfaces;
using SpotifyShuffler.Types;

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
            services.AddDbContext<SpotifyContext>(builder => builder.UseSqlite("Data Source=app.db;", b => b.MigrationsAssembly("SpotifyShuffler")));
            
            services
                .AddAuthentication(opts =>
                {
                    opts.DefaultSignInScheme = SpotifyAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddSpotify(opts =>
                {
                    opts.ClientId = Configuration["Authentication:Spotify:ClientId"];
                    opts.ClientSecret = Configuration["Authentication:Spotify:ClientSecret"];
                    opts.SaveTokens = true;

                    opts.Scope.Add("playlist-modify-private");
                    opts.Scope.Add("playlist-modify-public");
                    opts.Scope.Add("user-read-email");
                    opts.Scope.Add("user-read-private");
                });
                
            services.AddMvc(mvc => mvc.EnableEndpointRouting = false);

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<SpotifyContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserFinder, UserFinder>();
            services.AddScoped<IUserGenerator, UserGenerator>();
            services.AddScoped<IUserCreator, UserCreator>();
            services.AddScoped<IAccessTokenStore, AccessTokenStore>();
            services.AddScoped<IClaimGenerator, ClaimGenerator>();
            services.AddScoped<ISpotifyAccountGenerator, SpotifyAccountGenerator>();
            services.AddScoped<IRegistrationGenerator, RegistrationGenerator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseMvc(x => x.MapRoute("default", "{Controller}/{Action}"));
        }
    }
}