using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.CentroDto;
using CategoriaApi.Exceptions;
using CategoriaApi.Model;
using CategoriaApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CategoriaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CentroController: ControllerBase
    {

        private DatabaseContext _context;
        private IMapper _mapper;
        private CentroDeDistribuicaoService _service;
        public CentroController(DatabaseContext context, IMapper mapper, CentroDeDistribuicaoService service)
        {
            _service = service;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionarCentro([FromBody] CreateCentroDto centroDto )
        {
            try
            {
                ReadCentroDto readCentro = _service.AddCentroDeDistribuicao(centroDto);
                return CreatedAtAction(nameof(GetCentroPorId), new { id = readCentro.Id }, readCentro);
            }
            catch (AlreadyExistException)
            {
                return BadRequest("Esse centro já existe");
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCentro(int id, [FromBody] UpdateCentroDto updateCentro)
        {
            try
            {
                var centro = _service.AtualizarCentroService(id, updateCentro);
                if (centro.IsFailed) return NotFound();
                return NoContent();

            }
            catch (NullException)
            {
                return NotFound("Id não encontrado");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirCentro(int id)
        {
            Result result = _service.ExcluirCentro(id);
            if (result.IsFailed) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
       public IActionResult GetCentroPorId(int id)
       {
            ReadCentroDto readCentro = _service.RecuperarCentroPorId(id);
            if (readCentro != null) return Ok(readCentro);
            return NotFound("Não encontrado");
       }
    }
}
