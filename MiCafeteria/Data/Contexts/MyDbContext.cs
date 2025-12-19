using MiCafeteria.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MiCafeteria.Data.Contexts
{
    //le ponemos de nombre mydbcontext por que tenemo que acordarnos que tiene que eredar si o si de Dbcontext
    public class MyDbContext : DbContext
    {

        //Entidad es como si fuera una tabla


        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }

        public MyDbContext(DbContextOptions options) : base(options) { }


        protected MyDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();  // Auto-incremental
            });
        }


        //este metodo viene de la clase AUDITORIA
        public override int SaveChanges(bool acceptAllChangedOnSuccess)
        {
            DateTime ahora = DateTime.UtcNow;

            //mira si las entradas son añadidas o modificadas

            var entradas = ChangeTracker.Entries<Auditoria>()
                .Where(x => x.State == EntityState.Added
                || x.State == EntityState.Modified);

            foreach (var entrada in entradas)
            {
                //FechaModificacion hereda de la Clase Auditoria
                entrada.Entity.FechaModificacion = ahora;

                if (entrada.State == EntityState.Added)
                    //FechaCreacion hereda de la clase Auditoria
                    entrada.Entity.FechaCreacion = ahora;

            }
            return base.SaveChanges(acceptAllChangedOnSuccess);
        }


    }
}
