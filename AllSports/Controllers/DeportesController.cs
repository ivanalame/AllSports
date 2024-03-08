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

        public async Task<IActionResult> Index()
        {
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

        public IActionResult Productos(int IdCategoriaProducto)
        {
            List<Producto> Productos = this.repo.GetProductosById(IdCategoriaProducto);
            return View(Productos);
        }

        public IActionResult DetailProducto(int IdProducto)
        {
            List<Valoracion> Valoraciones = this.repo.GetValoracionById(IdProducto);
            Producto producto = this.repo.GetProductoById(IdProducto);
            ViewData["Producto"] = producto;
            return View(Valoraciones);
        }

        public IActionResult Compra(int IdProducto)
        {
            Producto producto = this.repo.GetProductoById(IdProducto);
            return View(producto);
        }

        #region  login y register
        public IActionResult LogIn()
        {
            return View();
        }

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

            return View();
        }
        #endregion
    }
}
