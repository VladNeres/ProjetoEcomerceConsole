

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

            if (categoriaDto.Nome.Length >= 3)
            {
                if (categoriaNome == null)
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
            Categoria categorias = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            IEnumerable<SubCategoria> subCategorias = _context.SubCategorias.Where(sub => sub.CategoriaId == id);

            if (categorias == null)
            {
                return NotFound();
            }
            if (categoriaUpdateDto.Status == false)
            {
                foreach (var subCategoria in subCategorias)
                {
                    subCategoria.Status = false;
                }
            }
            _mapper.Map(categoriaUpdateDto, categorias);
            categorias.DataAtualizacao = DateTime.Now;

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


        [HttpGet]
        public IActionResult GetCategoria([FromQuery] string nomeCategoria, [FromQuery] bool? status, [FromQuery] int quantidadeDeCategorias,
            [FromQuery] string ordem, [FromQuery] bool? temSub)
        {
            List<Categoria> categorias = _context.Categorias.ToList();
            if (categorias == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(nomeCategoria))
            {
                IEnumerable<Categoria> query = from categoria in categorias
                                               orderby categoria.Nome ascending
                                               where categoria.Nome.ToUpper().StartsWith(nomeCategoria.ToUpper())
                                               select categoria;

                categorias = query.ToList();

            }
            if (status == true || status == false)
            {
                IEnumerable<Categoria> query = from categoria in categorias
                                               where categoria.Status == status
                                               select categoria;
                categorias = query.ToList();
            }
            if (quantidadeDeCategorias > 0)
            {
                IEnumerable<Categoria> query = from categoria in categorias.Take(quantidadeDeCategorias)
                                               select categoria;
                categorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordem) && ordem.ToUpper() == "CRESCENTE")
            {
                IEnumerable<Categoria> query = from categoria in categorias
                                               orderby categoria.Nome ascending
                                               select categoria;
                categorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordem) && ordem.ToUpper() == "DECRESCENTE")
            {
                IEnumerable<Categoria> querydecres = from categoria in categorias
                                                     orderby categoria.Nome descending
                                                     select categoria;
                categorias = querydecres.ToList();
            }
            if (temSub == true && temSub != null)
            {
                IEnumerable<Categoria> query = from categoria in categorias
                                               where categoria.SubCategoria.Count() > 0
                                               select categoria;
                categorias = query.ToList();
            }
            if (temSub == false && temSub != null)
            {
                IEnumerable<Categoria> query = from categoria in categorias
                                               where categoria.SubCategoria.Count() == 0
                                               select categoria;
                categorias = query.ToList();
            }

            List<ReaderCategoriaDto> readDto = _mapper.Map<List<ReaderCategoriaDto>>(categorias);
            return Ok(readDto);

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
