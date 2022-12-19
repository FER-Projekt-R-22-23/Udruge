using UdrugeApp.DataAccess.SqlServer.Data;
using UdrugeApp.Repositories;
using UdrugeApp.Repositories.SqlServer;
using Microsoft.EntityFrameworkCore;
using ExampleApp.Repositories.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// create a configuration (app settings) from the appsettings file, depending on
// the ENV
IConfiguration configuration = builder.Environment.IsDevelopment()
            ? builder.Configuration.AddJsonFile("appsettings.Development.json").Build()
            : builder.Configuration.AddJsonFile("appsettings.json").Build();

// register the DbContext -EF ORM
// this allows the DbContext to be injected

builder.Services.AddDbContext<UdrugeContext>(options => options.UseSqlServer(configuration.GetConnectionString("UdrugeDB")));
//Add transients
//builder.Services.AddTransient<I<ime>, <ime>>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUdrugeRepository, UdrugeRepository>();
builder.Services.AddTransient<IVoditeljiUdrugeRepository, VoditeljiUdrugeRepository>();
builder.Services.AddTransient<IProstoriRepository, ProstoriRepository>();
builder.Services.AddTransient<IResursRepository, ResursRepository>();
HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
builder.Services.AddHttpClient("Clanstvo", client =>
{
    client.BaseAddress = new Uri("https://localhost:7273");
}).ConfigurePrimaryHttpMessageHandler(x => clientHandler);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();