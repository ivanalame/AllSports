using AllSports.Helpers;
using AllSports.Models;
using AllSports.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AllSports.Controllers
{
    public class UploadFilesController : Controller
    {

        private HelperPathProvider helperPathProvider;
        private RepositoryDeportes repo;
       public UploadFilesController(HelperPathProvider helperPathProvider,RepositoryDeportes repo)
        {
            this.helperPathProvider = helperPathProvider;
            this.repo = repo;
        }

        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubirFichero(IFormFile fichero)
        {
            string path = this.helperPathProvider.MapPath(fichero.FileName, Folders.Images);
            //SUBIMOS EL FICHERO UTILIZANDO Stream 
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                //MEDIANTE IFormFile COPIAMOS EL CONTENIDO DEL FICHERO 
                //AL STREAM 
                await fichero.CopyToAsync(stream);
            }

            ViewData["MENSAJE"] = "Fichero subido a " + path;

            string urlServer = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            ViewData["TEST"] =  HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            string urlPath = this.helperPathProvider.MapUrlPath(fichero.FileName, Folders.Uploads);
            ViewData["URL"] = urlPath;
            return View();
        }

       
    }
}
