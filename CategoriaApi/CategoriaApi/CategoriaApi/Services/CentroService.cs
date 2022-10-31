using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.CentroDto;
using CategoriaApi.Exceptions;
using CategoriaApi.Model;
using CategoriaApi.Repository;
using FluentResults;
using System;
using System.Linq;

namespace CategoriaApi.Services
{
    public class CentroService
    {
        IMapper _mapper;
        DatabaseContext _context;
        CentroRepository _repository;

        public CentroService(IMapper mapper, DatabaseContext context, CentroRepository repository)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        public ReadCentroDto AdicionarCentro(CreateCentroDto centroDto)
        {
            CentroDeDistribuicao centroNome = _repository.RecuperarCentroNome(centroDto);

            if (centroNome == null)
            {
                CentroDeDistribuicao centro = _mapper.Map<CentroDeDistribuicao>(centroDto);
                centro.Status = true;
                centro.DataCriacao = DateTime.Now;
                _repository.AddCentro(centro);
                return  _mapper.Map<ReadCentroDto>(centro);
            }
                throw new AlreadyExistsExceprion();
        }

        public Result AtualizarCentro(int id, UpdateCentroDto centroDto)
        {
            CentroDeDistribuicao centro = _repository.RecuperarCentroId(id);

            if (centro != null)
            {
                return Result.Fail("Id não encontrado");
            }
            _mapper.Map(centroDto, centro);
            centro.DataAtualizacao = DateTime.Now;
            _repository.Salvar();
            return Result.Ok();
        }

        public Result ExcluirCentro(int id)
        {
            CentroDeDistribuicao centro = _repository.RecuperarCentroId(id);
            if (centro == null)
            {
                return Result.Fail("Id não encontrado");
            }
            _repository.ExcluirCentro(centro);
            return Result.Ok();
        }

        public ReadCentroDto GetCentroPorId(int id)
        {
            CentroDeDistribuicao centro = _repository.RecuperarCentroId(id);

            if (centro != null)
            {
                ReadCentroDto readCentroDto = _mapper.Map<ReadCentroDto>(centro);
                return readCentroDto;
            }
            return null;
        }
    }
}
