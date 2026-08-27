using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Api.Data;
using Api.Model;

namespace Api.Extension
{
    public static class PostgreSqlServiceExtension
    {
        public static void AddPstgreSqlDbContext(
            this IServiceCollection services,
            IConfiguration configuration)
            {
                services.AddDbContext<AppDbContext>(Options => 
                {
                    Options.UseNpgsql(
                    configuration.GetConnectionString("PostgreSQLConnection"));
                });
            }
        public static void AddPostgreSqlIdentityServiceExtension(
            this IServiceCollection services
        )
        {
            services.AddIdentity<AppUser,IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
        }
    }

}