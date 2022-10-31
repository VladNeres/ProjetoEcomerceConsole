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
    public class CentroDeDistribuicaoService
    {
        private CentroRepository _repository;
       private IMapper _mapper;

        public CentroDeDistribuicaoService( IMapper mapper, CentroRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ReadCentroDto AddCentroDeDistribuicao(CreateCentroDto centroDto)
        {
            CentroDeDistribuicao categoriaNome = _repository.RetornarNomeDocentro(centroDto);

            if (centroDto.Nome.Length >= 3)
            {
                if (categoriaNome == null)
                {
                    CentroDeDistribuicao centro = _mapper.Map<CentroDeDistribuicao>(centroDto);
                    centro.DataCriacao = DateTime.Now;
                    centro.Status = true;
                    _repository.AddCentro(centro);
                    return _mapper.Map<ReadCentroDto>(centro);

                }
                throw new AlreadyExistException();
            }
            throw new MinCharacterException();
        }

        public Result AtualizarCentroService(int id, UpdateCentroDto centroDto)
        {
            CentroDeDistribuicao centro = _repository.RecuperarCentroPorId(id);
            if(centro == null)
            {
                return Result.Fail("Centro não encontrado");
            }
           
            CentroDeDistribuicao centroupdate = _mapper.Map(centroDto, centro);
            centroupdate.DataAtualizacao = DateTime.Now;
            _repository.Salvar();
            return Result.Ok();
        }

        public Result ExcluirCentro(int id)
        {
            var centro = _repository.RecuperarCentroPorId(id);

            if (centro == null)
            {
                return Result.Fail("Centro não encontrado");
            }
            _repository.ExcluirCentro(centro);
            return Result.Ok();
        }

        public ReadCentroDto RecuperarCentroPorId(int id)
        {
            CentroDeDistribuicao centro = _repository.RecuperarCentroPorId(id);
            if (centro != null)
            {
                ReadCentroDto centroDto = _mapper.Map<ReadCentroDto>(centro);
                return centroDto;
            }
                throw new NullException();

        }
    }
}
