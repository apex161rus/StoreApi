using Api.Extension;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPstgreSqlDbContext(builder.Configuration);
builder.Services.AddPostgreSqlIdentityServiceExtension();
builder.Services.AddconfigureIdentityoptions();
builder.Services.AddJwtTokenGenerator();
builder.Services.AddAuthenticationConfig(builder.Configuration);
builder.Services.AddCors();

// builder.Services.AddDbContext<AppDbContext>(Options => 
// {
//     Options.UseNpgsql(
//         builder.Configuration.GetConnectionString("PostgreSQLConnection"));
// });


var app = builder.Build();
    // .SeedProducts();


app.UseCors(options => options
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .WithExposedHeaders("*"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.Services.InitializeRoleAsync();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();
