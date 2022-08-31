

using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto;
using CategoriaApi.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CategoriaApi.Controllers
{
    [ApiController]
        [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private CategoriaContext _context;
        private IMapper _mapper;

        public CategoriaController(CategoriaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionarCategoria([FromBody] CreateCategoriaDto categoria)
        {
            Categoria categoriaDto = _mapper.Map<Categoria>(categoria);
            if (categoria.Status == true)
            {
                if (categoria.Nome != categoriaDto.Nome)
                {
                    _context.Categorias.Add(categoriaDto);
                    _context.SaveChanges();
                    Console.WriteLine(categoria.Nome);
                    return CreatedAtAction(nameof(GetCategoriaPorId), new { id = categoriaDto.Id }, categoria);

                }
                else
                {
                    return BadRequest("Essa categoria ja existe");
                }
                
            }
            else
            return BadRequest("A categoria deve ser criada com o status (true)");
        }

        [HttpPut("{id}")]
        public IActionResult EditarCategoria(int id, [FromBody] UpdateCategoriaDto categoriaUpdateDto)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }
            _mapper.Map(categoriaUpdateDto, categoria);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCategoria(int id)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            if (categoria == null)
            {
                return NoContent();
            }
            _context.Remove(categoria);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet ]
        public IActionResult GetCategoria()
        {
            return Ok(_context.Categorias);
        }


        [HttpGet("{Id}")]
        public IActionResult GetCategoriaPorId(int id)
        {
                Categoria categoria= _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            if (categoria != null)
            {
                ReaderCategoriaDto reader = _mapper.Map<ReaderCategoriaDto>(categoria);
                return Ok(categoria);
            }
            return NotFound("Não encontrado");
        }

    }
}
