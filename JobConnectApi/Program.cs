using System.Text;
using JobConnectApi.Database;
using JobConnectApi.Middleware;
using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.Jwt;
using AuthenticationMiddleware = JobConnectApi.Middleware.AuthenticationMiddleware;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAnyOrigin",
            policy => policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    });
    builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<DatabaseContext>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
    builder.Services.AddScoped<UserService>();
    builder.Services.AddScoped<IJwtService, JwtService>();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("U2NEQATDmLMd+REgxyvSnKqZPzj2o31yQWskSAk+4JDTUtNhHN9WrzWhfwlMVS6n\n"))
        };
    });


    builder.Services.AddDbContext<DatabaseContext>(options =>
        options.UseSqlite("Data Source=JobConnect.db"));
    
}


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

{
    // Other app configuration (error handling, HTTPS redirection, etc.)
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseCors("AllowAnyOrigin"); // Apply policy globally
    app.UseAuthorization();
    app.MapControllers();
    using (var scope = app.Services.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var roles = new[] { "Admin", "Employer", "JobSeeker" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
    app.Run();


}
