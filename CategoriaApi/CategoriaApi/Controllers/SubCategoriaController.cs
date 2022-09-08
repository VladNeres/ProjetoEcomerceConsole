using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoSubCategoria;
using CategoriaApi.Model;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CategoriaApi.Controllers
{
        [ApiController]
        [Route("[controller]")]

    public class SubCategoriaController: ControllerBase
    {
        private DatabaseContext _context;
        private IMapper _mapper;
        public SubCategoriaController (DatabaseContext context, IMapper mapper)
        {
            _context= context;
            _mapper=mapper;
        }

        [HttpPost]
        public IActionResult CriarSubCategoria([FromBody] CreateSubCategoriaDto subCategoriaDto)
        {
           SubCategoria subCategoriaNome = _context.SubCategorias.FirstOrDefault(subCategoria=> subCategoria.Nome.ToUpper()==subCategoriaDto.Nome.ToUpper());
            

                if(subCategoriaDto.Nome.Length>=3)
                {
                    if(subCategoriaNome== null)
                    {

                        SubCategoria subCategoria= _mapper.Map<SubCategoria>(subCategoriaDto);
                        _context.SubCategorias.Add(subCategoria);
                        _context.SaveChanges();
                        return CreatedAtAction(nameof(GetSubCategoriaPorId), new { id = subCategoria.Id }, subCategoriaDto);
                    }
                    return BadRequest("A subCategoria já existe");
                }
                return BadRequest("A categoria deve conter entre 3 e 50 caracteres");
            
            
        }

        [HttpPut("{id}")]
        public IActionResult EditarSubCategoria(int id, [FromBody] UpdateSubCategoriaDto subCategoriaDto) 
        {
            SubCategoria subCategoria = _context.SubCategorias.FirstOrDefault(subCategoria => subCategoria.Id == id);
            if(subCategoria== null)
            {
                return NotFound();
            }
            _mapper.Map(subCategoriaDto, subCategoria);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarSubCategoria(int id)
        {
            SubCategoria subCategoria = _context.SubCategorias.FirstOrDefault(subCategoria => subCategoria.Id == id);
            if(subCategoria== null)
            {
                return NotFound();
            }
            _context.Remove(subCategoria);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetSubCategorias()
        {
            return Ok(_context.SubCategorias);
        }

        [HttpGet("{id}")]

        public IActionResult GetSubCategoriaPorId(int id)
        {
            SubCategoria subCategoria = _context.SubCategorias.FirstOrDefault(subCategoria => subCategoria.Id == id);
            if(subCategoria != null)
            {
                ReadSubCategoriaDto readerSubCategoria = _mapper.Map<ReadSubCategoriaDto>(subCategoria);
                return Ok(subCategoria);
            }
            return NotFound();
        }
    }
}
