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

    //options.UseSqlServer(builderConnection.ConnectionString, b => b.MigrationsAssembly("ChallengeCore.Infrastructure"));

});



//SERVICIOS
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChallengeService, ChallengeService>();
builder.Services.AddScoped<IRewardService, RewardService>();


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

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
