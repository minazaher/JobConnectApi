using JobConnectApi.Database;
using JobConnectApi.Models;
using JobConnectApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAnyOrigin",
            policy => policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    });
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
    builder.Services.AddScoped<IUserService, UserService>();
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

    // Map controllers and Identity endpoints after Swagger middleware
    app.MapControllers();
  
    app.Run();


}
