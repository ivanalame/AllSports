using AllSports.Models;
using AllSports.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AllSports.ViewComponents
{
    public class MenuDeportesViewComponent: ViewComponent
    {
        private RepositoryDeportes repo; 

        public MenuDeportesViewComponent(RepositoryDeportes repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Deporte> deportes = this.repo.GetDeportes();
            return View(deportes);

        }
    }
}
