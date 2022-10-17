using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoProduto;
using CategoriaApi.Model;
using CategoriaApi.Repository;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace CategoriaApi.Services
{
    public class ProdutoServices
    {
        private DatabaseContext _context;
        private IMapper _mapper;
        private ProdutoRepository _produtoRepository;

        public ProdutoServices(DatabaseContext context, IMapper mapper, ProdutoRepository produtoRepos)
        {
            _produtoRepository = produtoRepos;

            _context = context;
            _mapper = mapper;
        }


        public ReadProdutoDto AdicionarProduto(CreateProdutoDto produtoDto)
        {

            SubCategoria subId = _produtoRepository.SubCategoriaID(produtoDto);
            Produto produtoNome = _context.Produtos.FirstOrDefault(produtoNome => produtoNome.Nome.ToUpper() == produtoDto.Nome.ToUpper());


            if (produtoDto.Nome.Length<3 || produtoDto.Nome.Length>50)
            {
                throw new ArgumentException();
            }
            if (subId == null)
            {
                throw new NullReferenceException();
            }
            if (subId.Status == false)
            {
                throw new ArgumentException();
            }
            if (produtoNome == null)
            {
                Produto produto = _mapper.Map<Produto>(produtoDto);
                produto.DataCriacao = DateTime.Now;
                produto.CategoriaId = subId.CategoriaId;
                produto.Status = true;
                _context.Produtos.Add(produto);
                _context.SaveChanges();
                return _mapper.Map<ReadProdutoDto>(produto);

            }
            throw new Exception();
        }


        public Result AtualizarProduto(int id, UpdateProdutoDto produtoDto)
        {
            Produto produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
            if (produto == null)
            {
                return Result.Fail("Produto não encontrado");
            }
            _mapper.Map(produtoDto, produto);
            produto.DataAtualizacao = DateTime.Now;
            _context.SaveChanges();
            return Result.Ok();

        }

        public Result DeletarProduto(int id)
        {
            Produto produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
            if (produto == null)
            {
                return Result.Fail("Produto não encontrado");
            }
            _context.Remove(produto);
            _context.SaveChanges();
            return Result.Ok();
        }

        public ReadProdutoDto GetProdutoPorId(int id)
        {
            Produto produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
            if (produto != null)
            {
                ReadProdutoDto readDto = _mapper.Map<ReadProdutoDto>(produto);
                return readDto;
            }
            return null;
        } 

    }
}
