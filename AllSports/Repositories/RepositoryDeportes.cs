using AllSports.Data;
using AllSports.Helpers;
using AllSports.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

#region Views and procedures
//create view  V_PRODUCTOS
//AS
//	SELECT CAST(
//	row_number() over(order by IdProducto)as int) as posicion,
//    isnull(IdProducto, 0) as IdProducto, Nombre, Precio, Marca, Descripcion, IdTalla, Imagen, IdCategoriaProducto, DescLarga 
//	from PRODUCTOS
//GO
//CREATE PROCEDURE SP_PRODUCTOS (@posicion int, @registros int out)
//as
//select @registros = COUNT(IdProducto) from PRODUCTOS

//select IdProducto, Nombre, PRECIO, MARCA, DESCRIPCION, IDTALLA, IMAGEN, IDCATEGORIAPRODUCTO, DESCLARGA
//FROM V_PRODUCTOS
//where posicion >=@posicion and posicion< (@posicion+6)
//go
#endregion
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
        //GET TODAS LAS CATEGORIAS
        public List<CategoriaProduto> GetAllCategorias()
        {
            var consulta = from datos in this.context.CategoriaProducto select datos;
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

        //ELIMINAR UN PRODUCTO
        public async Task DeleteProducto(int idproducto)
        {
            Producto producto = await this.GetProductoByIdAsync(idproducto);
            this.context.Productos.Remove(producto);
            await this.context.SaveChangesAsync();
        }

        //MODIFICAR UN PRODUCTO
        public async Task ModificarProducto(int idproducto,string nombre, int precio, string marca, string descripcion, int talla, string imagen, int idcategoria, string desclarga)
        {
            Producto producto = await this.GetProductoByIdAsync(idproducto);

            producto.Nombre = nombre;
            producto.Precio = precio;
            producto.Marca = marca;
            producto.Descripcion = descripcion;
            producto.Talla = talla;
            producto.Imagen = imagen;
            producto.IdCategoriaProducto = idcategoria;
            producto.Descripcion_Larga = desclarga;

            await this.context.SaveChangesAsync();
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

        //Insert New Compra
        public async Task InsertNewCompra(int IdUsuario,int IdProducto, int Cantidad,DateTime FechaCompra,string Descuento,string metodo,string direccion,string provincia)
        {
            int maxid = await this.context.Compras.MaxAsync(z => z.IdCompra) + 1;
            Random random = new Random();
            int albaran = random.Next(1, 1001);
            Compra compra = new Compra ();

            compra.IdCompra = maxid;
            compra.Albaran = albaran;
            compra.IdUsuario = IdUsuario;
            compra.IdProducto = IdProducto;
            compra.Cantidad = Cantidad;
            compra.FechaCompra=FechaCompra;
            compra.Descuento = Descuento;
            compra.Metodo_Pago = metodo;
            compra.Direccion = direccion;
            compra.Provincia = provincia;

            this.context.Compras.Add(compra);

            await this.context.SaveChangesAsync();
        }

        //INSERT COMPRA TODO CARRITO
        public async Task InsertAllCarrito(List<int>idsProducto,int IdUsuario, int IdProducto, int Cantidad, DateTime FechaCompra, string Descuento)
        {
            int maxid = await this.context.Compras.MaxAsync(z => z.IdCompra) + 1;
            Random random = new Random();
            int albaran = random.Next(1, 1001);
            Compra compra = new Compra();

            compra.IdCompra = maxid;
            compra.Albaran = albaran;
            compra.IdUsuario = IdUsuario;
            compra.IdProducto = IdProducto;
            compra.Cantidad = Cantidad;
            compra.FechaCompra = FechaCompra;
            compra.Descuento = Descuento;

            this.context.Compras.Add(compra);

            await this.context.SaveChangesAsync();
        }
        //INSERT VALORACION
        public async Task InsertValoracion(int idUsuario, int idProducto, string comentario, int puntuacion)
        {
            int maxid = await this.context.Valoraciones.MaxAsync(z => z.IdValoracion) + 1;

            Valoracion valoracion = new Valoracion();


            valoracion.IdValoracion = maxid;
            valoracion.IdUsuario = idUsuario;
            valoracion.IdProducto = idProducto;    
            valoracion.Comentario   = comentario;
            valoracion.Puntuacion = puntuacion;

            this.context.Valoraciones.Add(valoracion);

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
        public async Task<Producto> GetProductoByIdAsync(int IdProducto)
        {
            return await context.Productos.FirstOrDefaultAsync(p => p.IdProducto == IdProducto);
        }


        //GET TODAS LAS VALORACIONES DE UN PRODUCTO 
        public List<ValoracionConNombreUsuario> GetValoracionById(int IdProducto)
        {
            var consulta = from valoracion in this.context.Valoraciones
                           join usuario in this.context.Usuarios on valoracion.IdUsuario equals usuario.IdUsuario
                           where valoracion.IdProducto == IdProducto
                           select new ValoracionConNombreUsuario
                           {
                               IdValoracion = valoracion.IdValoracion,
                               IdUsuario = valoracion.IdUsuario,
                               NombreUsuario = usuario.Email,
                               Puntuacion = valoracion.Puntuacion,
                               Comentario = valoracion.Comentario
                           };

            return consulta.ToList();
        }
        //GET Todas las compras de un usuario 
        public async Task <List<Compra>> GetComprasByIdUser(int IdUsuario)
        {
            return await this.context.Compras
          .Where(compra => compra.IdUsuario == IdUsuario)
          .ToListAsync();
        }

        //GET PRODUCTOS SESSION
        public async Task<List<Producto>> GetProductosSessionAsync(List<int> ids)
        {
            var consulta = from datos in this.context.Productos
                           where ids.Contains(datos.IdProducto)
                           select datos;

            if(consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return await consulta.ToListAsync();
            }
        }


        #region paginacion
        public async Task<ModelPaginacionProductos> GetProductosPaginacion(int posicion)
        {
            string sql = "SP_PRODUCTOS @posicion, @registros out";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamRegistros = new SqlParameter("@registros", -1);
            pamRegistros.Direction = System.Data.ParameterDirection.Output;

            var consulta = this.context.Productos.FromSqlRaw(sql, pamPosicion, pamRegistros);

            List<Producto> productos = await consulta.ToListAsync();
            int registros = (int)pamRegistros.Value;

            return new ModelPaginacionProductos
            {
                NumeroRegistros = registros,
                Productos = productos
            };
        }
        #endregion
    }
}
