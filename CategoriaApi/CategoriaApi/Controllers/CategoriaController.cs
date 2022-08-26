

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
        private static int Id = 1;
        private static List<Categoria> categorias = new List<Categoria>();

        [HttpPost]
        public IActionResult AdicionarCategoria([FromBody] Categoria categoria)
        {
            if (categoria.Status== true)
            {
                categoria.Id = Id++;
                categorias.Add(categoria);
                Console.WriteLine(categoria.Nome);
                return CreatedAtAction(nameof(GetCategoriaPorId), new { id = categoria.Id }, categoria);
            }
            else
            return BadRequest("A categoria deve ser criada com o status (true)");
        }

        [HttpGet ]
        public IActionResult GetCategoria()
        {
            return Ok(categorias);
        }


        [HttpGet("{Id}")]
        public IActionResult GetCategoriaPorId(int id)
        {
                Categoria categoria= categorias.FirstOrDefault(categoria => categoria.Id == id);
            if (categoria != null)
            {
                return Ok(categoria);
            }
            return NotFound("Não encontrado");
        }
    }
}
