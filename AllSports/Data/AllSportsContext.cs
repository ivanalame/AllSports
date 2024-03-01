using AllSports.Models;
using Microsoft.EntityFrameworkCore;

namespace AllSports.Data
{
    public class AllSportsContext: DbContext
    {
        public AllSportsContext(DbContextOptions<AllSportsContext> options) : base(options)
        {}

        public DbSet<Deporte> Deportes { get; set; }
        public DbSet<DetalleDeporte> DetalleDeporte { get; set; }

        public DbSet<Nutricion> Nutricion { get; set; }
    }
}
