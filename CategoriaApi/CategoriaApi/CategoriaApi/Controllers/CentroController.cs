using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.CentroDto;
using CategoriaApi.Exceptions;
using CategoriaApi.Model;
using CategoriaApi.Repository;
using CategoriaApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoriaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CentroController: ControllerBase
    {
        private CentroService _service;
        private IMapper _mapper;
        private CentroRepository _centroRepository;

        public CentroController(IMapper mapper, CentroService service, CentroRepository repository )
        {
            _centroRepository = repository;
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarCentro([FromBody] CreateCentroDto centroDto)
        {
            try
            {
                var readCentro = await _service.AdicionarCentro(centroDto);
                return CreatedAtAction(nameof(GetCentroPorId), new {Id = readCentro.Id}, readCentro );
            }
            catch (AlreadyExistsExceprion)
            {
                return BadRequest("O Centro ja existe");
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCentro(int id, [FromBody] UpdateCentroDto centroDto)
        {
           Result centro = _service.AtualizarCentro(id, centroDto);
            if(centro.IsFailed ) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCentro(int id)
        {
            Result result = _service.ExcluirCentro(id);
            if(result.IsFailed) return NotFound();
            return NoContent();
        }
        

        [HttpGet("{id}")]
        public IActionResult GetCentroPorId(int id)
        {
           ReadCentroDto readCentro= _service.GetCentroPorId(id);
            if (readCentro != null) return Ok(readCentro);
            
            return BadRequest("Id não encontrado");
        }

        [HttpGet]
        public List<CentroDeDistribuicao> GetCentros([FromQuery]string nome, [FromQuery] bool? status, [FromQuery] string logradouro,
            [FromQuery] int? numero, [FromQuery] string bairro, [FromQuery] string localidade, [FromQuery] string uf,
            [FromQuery] string cep, [FromQuery] string ordem, [FromQuery] int itensPorPagina, [FromQuery] int pagina)
        {
            return _centroRepository.GetCentros(nome, status, logradouro, numero, bairro, localidade, uf, cep, ordem, itensPorPagina, pagina);
        }
    }
}
