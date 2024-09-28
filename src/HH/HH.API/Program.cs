using HH.API.Extension;
using HH.Application;
using HH.Application.Chat.Hubs;
using HH.Infrastructure;
using HH.Infrastructure.Database;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerGenWithAuthorization();

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastracture(builder.Configuration);

builder.Services.AddCors(config => 
                config.AddDefaultPolicy(options => 
                        options.AllowAnyHeader()
                                .AllowAnyMethod()
                                .WithOrigins("http://localhost:5173")));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chat");
app.UseCors();
app.MapControllers();


//app.UseCookiePolicy(new CookiePolicyOptions
//{
//    HttpOnly = HttpOnlyPolicy.Always,
//    Secure = CookieSecurePolicy.Always
//});

await app.ApplyMigration();
app.Run();
