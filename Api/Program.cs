using System.ComponentModel;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(Options => 
{
    Options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgreSQLConnection"));
});

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.MapGet("/Helo", () => "тест");

app.Run();
