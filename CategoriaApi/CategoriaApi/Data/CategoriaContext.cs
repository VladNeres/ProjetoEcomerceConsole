using CategoriaApi.Model;
using Microsoft.EntityFrameworkCore;

namespace CategoriaApi.Data
{
    public class CategoriaContext: DbContext
    {
        public CategoriaContext(DbContextOptions<CategoriaContext> opt) : base(opt)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
    }
}
