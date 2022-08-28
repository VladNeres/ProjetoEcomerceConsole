

using CategoriaApi.Data;
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

        public CategoriaController(CategoriaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionarCategoria([FromBody] Categoria categoria)
        {
            if (categoria.Status== true)
            {
                _context.Categorias.Add(categoria);
                    _context.SaveChanges();
                Console.WriteLine(categoria.Nome);
                return CreatedAtAction(nameof(GetCategoriaPorId), new { id = categoria.Id }, categoria);
                
            }
            else
            return BadRequest("A categoria deve ser criada com o status (true)");
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
                return Ok(categoria);
            }
            return NotFound("Não encontrado");
        }

        [HttpPut("{id}")]
        public IActionResult EditarCategoria(int id, [FromBody] Categoria novoNome)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }
            categoria.Nome = novoNome.Nome;
            categoria.Status = novoNome.Status;
            categoria.DataCriacao = categoria.DataCriacao;
            categoria.DataAlteracao = novoNome.DataAlteracao = DateTime.Now.ToString("dd-MM-yyyy:HH:mm:ss") ;
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
    }
}
