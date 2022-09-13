

using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoCategoria;
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
        private DatabaseContext _context;
        private IMapper _mapper;

        public CategoriaController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionarCategoria([FromBody] CreateCategoriaDto categoriaDto)
        {

            Categoria categoriaNome = _context.Categorias.FirstOrDefault(categoriaNome => categoriaNome.Nome.ToUpper() == categoriaDto.Nome.ToUpper());
            
            if (categoriaDto.Nome.Length >= 3 )
            {
                if(categoriaNome == null)
                {
                    Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
                     categoria.DataCriacao = DateTime.Now;
                     categoria.Status = true;
                            _context.Categorias.Add(categoria);
                            _context.SaveChanges();
                            Console.WriteLine(categoria.Nome);
                            return CreatedAtAction(nameof(GetCategoriaPorId), new { id = categoria.Id }, categoriaDto);                
                }
                return BadRequest("(Atenção)!.\n A categoria já existe!");
            }
                return BadRequest("Para criar uma categoria,o campo (Nome) deve conter de 3 a 50 caracteres\n" +
                    "e o Status deve ser verdadeiro (true)");
            
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
            categoria.DataAtualizacao = DateTime.Now;
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
