using Microsoft.AspNetCore.Authentication.Cookies;
using MovieUI.Components;
using MovieUI.Services;

namespace MovieUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddHttpClient<IMovieService, MovieService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5251/");
            });
			builder.Services.AddHttpClient<IUserService, UserService>(client =>
			{
				client.BaseAddress = new Uri("http://localhost:5251/");
			});
			builder.Services.AddHttpClient<IRatingService, RatingService>(client =>
			{
				client.BaseAddress = new Uri("http://localhost:5251/");
			});
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "auth_token";
                    options.LoginPath = "/login";
                    options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
                    options.AccessDeniedPath = "/access-denied";
                });
            builder.Services.AddAuthorization();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddHttpContextAccessor();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAntiforgery();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
