using AllSports.Extensions;
using AllSports.Models;
using AllSports.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AllSports.Controllers
{
    public class DeportesController : Controller
    {
        private RepositoryDeportes repo;
        private RepositoryUsuarios _repo;
        public DeportesController(RepositoryDeportes repo, RepositoryUsuarios _repo)
        {
            this.repo = repo;
            this._repo = _repo;
        }

        public async Task<IActionResult> Index(int? idProducto)
        {

            if (idProducto != null)
            {
                List<int> idsProductos;

                if (HttpContext.Session.GetString("IDSPRODUCTOS") == null)
                {
                    //TODAVIA NO TENEMOS DATOS EN SESSION Y CREAMOS LA COLECCION 
                    idsProductos = new List<int>();
                }
                else
                {
                    idsProductos =  HttpContext.Session.GetObject<List<int>>("IDSPRODUCTOS");
                }
               idsProductos.Add(idProducto.Value);
                //GUARDAMOS LA COLECCION EN SESSION
                HttpContext.Session.SetObject("IDSPRODUCTOS", idsProductos);
                ViewData["MENSAJE"] = "Productos Almacenados" + idsProductos.Count ;
            }


            List<Deporte> deportes = repo.GetDeportes();
            List<Nutricion> nutricion = repo.GetNutricion();
            List<DetalleDeporte> detalleDeportes = repo.GetDetalleDeportes();
            List<Producto> productos =  await repo.GetAllProductos();

            var viewModel = new IndexViewModel
            {
                Deportes = deportes,
                Nutricion = nutricion,
                DetalleDeporte = detalleDeportes,
                Productos = productos

            };
            return View(viewModel);
        }

        public IActionResult DetalleDeporte(int IdDeporte)
        {
            List<DetalleDeporte> Deportes = this.repo.GetDetalleDeportesById(IdDeporte);
            return View(Deportes);
        }

        
        public IActionResult CategoriasProducto(int IdDetalleDeporte)
        {
            List<CategoriaProduto> Categorias = this.repo.GetCategoriasProducoPorId(IdDetalleDeporte);
            return View(Categorias);
        }

        public IActionResult Productos(int IdCategoriaProducto, int? idProducto)
        {

            if (idProducto != null)
            {
                List<int> idsProductos;

                if (HttpContext.Session.GetString("IDSPRODUCTOS") == null)
                {
                    //TODAVIA NO TENEMOS DATOS EN SESSION Y CREAMOS LA COLECCION 
                    idsProductos = new List<int>();
                }
                else
                {
                    idsProductos = HttpContext.Session.GetObject<List<int>>("IDSPRODUCTOS");
                }
                idsProductos.Add(idProducto.Value);
                //GUARDAMOS LA COLECCION EN SESSION
                HttpContext.Session.SetObject("IDSPRODUCTOS", idsProductos);
                ViewData["MENSAJE"] = "Productos Almacenados" + idsProductos.Count;
            }

            List<Producto> Productos = this.repo.GetProductosById(IdCategoriaProducto);
            return View(Productos);
        }

        public async Task <IActionResult> DetailProducto(int IdProducto)
        {

            if (IdProducto != null)
            {
                List<int> idsProductos;

                if (HttpContext.Session.GetString("IDSPRODUCTOS") == null)
                {
                    //TODAVIA NO TENEMOS DATOS EN SESSION Y CREAMOS LA COLECCION 
                    idsProductos = new List<int>();
                }
                else
                {
                    idsProductos = HttpContext.Session.GetObject<List<int>>("IDSPRODUCTOS");
                }
                idsProductos.Add(IdProducto);
                //GUARDAMOS LA COLECCION EN SESSION
                HttpContext.Session.SetObject("IDSPRODUCTOS", idsProductos);
                ViewData["MENSAJE"] = "Productos Almacenados" + idsProductos.Count;
            }

            List<ValoracionConNombreUsuario> Valoraciones = this.repo.GetValoracionById(IdProducto);
            Producto producto = await this.repo.GetProductoByIdAsync(IdProducto);
            ViewData["Producto"] = producto;
            return View(Valoraciones);
        }

        public async Task <IActionResult> Compra(int IdProducto)
        {
            Producto producto = await this.repo.GetProductoByIdAsync(IdProducto);
            return View(producto);
        }

        #region SESSION CARRITO
        //public async Task <IActionResult> SessionCarrito(int? idProducto)
        //{
           
        //    return View();
        //}

        public async Task<IActionResult> ProductosAlmacenadosCarrito(int? ideliminar)
        {
            List<int> idsProductos = HttpContext.Session.GetObject<List<int>>("IDSPRODUCTOS");

            if (idsProductos!=null)
            {
                if (ideliminar!=null)
                {
                    idsProductos.Remove(ideliminar.Value);
                    if(idsProductos.Count == 0)
                    {
                        HttpContext.Session.Remove("IDSPRODUCTOS");
                    }
                    else
                    {
                        HttpContext.Session.SetObject("IDSPRODUCTOS", idsProductos);
                    }
                }
                List<Producto> productos = await this.repo.GetProductosSessionAsync(idsProductos);
                return View(productos);
            }
            return View();
        }
        #endregion

        #region  login y register
        public IActionResult LogIn()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> LogIn(string email, string password)
        {
            var mail = email;
            Usuario user = await this._repo.LogInUserAsync(mail, password);
            if (user == null)
            {
                ViewData["MENSAJE"] = "Credenciales incorrectas";
                return View();
            }
            else
            {
                ViewData["MENSAJE"] = "TODO CORRECTO";
                return View(user);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register
            (string nombre, int nif, string email, string password, string apellidos)
        {
            await this._repo.RegisterUser(nombre, apellidos, nif, email, password);
            //string serverUrl = this.helperPathProvider.MapUrlServerPath();
            ////https://localhos:8555/Usuarios/ActivateUser/TOKEN?    esta es la url que tengo que generar
            //serverUrl = serverUrl + "/Usuarios/ActivateUser/" + user.TokenMail;
            //string mensaje = "<h3>Usuario Registrado</h3>";
            //mensaje += "<p>Debe activar su cuenta con nosotros pulsando el siguiente enlace</p>";
            //mensaje += "<p>" + serverUrl + "</p>";
            //mensaje += "<a href='" + serverUrl + "'>" + serverUrl + "</a>";
            //mensaje = "<p>Muchas Gracias </p>";
            //await this.helperMails.SendMailAsync(email, "Registro Usuario", mensaje);

            //ViewData["MENSAJE"] = "Usuario registrado correctamente";
            return View();
        }
        #endregion


        #region insert producto
        public IActionResult InsertProducto()
        {
            var categorias = this.repo.GetAllCategorias();
            ViewData["CATEGORIAS"] = categorias;
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> InsertProducto(Producto producto, IFormFile fichero)
        {
            if (fichero != null)
            {
                producto.Imagen = fichero.FileName;
                await this.repo.InsertProducto(producto.Nombre, producto.Precio, producto.Marca, producto.Descripcion, producto.Talla, producto.Imagen, producto.IdCategoriaProducto, producto.Descripcion_Larga);
            }
            else
            {
                ViewData["MENSAJE"] = "asigne imagen";
            }
            var categorias = this.repo.GetAllCategorias();
            ViewData["CATEGORIAS"] = categorias;
            return View();
        }
        #endregion
    }
}
