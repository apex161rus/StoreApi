using Api.Data;
using Api.Seed;

namespace Api.Extension
{
    public static class SeedExtensions
    {
        public static WebApplication SeedProducts(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            if (!dbContext.Products.Any())
            {
                var products = FakeProductGenerator.GenerateProductlist(10);
                dbContext.Products.AddRange(products);
                dbContext.SaveChanges();
            }
            
            return app;
        }
    }
}