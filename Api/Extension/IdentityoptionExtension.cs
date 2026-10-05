using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Api.Extension
{
    public static class IdentityoptionExtension
    {
        public static IServiceCollection AddconfigureIdentityoptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                // options.Password.RequireDigit = true; //Требуется число от 0 до 9 в пароле.
                // options.Password.RequireLowercase = true; //Требуется буква в нижнем регистре в пароле.
                // options.Password.RequireNonAlphanumeric = true; //Требуется символ, отличный от буквы, в пароле.
                // options.Password.RequireUppercase = true; //Требуется символ верхнего регистра в пароле.
                // options.Password.RequiredLength = 6; //Минимальная длина пароля.
                // options.Password.RequiredUniqueChars = 1;Применяется только к ASP.NET Core 2.0 или более поздней версии. Требуется количество разных символов в пароле.

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0; 
            });
            return services;
        }
    }
}