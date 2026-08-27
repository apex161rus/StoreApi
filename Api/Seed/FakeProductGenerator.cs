using Api.Model;
using Bogus;

namespace Api.Seed
{
    public static class FakeProductGenerator
    {
        static public List<Product> GenerateProductlist (int Count = 10)
        {
            var Categorys = new[]{"Категория 1","Категория 2","Категория 3"};
            var SpecialTags = new[]{"Новинки","Популярные","Рекомендуемый"};
            return new Faker<Product>("ru")
            .RuleFor(m => m.Id, f => f.IndexFaker + 1)
            .RuleFor(m => m.Name, f => f.Commerce.ProductName ())
            .RuleFor(m => m.Description, f =>f.Lorem.Sentence())
            .RuleFor(m => m.Category, f => f.PickRandom(Categorys))
            .RuleFor(m => m.SpecialTag, f => f.PickRandom(SpecialTags))
            .RuleFor(m => m.Price, f => Math.Round (f.Random.Double (1, 1000), 2))
            .RuleFor(m => m.Image, f => $"https://placehold.ru/200")
            .Generate(Count);
        }
        
    }
}