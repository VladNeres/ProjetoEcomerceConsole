using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.CentroDto;
using CategoriaApi.Exceptions;
using CategoriaApi.Model;
using CategoriaApi.Repository;
using FluentResults;
using System;
using System.Linq;
using RestSharp.Deserializers;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace CategoriaApi.Services
{
    public class CentroService
    {
        IMapper _mapper;
        CentroRepository _repository;

        public CentroService(IMapper mapper, CentroRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CentroDeDistribuicao> ViaCep(CreateCentroDto centroDto)
        {
            HttpClient client = new HttpClient();
            var requisicao = await client.GetAsync($"https://viacep.com.br/ws/{centroDto.CEP}/json/");
            var conteudo = await requisicao.Content.ReadAsStringAsync();

            var jsonObject = JsonConvert.DeserializeObject<CentroDeDistribuicao>(conteudo);
           if(conteudo == null)
           {
                throw new NullException();
           }
                return jsonObject;
        } 
        public async Task<ReadCentroDto> AdicionarCentro(CreateCentroDto centroDto)
        {
            CentroDeDistribuicao centroNome = _repository.RecuperarCentroNome(centroDto);
            var endereco =await ViaCep(centroDto);
           
            if (centroNome == null)
            {

                CentroDeDistribuicao centro = _mapper.Map<CentroDeDistribuicao>(centroDto);
                centro.Status = true;
                centro.DataCriacao = DateTime.Now;
                
                _repository.AddCentro(centro, endereco);
                return  _mapper.Map<ReadCentroDto>(centro);
            }
                throw new AlreadyExistsExceprion();
        }

        public Result AtualizarCentro(int id, UpdateCentroDto centroDto)
        {
            CentroDeDistribuicao centro = _repository.RecuperarCentroId(id);

            if (centro == null)
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
