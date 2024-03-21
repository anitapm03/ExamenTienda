using ExamenTienda.Filters;
using ExamenTienda.Models;
using ExamenTienda.Repositories;
using Microsoft.AspNetCore.Authorization;
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


        public IActionResult DetalleLibro(int id)
        {
            Libro libro = this.repo.FindLibro(id);
            return View(libro);
        }

        /*COSAS DEL USER*/
        [AuthorizeUsuarios]
        public IActionResult PerfilUsuario(/*int id*/)
        {
            //Usuario user = this.repo.FindUser(id);
            return View(/*user*/);
        }
        [AuthorizeUsuarios]
        public IActionResult Carrito()
        {
            return View();
        }

        [AuthorizeUsuarios]
        public IActionResult MisPedidos()
        {
            return View();
        }
    }
}
