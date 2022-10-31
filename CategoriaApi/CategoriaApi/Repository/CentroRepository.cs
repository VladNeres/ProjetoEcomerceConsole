using CategoriaApi.Data;
using CategoriaApi.Data.Dto.CentroDto;
using CategoriaApi.Model;
using System.Linq;

namespace CategoriaApi.Repository
{
    public class CentroRepository
    {
        DatabaseContext _context;

        public CentroRepository(DatabaseContext centroRepository)
        {
            _context = centroRepository;
        }

        public void AddCentro(CentroDeDistribuicao centro)
        {
            _context.Add(centro);
            _context.SaveChanges();
        }

        public void ExcluirCentro(CentroDeDistribuicao centro)
        {
            _context.Remove(centro);
            _context.SaveChanges();
        }

        public CentroDeDistribuicao RecuperarCentroPorId(int id)
        {
            CentroDeDistribuicao centro = _context.Centros.FirstOrDefault(centro => centro.Id == id);
                return centro;
        }

        public CentroDeDistribuicao RetornarNomeDocentro(CreateCentroDto centroDto)
        {
            CentroDeDistribuicao centroNome = _context.Centros.FirstOrDefault(centro => centro.Nome.ToLower() == centroDto.Nome.ToLower());
            return centroNome;
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
