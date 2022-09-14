using CategoriaApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CategoriaApi.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SubCategoria>().
                HasOne(subCategoria => subCategoria.Categoria).
                WithMany(categoria => categoria.SubCategoria).
                HasForeignKey(subCategoria => subCategoria.CategoriaId);
        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<SubCategoria> SubCategorias { get; set; }

    }
}
