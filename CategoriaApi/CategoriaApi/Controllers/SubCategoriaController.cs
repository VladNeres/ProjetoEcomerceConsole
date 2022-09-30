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
            try
            {

                if(subCategoriaDto.Nome.Length>=3 && subCategoriaDto.Nome.Length<=50)
                {
                    if(subCategoriaNome== null)
                    {

                        SubCategoria subCategoria= _mapper.Map<SubCategoria>(subCategoriaDto);
                        subCategoria.Status = true;
                        subCategoria.DataCriacao = DateTime.Now;
                        _context.SubCategorias.Add(subCategoria);
                        _context.SaveChanges();
                        return CreatedAtAction(nameof(GetSubCategoriaPorId), new { id = subCategoria.Id }, subCategoriaDto);
                    }
                    return BadRequest("A subCategoria já existe");
                }
                return BadRequest("A categoria deve conter entre 3 e 50 caracteres");
            
            }
            catch (Exception)
            {
                return BadRequest("É necessario Informar um Id da categoria em que deseja registrar a Subcategoria");
            }

            
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
            subCategoria.DataAtualizacao = DateTime.Now;
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
        public IActionResult GetSubCategorias([FromQuery] string nomeSubCategoria, [FromQuery] bool? statusSubCategoria, [FromQuery] string ordem,
            int quantidadeSub)
        {
            List<SubCategoria> subCategorias = _context.SubCategorias.ToList();
            if (subCategorias == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(nomeSubCategoria))
            {
                IEnumerable<SubCategoria> query = from subCategoria in subCategorias
                                                  where subCategoria.Nome.ToUpper().StartsWith(nomeSubCategoria.ToUpper())
                                                  select subCategoria;
                subCategorias = query.ToList();
            }
            if (statusSubCategoria == true || statusSubCategoria == false)
            {
                IEnumerable<SubCategoria> query = from subCategoria in subCategorias
                                                  where subCategoria.Status == statusSubCategoria
                                                  select subCategoria;
                subCategorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordem) && ordem.ToUpper() == "CRESCENTE")
            {
                IEnumerable<SubCategoria> query = from subCategoria in subCategorias
                                                  orderby subCategoria.Nome ascending
                                                  select subCategoria;
                subCategorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordem) && ordem.ToUpper() == "DECRESCENTE")
            {
                IEnumerable<SubCategoria> query = from subCategoria in subCategorias
                                                  orderby subCategoria.Nome descending
                                                  select subCategoria;
                subCategorias = query.ToList();
            }
            if (quantidadeSub > 0)
            {
                IEnumerable<SubCategoria> query= from subCategoria in subCategorias.Take(quantidadeSub)
                                                 select subCategoria;

                subCategorias = query.ToList();
            }
            

            List<ReadSubCategoriaDto> readSubDto = _mapper.Map<List<ReadSubCategoriaDto>>(subCategorias);
            return Ok(subCategorias);
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
