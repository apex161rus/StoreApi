using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Api.Data
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions options)
        : base(options)
        {
            
        }
    }
}