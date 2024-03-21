using ExamenTienda.Extensions;
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


        public IActionResult DetalleLibro(int id, int? idComprar)
        {

            Libro libro = this.repo.FindLibro(id);

            if (idComprar != null)
            {
                List<int> carritoList;

                if (HttpContext.Session.GetString("CARRITO") != null)
                {
                    carritoList =
                        HttpContext.Session.GetObject<List<int>>("CARRITO");
                }
                else
                {
                    carritoList = new List<int>();
                }
                carritoList.Add(idComprar.Value);

                HttpContext.Session.SetObject("CARRITO", carritoList);
                ViewData["MSG"] = "Añadido al carrito!";
            }

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
        public async Task<IActionResult> Carrito(int? ideliminar)
        {
            List<int> ids =
                HttpContext.Session.GetObject<List<int>>("CARRITO");

            if (ids != null)
            {
                if (ideliminar != null)
                {
                    ids.Remove(ideliminar.Value);
                    if(ids.Count() == 0)
                    {
                        HttpContext.Session.Remove("CARRITO");
                    }
                    else
                    {
                        HttpContext.Session.SetObject("CARRITO", ids);
                    }
                }

                List<Libro> libros = await
                    this.repo.GetLibrosSessionAsync(ids);
                return View(libros);
            }
            else
            {
                ViewData["MSG"] = "No hay artículos seleccionados";
                return View();
            }
            
        }

        [AuthorizeUsuarios]
        public IActionResult MisPedidos(int idUsuario)
        {
            List<VistaPedido> pedidos =
                this.repo.GetVistaPedidosUsuario(idUsuario);
            return View(pedidos);
        }

        [AuthorizeUsuarios]
        public IActionResult FinalizarCompra(int idUsuario)
        {
            List<int> ids =
                HttpContext.Session.GetObject<List<int>>("CARRITO");

            int idfactura = this.repo.GetNumFactura();

            foreach(int id in ids)
            {
                this.repo.InsertarPedido(idfactura, id, idUsuario);
            }
            HttpContext.Session.Remove("CARRITO");
            return RedirectToAction("MisPedidos");
        }
    }
}
