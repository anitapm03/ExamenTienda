using ExamenTienda.Data;
using ExamenTienda.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ExamenTienda.Repositories
{
    #region PROCEDURES
    /*CREATE PROCEDURE SP_INSERTAR_PEDIDO
(@FACTURA INT,
@IDLIBRO INT,
@IDUSUARIO INT,
@FECHA DATE)
AS
	DECLARE @ID INT
	SELECT @ID = MAX(IDPEDIDO) + 1 FROM PEDIDOS
	INSERT INTO PEDIDOS VALUES (@ID, @FACTURA, @FECHA, @IDLIBRO, @IDUSUARIO, 1)
GO*/
    #endregion
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

        public Libro FindLibro(int id)
        {
            var consulta = from datos in context.Libros
                           where datos.IdLibro == id
                           select datos;
            return consulta.FirstOrDefault();
        }

        public async Task<List<Libro>> 
            GetLibrosSessionAsync(List<int> ids)
        {
            var consulta = from datos in this.context.Libros
                           where ids.Contains(datos.IdLibro)
                           select datos;
            if(consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return await consulta.ToListAsync();
            }
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

        public int GetNumFactura()
        {
            int maxFactura = context.Pedidos.Max(p => p.IdFactura);
            return maxFactura + 1;
        }

        public List<Usuario> GetUsuarios()
        {
            var consulta = from datos in context.Usuarios
                          select datos;
            return consulta.ToList();
        }

        public Usuario FindUser(int id)
        {
            var consulta = from datos in context.Usuarios
                           where datos.IdUsuario == id
                           select datos;
            return consulta.FirstOrDefault();
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

        public void InsertarPedido(int factura, int idlibro, int idusuario)
        {
            string sql = "SP_INSERTAR_PEDIDO @FACTURA, @IDLIBRO, @IDUSUARIO, @FECHA";
            SqlParameter pfac = new SqlParameter("@FACTURA", factura);
            SqlParameter plib = new SqlParameter("@IDLIBRO", idlibro);
            SqlParameter pusu = new SqlParameter("@IDUSUARIO", idusuario);
            SqlParameter pfe = new SqlParameter("@FECHA", DateTime.Now);

            this.context.Database.ExecuteSqlRaw(sql, pfac, plib, pusu, pfe);

        }

        /*LOGIN */
        public async Task<Usuario> LogInUsuarioAsync
            (string email, string passw)
        {
            Usuario user = 
                await this.context.Usuarios
                .Where(z => z.Email == email
                && z.Pass ==  passw).FirstOrDefaultAsync();
            return user;
        }

    }
}
