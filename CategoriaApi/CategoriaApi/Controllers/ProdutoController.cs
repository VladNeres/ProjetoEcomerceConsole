

using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoProduto;
using CategoriaApi.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CategoriaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private DatabaseContext _context;
        private IMapper _mapper;

        public ProdutoController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionarProduto([FromBody] CreateProdutoDto produtoDto)
        {
            try
            {
                List<SubCategoria> listaSub = _context.SubCategorias.ToList();
               SubCategoria subCategorias = _context.SubCategorias.FirstOrDefault(sub=>sub.Id !=0);
                IEnumerable <Produto> produtoId= _context.Produtos.Where(prod=> prod.SubCategoriaId == (subCategorias.Id));


                if (produtoId == null)
                {

                       if( subCategorias.Status==false )
                       {
                         return BadRequest("Não é possivel criar um produto em uma subCategoria inativa");
                       }
                    return BadRequest("Não é possivel criar um produto em uma subCategoria inativa");

                }
                Produto produto = _mapper.Map<Produto>(produtoDto);
                    produto.DataCriacao = DateTime.Now;
                    _context.Produtos.Add(produto);
                    _context.SaveChanges();
                    return CreatedAtAction(nameof(GetProdutoPorId), new { Id = produto.Id }, produto);
            }
            catch (Exception)
            {
                return BadRequest("É necessario informar o numero da subcategoria");
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualiazarProduto(int id, [FromBody] UpdateProdutoDto produtoDto)
        {
            Produto produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            _mapper.Map(produtoDto, produto);
            produto.DataAtualizacao = DateTime.Now;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetProdutos()
        {
            return Ok(_context.Produtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetProdutoPorId(int id)
        {
            Produto produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
            if (produto != null)
            {
                ReadProdutoDto readDto = _mapper.Map<ReadProdutoDto>(produto);
                return Ok(readDto);
            }
            return NotFound();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteProduto( int id)
        {
            Produto produto = _context.Produtos.FirstOrDefault(prod => prod.Id == id);
            
            if(produto == null)
            {
                return NotFound();
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
