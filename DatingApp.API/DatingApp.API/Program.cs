using DatingApp.API.Extensions;
using DatingApp.API.Helpers;
using DatingApp.API.Middleware;
using DatingApp.API.Seed;
using DatingApp.BLL.Mappers;
using DatingApp.BLL.Services;
using DatingApp.BLL.Services.Interfaces;
using DatingApp.BLL.Services.SignalR;
using DatingApp.DAL.Repository;
using DatingApp.DAL.Repository.Interfaces;
using DatingApp.Models.Configuration;
using DatingApp.Models.Database.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddCors();

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudStorageConfig"));

#region Repository

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion

#region Services

builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddSignalR();

#endregion

builder.Services.AddScoped<LogUserActivity>();

builder.Services.AddAutoMapper(typeof(AppUserProfile).Assembly);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddLogging();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("V1", new OpenApiInfo { Title = "DatingApp API", Version = "V1" });
});

builder.Services.AddDbContext<DataContext>(x =>
    x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());
app.UseHttpsRedirection();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<PresenceHub>("hubs/presence");
app.MapHub<MessageHub>("hubs/message");

app.UseSwagger();
app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/V1/swagger.json", "DatingApp API V1"); });

MigrateDatabase();

app.Run();

async void MigrateDatabase()
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DataContext>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
        await context.Database.MigrateAsync();
        await SeedDatabase.ClearConnections(context);
        await SeedDatabase.SeedUsers(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during migration");
    }
}