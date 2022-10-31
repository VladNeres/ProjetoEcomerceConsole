using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.CentroDto;
using CategoriaApi.Model;
using System.Collections.Generic;
using System.Linq;

namespace CategoriaApi.Repository
{
    public class CentroRepository
    {
        DatabaseContext _context;
        IMapper _mapper;

        public CentroRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public CentroDeDistribuicao RecuperarCentroId(int id)
        {
            var centro = _context.Centros.FirstOrDefault(centro => centro.Id == id);
            return centro;
        }

        public CentroDeDistribuicao RecuperarCentroNome(CreateCentroDto centroDto)
        {
            var centroNome= _context.Centros.FirstOrDefault(centro => centro.Nome.ToUpper() == centroDto.Nome.ToUpper());
            return centroNome;
        }

        public List<CentroDeDistribuicao> RecuperarListaDeCentro()
        {
            var listaDeCentro = _context.Centros.ToList();
            return listaDeCentro;
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
