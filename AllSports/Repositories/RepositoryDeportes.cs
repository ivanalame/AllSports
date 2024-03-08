using AllSports.Data;
using AllSports.Helpers;
using AllSports.Models;
using Microsoft.EntityFrameworkCore;

namespace AllSports.Repositories
{
    public class RepositoryDeportes
    {
        private AllSportsContext context;

        public RepositoryDeportes(AllSportsContext context)
        {
            this.context = context;
        }

        public List<Deporte> GetDeportes()
        {
            var consulta = from datos in this.context.Deportes select datos;
            return consulta.ToList();
        }

        //GET Sexo

        //Get NUTRICION
        public List<Nutricion> GetNutricion()
        {
            var consulta = from datos in this.context.Nutricion select datos;
            return consulta.ToList();
        }

        //Get DETALLES DEPORTES
        public List<DetalleDeporte> GetDetalleDeportes()
        {
            var consulta = from datos in this.context.DetalleDeporte select datos;
            return consulta.ToList();
        }
        //Get DetalleDeporte por id
        public List<DetalleDeporte> GetDetalleDeportesById(int IdDeporte)
        {
            var consulta = from datos in this.context.DetalleDeporte
                           where datos.IdDeporte == IdDeporte
                           select datos;
            return consulta.ToList();
        }

        //GET CATEGORIOPRODUCTO POR ID
         public List<CategoriaProduto> GetCategoriasProducoPorId(int IdDetalle)
        {
                 var consulta = from datos in this.context.CategoriaProducto
                                          where datos.IdDetalleDeporte == IdDetalle
                                          select datos;

            return consulta.ToList();
        }

        //Get Todos los productos

        public async Task<List<Producto>> GetAllProductos()
        {
            var consulta = from datos in this.context.Productos
                           select datos;


            List<Producto>productos = await consulta.ToListAsync();

            return productos;
        }

        //INSERT PRODUCTO
        public async Task InsertProducto(string nombre, int precio,string marca, string descripcion, int talla, string imagen, int idcategoria,string desclarga)
        {
            int maxid = await this.context.Productos.MaxAsync(z => z.IdProducto) + 1;

            Producto producto = new Producto();


                producto.IdProducto = maxid;
            producto.Nombre = nombre;
            producto.Precio = precio;
            producto.Marca = marca;
            producto.Descripcion = descripcion;
            producto.Talla = talla;
            producto.Imagen = imagen;
            producto.IdCategoriaProducto = idcategoria;
            producto.Descripcion_Larga = desclarga;
            
           
            this.context.Productos.Add(producto);

            await this.context.SaveChangesAsync();
        }
        //Get Productos de una categoria

        public List<Producto> GetProductosById(int IdCategoriaProducto)
        {
            var consulta = from datos in this.context.Productos
                           where datos.IdCategoriaProducto == IdCategoriaProducto
                           select datos;

            return consulta.ToList();
        }

        //Get producto por id Producto 
        public Producto GetProductoById(int IdProducto)
        {
            var consulta = from datos in this.context.Productos
                           where datos.IdProducto == IdProducto
                           select datos;

            return consulta.First();
        }

        //GET TODAS LAS VALORACIONES DE UN PRODUCTO 
        public List<Valoracion> GetValoracionById(int IdProducto)
        {
            var consulta = from datos in this.context.Valoraciones
                           where datos.IdPrdocucto == IdProducto
                           select datos;

            return consulta.ToList();
        }
        //GET Todas las compras de un usuario 
        public List<Compra> GetComprasByIdUser(int IdUsuario)
        {
            var consulta = from datos in this.context.Compras
                           where datos.IdUsuario == IdUsuario
                           select datos;


            return consulta.ToList();
        }
    }
}
