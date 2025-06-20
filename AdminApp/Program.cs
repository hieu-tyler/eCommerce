using AdminApp.Services.Language;
using AdminApp.Services.Product;
using AdminApp.Services.Role;
using AdminApp.Services.User;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using ViewModels.System.Users;

namespace AdminApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout to 30 minutes
        });

        // Add Validation for LoginRequest
        builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Login/"; // Set the login path for unauthenticated users
                options.AccessDeniedPath = "/User/Forbidden/"; // Set the access denied path for unauthorized users
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(10); // Set the cookie expiration time
                //options.SlidingExpiration = true; // Enable sliding expiration
            });

        // Register HttpClient for API calls
        builder.Services.AddHttpClient<IUserApiClient, UserApiClient>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
        builder.Services.AddTransient<IRoleApiClient, RoleApiClient>();
        builder.Services.AddTransient<ILanguageApiClient, LanguageApiClient>();
        builder.Services.AddTransient<IProductApiClient, ProductApiClient>();


        // if DEBUG
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
        }
        //endif

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseSession(); // Enable session middleware before UseAuthorization

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
