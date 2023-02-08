using System.Text;
using DatingApp.API.Helpers;
using DatingApp.API.Seed;
using DatingApp.BLL.Mappers;
using DatingApp.BLL.Services;
using DatingApp.BLL.Services.Interfaces;
using DatingApp.DAL.Repository;
using DatingApp.DAL.Repository.Interfaces;
using DatingApp.Models.Configuration;
using DatingApp.Models.Database.DataModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();

builder.Services.Configure<CloudStorageConfig>(builder.Configuration.GetSection("CloudStorageConfig"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

#region Repository

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();

#endregion

#region Services

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();

#endregion

builder.Services.AddScoped<LogUserActivityFilter>();

builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
            ValidateIssuer = true,
            ValidateAudience = false
        };
    });

builder.Services.AddDbContext<DataContext>(x =>
    x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());
app.UseHttpsRedirection();


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/V1/swagger.json", "DatingApp API V1");
});

MigrateDatabase();

app.Run();

void MigrateDatabase()
{
    using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    using var context = serviceScope.ServiceProvider.GetService<DataContext>();
    context!.Database.Migrate();

    SeedDatabase.SeedUsers(context);
}