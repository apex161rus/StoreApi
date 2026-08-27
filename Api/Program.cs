using Api.Data;
using Api.Extension;
using Api.Seed;
using Bogus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPstgreSqlDbContext(builder.Configuration);
builder.Services.AddPostgreSqlIdentityServiceExtension();

// builder.Services.AddDbContext<AppDbContext>(Options => 
// {
//     Options.UseNpgsql(
//         builder.Configuration.GetConnectionString("PostgreSQLConnection"));
// });

var app = builder.Build()
    .SeedProducts();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
