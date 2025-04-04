using Microsoft.EntityFrameworkCore;

namespace L02P02_2022_EA_650_2022_RC_652.Models
{
    public class libroContext : DbContext
    {
        public libroContext(DbContextOptions options) : base(options)
        { }

        public DbSet<autores> autores { get; set; }
        public DbSet<categorias> categorias { get; set; }
        public DbSet<clientes> clientes { get; set; }
        public DbSet<comentarios_libros> comentarios_libros { get; set; }
        public DbSet<libros> libros { get; set; }
        public DbSet<pedido_detalle> pedido_detalle { get; set; }
        public DbSet<pedido_encabezado> pedido_encabezado { get; set; }
    }
}
