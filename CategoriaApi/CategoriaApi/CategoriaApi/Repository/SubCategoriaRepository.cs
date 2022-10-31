using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoSubCategoria;
using CategoriaApi.Model;
using System.Collections.Generic;
using System.Linq;

namespace CategoriaApi.Repository
{
    public class SubCategoriaRepository
    {
        public DatabaseContext _context;

        public SubCategoriaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void AddSubCategoria(SubCategoria subCat)
        {
            _context.Add(subCat);
            _context.SaveChanges();
        }

        public void DeletarSubCategoria(SubCategoria sub)
        {
            _context.Remove(sub);
            _context.SaveChanges();
        }

        public SubCategoria RecuperarSubId(int id)
        {
            SubCategoria sub = _context.SubCategorias.FirstOrDefault(sub => sub.Id == id);
            return sub;
        }

        public SubCategoria RecuperarSubNome(CreateSubCategoriaDto subCategoriaDto)
        {
            SubCategoria subNome = _context.SubCategorias.FirstOrDefault(sub => sub.Nome.ToUpper()== subCategoriaDto.Nome.ToUpper());
            return subNome;
        }

        public void Salvar()
        {
            _context.SaveChanges();

        }

        public Categoria RecuperarCategoriaId(CreateSubCategoriaDto subDto)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(cat => cat.Id == subDto.CategoriaId);
            return categoria;
        }
        public List<SubCategoria> RecuperarListaDeSub()
        {
            var subCategorias = _context.SubCategorias.ToList();
            return subCategorias;
        }
    }
}
