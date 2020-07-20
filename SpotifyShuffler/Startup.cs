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
using Newtonsoft.Json;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface.Extension;
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
            
            services.AddSpotify();

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
                
            services.AddMvc(mvc => mvc.EnableEndpointRouting = false).AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                o.SerializerSettings.Formatting = Formatting.Indented;
            });
            
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<SpotifyContext>()
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager>();
            
            services.AddScoped<IUserFinder, UserFinder>();
            services.AddScoped<IUserGenerator, UserGenerator>();
            services.AddScoped<IUserCreator, UserCreator>();
            services.AddScoped<IAccessTokenStore, AccessTokenStore>();
            services.AddScoped<IClaimGenerator, ClaimGenerator>();
            services.AddScoped<ISpotifyAccountGenerator, SpotifyAccountGenerator>();
            services.AddScoped<IRegistrationGenerator, RegistrationGenerator>();
            services.AddScoped<IPlaylistValidator, PlaylistValidator>();
            services.AddScoped<IPlaylistSizeValidator, PlaylistSizeValidator>();

            services.AddScoped<IRegistrationValidator, RegistrationValidator>();
            services.AddScoped<IRegistrationActivator, RegistrationActivator>();
            services.AddScoped<IUserLoginInfoGenerator, UserLoginInfoGenerator>();

            services.AddScoped<IModelIndexer, ModelIndexer>();
            services.AddScoped<IPlaylistPrototypeGenerator, PlaylistPrototypeGenerator>();
            services.AddScoped<IPrototypesSorter, PrototypesSorter>();
            services.AddScoped<ITrackPrototypeGenerator, TrackPrototypeGenerator>();
            services.AddScoped<ITrackPrototypesGenerator, TrackPrototypesGenerator>();
            services.AddScoped<IArtistLabelGenerator, ArtistLabelGenerator>();

            services.AddScoped<IOperationValidator, OperationValidator>();
            services.AddScoped<ISpotifyUrisGenerator, SpotifyUrisGenerator>();
            
            services.AddScoped(typeof(OperationManager));
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