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
            builder.Entity<Categoria>().
                HasOne(categoria => categoria.SubCategoria).
                WithOne(subCategoria => subCategoria.Categoria).
                HasForeignKey<SubCategoria>(subCategoria => subCategoria.CategoriaId);
        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<SubCategoria> SubCategorias { get; set; }

    }
}
