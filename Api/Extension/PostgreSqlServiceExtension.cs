using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Api.Data;

namespace Api.Extension
{
    public static class PostgreSqlServiceExtension
    {
        public static void AddPstgreSqlDbContext(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<AppDbContext>(Options => 
            {
                Options.UseNpgsql(
                configuration.GetConnectionString("PostgreSQLConnection"));
            });
        }
    }
}