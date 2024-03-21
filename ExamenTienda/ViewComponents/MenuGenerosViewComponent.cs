using ExamenTienda.Models;
using ExamenTienda.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamenTienda.ViewComponents
{
    public class MenuGenerosViewComponent: ViewComponent
    {
        private RepositoryTienda repo;
        public MenuGenerosViewComponent(RepositoryTienda repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> generos = this.repo.GetGeneros();
            return View(generos);
        }
    }
}
