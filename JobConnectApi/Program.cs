using System.Text;
using JobConnectApi.Database;
using JobConnectApi.Mapper;
using JobConnectApi.Middleware;
using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Microsoft.Owin.Security.Jwt;
using Swashbuckle.AspNetCore.Filters;
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
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });  
    
    builder.Services.AddControllers();
    builder.Services.AddScoped<UserService>();
    builder.Services.AddScoped<IJwtService, JwtService>();
    builder.Services.AddScoped<IJobService, JobService>();
    builder.Services.AddScoped<IDataRepository<Employer>, DataRepository<Employer>>();
    builder.Services.AddScoped<IDataRepository<Job>, DataRepository<Job>>();
    builder.Services.AddScoped<IProposalService, ProposalService>();
    builder.Services.AddAutoMapper(typeof(JobMappingProfile));


    builder.Services.AddDbContext<DatabaseContext>();
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidIssuer = "jobConnect",
            IssuerSigningKey =   new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key-super-secret-key")),
            
        };
    });
    builder.Services.AddAuthorization(auth =>
    {
        auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
            .RequireAuthenticatedUser().Build());
    });


    
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
    app.MapControllers();
    app.UseAuthentication();
    app.UseAuthorization();

    using (var scope = app.Services.CreateScope())
    {
        // Create roles first (ensure migrations are applied)
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
