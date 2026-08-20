using Api.Data;
using Api.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPstgreSqlDbContext(builder.Configuration);

// builder.Services.AddDbContext<AppDbContext>(Options => 
// {
//     Options.UseNpgsql(
//         builder.Configuration.GetConnectionString("PostgreSQLConnection"));
// });

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
