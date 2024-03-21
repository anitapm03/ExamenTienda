using ExamenTienda.Models;
using ExamenTienda.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamenTienda.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryTienda repo;
        public LibrosController(RepositoryTienda repo) 
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Libro> libros = this.repo.GetLibros();
            return View(libros);
        }

        public async Task<IActionResult>
            LibrosGenero(int idgenero)
        {
            List<Libro> libros =
                await this.repo.GetLibrosGenero(idgenero);
            return View(libros);
        }

    }
}
