using ChallengeCore.Api.Middleware;
using ChallengeCore.Application.Interfaces;
using ChallengeCore.Infrastructure.Configuration;
using ChallengeCore.Infrastructure.Data;
using ChallengeCore.Infrastructure.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection("DatabaseOptions"));

builder.Services.Configure<Features>(
    builder.Configuration.GetSection("Features")
);

builder.Services.Configure<PathImage>(
    builder.Configuration.GetSection("PathImage")
);


builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
{
    var dbOptions = serviceProvider
        .GetRequiredService<IOptions<DatabaseOptions>>()
        .Value;

    var builderConnection = new SqlConnectionStringBuilder
    {
        DataSource = dbOptions.ServerName,
        InitialCatalog = dbOptions.DatabaseName,
        IntegratedSecurity = true,
        TrustServerCertificate = true,
        ConnectTimeout = dbOptions.TimeOut
    };

    options.UseSqlServer(builderConnection.ConnectionString);
});

//SERVICIOS
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChallengeService, ChallengeService>();
builder.Services.AddScoped<IRewardService, RewardService>();
builder.Services.AddScoped<IPokemonService, PokemonService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() // acepta cualquier origen
              .AllowAnyHeader() // acepta cualquier header
              .AllowAnyMethod(); // acepta cualquier método (GET, POST, PUT, DELETE, etc.)
    });
});

builder.Services.AddHttpClient<PokemonService>();

//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
