using AllSports.Models;
using AllSports.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AllSports.Controllers
{
    public class DeportesController : Controller
    {
        private RepositoryDeportes repo;
        public DeportesController(RepositoryDeportes repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Deporte> deportes = repo.GetDeportes();
            List<Nutricion> nutricion = repo.GetNutricion();
            List<DetalleDeporte> detalleDeportes = repo.GetDetalleDeportes();

            var viewModel = new IndexViewModel
            {
                Deportes = deportes,
                Nutricion = nutricion,
                DetalleDeporte = detalleDeportes
                
            };
            return View(viewModel);
        }

        [HttpPost]
        public  IActionResult Index(int  IdDeporte)
        {
            List<DetalleDeporte> detalles = this.repo.GetDetalleDeportesById(IdDeporte);
            //var viewModel = new IndexViewModel
            //{
            //    DetalleDeporte = detalles

            //};
            ViewData["Mensaje"] = "hola";
            return View(detalles);
        }
    }
}
