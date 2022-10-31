using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoCategoria;
using CategoriaApi.Model;
using System.Collections.Generic;
using System.Linq;

namespace CategoriaApi.Repository
{
    public class CategoriaRepository
    {
        DatabaseContext _context;

        public CategoriaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void AddCategoria(Categoria categoria)
        {
            _context.Add(categoria);
            _context.SaveChanges();
        }

        public void DeletarCategoria(Categoria categoria)
        {
            _context.Remove(categoria);
            _context.SaveChanges();
        }

        public Categoria RecuperarCategoriaPorId(int id)
        {
            Categoria cateegoria= _context.Categorias.FirstOrDefault(categoria=> categoria.Id==id);
            return cateegoria;
        }

        public List<Categoria> RecuperarListaDeCategoria()
        {
            var categoria = _context.Categorias.ToList();
            return categoria;
        }

        public Categoria RecuperarCategoriaNome(CreateCategoriaDto categoriaDto)
        {
            var categoria= _context.Categorias.FirstOrDefault(categoria=> categoria.Nome.ToUpper() == categoriaDto.Nome.ToUpper());
            return categoria;
        }

        public IEnumerable<SubCategoria> RecuperarSubCategoriaPorId(int id)
        {
            IEnumerable<SubCategoria> subCategorias = _context.SubCategorias.Where(sub => sub.CategoriaId == id);
            return subCategorias;
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
