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
        public DbSet<CategoriaProduto> CategoriaProducto { get; set; }
        public DbSet<Producto> Productos { get; set; }

        public DbSet<Nutricion> Nutricion { get; set; }

        public DbSet<Compra> Compras { get; set; }
        public DbSet<Valoracion> Valoraciones { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
