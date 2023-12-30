using ExchangesApi.Database;
using ExchangesApi.Services;
using ExchangesApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<CurrenciesDbContext>(x =>
    x.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<ICurrenciesService, CurrenciesService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();