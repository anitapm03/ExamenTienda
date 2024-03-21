using ExamenTienda.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenTienda.Data
{
    public class TiendaContext: DbContext
    {
        public TiendaContext
            (DbContextOptions<TiendaContext> options)
        : base(options) { }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<VistaPedido> VistasPedidos { get; set; }
       
    }
}
