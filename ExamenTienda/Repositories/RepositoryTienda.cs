using ExamenTienda.Data;
using ExamenTienda.Models;

namespace ExamenTienda.Repositories
{
    public class RepositoryTienda
    {
        private TiendaContext context;

        public RepositoryTienda(TiendaContext context)
        {
            this.context = context;
        }

        public List<Genero> GetGeneros()
        {
            var consulta = from datos in context.Generos
                           select datos;
            return consulta.ToList();
        }

        public List<Libro> GetLibros()
        {
            var consulta = from datos in context.Libros
                           select datos;
            return consulta.ToList();
        }

        public async Task<List<Libro>> GetLibrosGenero(int idGenero)
        {
            var consulta =from datos in context.Libros
                          where datos.IdGenero == idGenero
                          select datos;
            return consulta.ToList();
        }

        public List<Pedido> GetPedidos()
        {
            var consulta = from datos in context.Pedidos
                           select datos;
            return consulta.ToList();
        }

        public List<Usuario> GetUsuarios()
        {
            var consulta = from datos in context.Usuarios
                          select datos;
            return consulta.ToList();
        }

        public List<VistaPedido> GetVistaPedidos()
        {
            var consulta = from datos in context.VistasPedidos
                           select datos;
            return consulta.ToList();
        }

        public List<VistaPedido> GetVistaPedidosUsuario(int idUsuario)
        {
            var consulta = from datos in context.VistasPedidos
                           where datos.IdUsuario == idUsuario
                           select datos;
            return consulta.ToList();
        }
    }
}
