using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.CentroDto;
using CategoriaApi.Exceptions;
using CategoriaApi.Model;
using CategoriaApi.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CategoriaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CentroController: ControllerBase
    {
        public CentroService _service;
        public DatabaseContext _context;
        public IMapper _mapper;

        public CentroController(DatabaseContext context, IMapper mapper, CentroService service)
        {
            _service = service;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public  IActionResult AdicionarCentro([FromBody] CreateCentroDto centroDto)
        {
            try
            {
                ReadCentroDto readCentro = _service.AdicionarCentro(centroDto);
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
    }
}
