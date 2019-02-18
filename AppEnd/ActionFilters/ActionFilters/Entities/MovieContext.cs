using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActionFilters.Entities
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
