using System;
using AspNet.Security.OAuth.Spotify;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SpotifyShuffler.Database;
using SpotifyShuffler.Interface;
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
                .AddAuthentication(opts => { opts.DefaultSignInScheme = SpotifyAuthenticationDefaults.AuthenticationScheme; })
                .AddCookie()
                .AddSpotify(opts =>
                {
                    opts.ClientId = Configuration["Authentication:Spotify:ClientId"];
                    opts.ClientSecret = Configuration["Authentication:Spotify:ClientSecret"];
                    opts.SaveTokens = true;

                    opts.Scope.Add("playlist-read-collaborative");
                    opts.Scope.Add("playlist-read-private");
                    opts.Scope.Add("playlist-modify-private");
                    opts.Scope.Add("playlist-modify-public");
                    opts.Scope.Add("user-read-email");
                    opts.Scope.Add("user-read-private");
                    opts.Scope.Add("user-library-modify");
                    opts.Scope.Add("user-library-read");
                });

            services.AddMvc(mvc => mvc.EnableEndpointRouting = false)
                .AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                o.SerializerSettings.Formatting = Formatting.Indented;
            });

            services.AddIdentity<User, Role>(options => { options.User.AllowedUserNameCharacters += " "; })
                .AddEntityFrameworkStores<SpotifyContext>()
                .AddUserManager<UserManager>();

            services.ConfigureApplicationCookie(x =>
            {
                x.ExpireTimeSpan = TimeSpan.FromMinutes(59);
            });

            services.AddScoped<IUserFinder, UserFinder>();
            services.AddScoped<IUserGenerator, UserGenerator>();
            services.AddScoped<IUserCreator, UserCreator>();
            services.AddScoped<IAccessTokenStore, AccessTokenStore>();
            services.AddScoped<IClaimGenerator, ClaimGenerator>();
            services.AddScoped<ISpotifyAccountGenerator, SpotifyAccountGenerator>();
            services.AddScoped<IRegistrationGenerator, RegistrationGenerator>();
            services.AddScoped<IPlaylistValidator, PlaylistValidator>();
            services.AddScoped<IPlaylistSizeValidator, PlaylistSizeValidator>();
            services.AddScoped<ICompletedPlaylistGenerator, CompletedPlaylistGenerator>();
            services.AddScoped<IRegistrationValidator, RegistrationValidator>();
            services.AddScoped<IRegistrationActivator, RegistrationActivator>();
            services.AddScoped<IUserLoginInfoGenerator, UserLoginInfoGenerator>();
            services.AddScoped<IModelIndexer, ModelIndexer>();
            services.AddScoped<IArtistLabelGenerator, ArtistLabelGenerator>();
            services.AddScoped<IOperationValidator, OperationValidator>();
            services.AddScoped<ISpotifyUrisGenerator, SpotifyUrisGenerator>();
            services.AddScoped<Executor>();
            services.AddScoped<ISpotifyTracksShuffler, SpotifyTracksShuffler>();
            services.AddScoped<ITracksAdder, TracksAdder>();
            services.AddScoped<ISpotifyPlaylistCreator, SpotifyPlaylistCreator>();
            services.AddScoped<IPlaylistCollaborativeChecker, PlaylistCollaborativeChecker>();
            services.AddScoped<IEmailAddressDeleter, EmailAddressDeleter>();
            services.AddScoped<IEmailAddressProvider, EmailAddressProvider>();
            services.AddScoped<IConfirmationCodeGenerator, ConfirmationCodeGenerator>();
            services.AddScoped<IConfirmationCodeProvider, ConfirmationCodeProvider>();
            services.AddScoped<IConfirmationCodeSender, ConfirmationCodeSender>();
            services.AddScoped<IConfirmationCodeValidator, ConfirmationCodeValidator>();
            services.AddScoped<IEmailAddressConfirmator, EmailAddressConfirmator>();
            services.AddScoped<IEmailAddressGenerator, EmailAddressGenerator>();
            services.AddScoped<ISpotifyEmailIsSameChecker, SpotifyEmailIsSameChecker>();
            services.AddScoped<EmailAddressManager>();

            services.AddScoped(typeof(OperationManager));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc(x => x.MapRoute("default", "{Controller}/{Action}"));
        }
    }
}