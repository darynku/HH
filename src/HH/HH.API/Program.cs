using HH.API.Extension;
using HH.Application;
using HH.Application.Chat.Hubs;
using HH.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerGenWithAuthorization();

builder.Services.AddCors(config => 
                config.AddDefaultPolicy(options =>
                        options.WithOrigins("http://localhost:5173")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials()));

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastracture(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.ApplyMigration();
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



app.Run();
