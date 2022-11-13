using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.CentroDto;
using CategoriaApi.Exceptions;
using CategoriaApi.Model;
using CategoriaApi.Repository;
using FluentResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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

        public async Task <CentroDeDistribuicao> ViaCep(CreateCentroDto centro)
        {
                HttpClient client = new HttpClient();
                var requisicao = await client.GetAsync($"https://viacep.com.br/ws/{centro.CEP}/json/");
                var resposta = await requisicao.Content.ReadAsStringAsync();
                if((int) requisicao.StatusCode == 200)
                {
                    var endereco = JsonConvert.DeserializeObject<CentroDeDistribuicao>(resposta);
                    return endereco;
                }
            throw new NullException();
        }
            
           
     
        public async Task<ReadCentroDto> AddCentroDeDistribuicao(CreateCentroDto centroDto)
        {
            CentroDeDistribuicao categoriaNome = _repository.RetornarNomeDocentro(centroDto);
            CentroDeDistribuicao centroEndereco = _repository.RetornarEndereco(centroDto);
            
            if (centroDto.Nome.Length >= 3)
            {
                if (categoriaNome == null && centroEndereco== null)
                {
                    var endereco= await ViaCep(centroDto);
                    CentroDeDistribuicao centro = _mapper.Map<CentroDeDistribuicao>(centroDto);
                   
                    centro.DataCriacao = DateTime.Now;
                    centro.Status = true;
                    _repository.AddCentro(centro,endereco);
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
                return null;

        }

        public List<CentroDeDistribuicao> GetCentroDeDistribuicao(string nome, string logradouro, string cep,
            string bairro, string localidade, string complemento, string uf, int? numero, bool? status,
            string ordem, int itensPagina, int paginaAtual)
        {
            return _repository.GetCentroDeDistribuicao( nome,logradouro, cep,
             bairro,localidade,complemento, uf,numero,  status, ordem, itensPagina,paginaAtual);
        }
    }
}
