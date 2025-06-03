using ECommerce.Application.Catalog.Products;
using ECommerce.Application.Catalog.Products.Manage;
using ECommerce.Application.Common;
using ECommerce.Application.System.Users;
using ECommerce.Data.EF;
using ECommerce.Data.Entities;
using ECommerce.ViewModels.System.Users;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("ECommerceDb");
        var configuration = builder.Configuration;

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

        // builder add registering DbContext with SQL Server
        builder.Services.AddDbContext<ECommerceDbContext>(options => options.UseSqlServer(connectionString));


        // Add Identity services
        builder.Services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<ECommerceDbContext>()
            .AddDefaultTokenProviders();

        // Add Dependency Injection
        builder.Services.AddTransient<IPublicProductService, PublicProductService>();
        builder.Services.AddTransient<IManageProductService, ManageProductService>();
        builder.Services.AddTransient<IStorageService, FileStorageService>();
        builder.Services.AddTransient<IUserService, UserService>();

        // Add FluentValidation for LoginRequest
        builder.Services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
        builder.Services.AddTransient<IValidator<RegisterRequest>, RegisterRequestValidator>();

        // Add AutoMapper
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger ECommerce Solution", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "@JWT Authorization header using the Bearer scheme. \r\n\n " +
               "Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n" +
               "Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new string[] {}
        }
            });
        });

        string issuer = configuration.GetValue<string>("Tokens:Issuer");
        string signingKey = configuration.GetValue<string>("Tokens:Key");
        byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

        builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });

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
        app.UseAuthorization();
        app.UseAuthentication();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger ECommerce Solution V1");
        });

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}