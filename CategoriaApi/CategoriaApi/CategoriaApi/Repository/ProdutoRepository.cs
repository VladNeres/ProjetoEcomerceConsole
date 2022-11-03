﻿using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoProduto;
using CategoriaApi.Model;
using Dapper;
using Dapper.Contrib.Extensions;
using FluentResults;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CategoriaApi.Repository
{
    public class ProdutoRepository
    {

        private DatabaseContext _context;
        private IMapper _mapper;
        private readonly IDbConnection _dbConnection;

        public ProdutoRepository(DatabaseContext context, IDbConnection conection,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _dbConnection = conection;
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            var result = await _dbConnection.GetAsync<Produto>(id);
            return result;
        }

        public async Task<IReadOnlyList<Produto>> GetAllAsync()
        {

            var result = await _dbConnection.GetAllAsync<Produto>();
            return result.ToList();
        }

        public void AddProduto(Produto produto)
        {

            _context.Add(produto);
            _context.SaveChanges();

        }

        public Produto DeleteProduto(Produto produto)
        {
            var result = _context.Produtos.FirstOrDefault(x => x.Id == produto.Id);
            _context.SaveChanges();
            return result;
        }

        public Produto RecuperarProdutoNome(CreateProdutoDto prodDto)
        {
            var produto = _context.Produtos.FirstOrDefault(prod => prod.Nome.ToUpper() == prodDto.Nome.ToUpper());
            return produto;
        }

        public Produto RecuperarProdutoId(int id )
        {
            var produto = _context.Produtos.FirstOrDefault(produto=> produto.Id==id );
            return produto;
        }

        public List<Produto> RecuperarListaDeProdutos()
        {
            return _context.Produtos.ToList();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

       
        public SubCategoria SubCategoriaID(CreateProdutoDto produtoDto)
        {
                SubCategoria sub= _context.SubCategorias.FirstOrDefault(sub=> sub.Id == produtoDto.SubCategoriaId);
                return sub;
        }

        public List<Produto>PesquisaComFiltros(string nome, bool? status, double? peso, double? altura, double? largura,
             double? comprimento, double? valor, int? estoque, string ordem, int itensPorPagina, int pagina)
        {
            var sql = "SELECT * FROM Produtos WHERE ";

            if (nome != null)
            {
                sql += "Nome LIKE \"%" + nome + "%\" and ";
            }
            if (status != null)
            {
                sql += "Status = @status and ";
            }
            if (peso != null)
            {
                sql += "Peso = @peso and ";
            }
            if (altura != null)
            {
                sql += "Altura = @altura and ";
            }
            if (largura != null)
            {
                sql += "Largura = @largura and ";
            }
            if (comprimento != null)
            {
                sql += "Comprimento = @comprimento and ";
            }
            if (valor != null)
            {
                sql += "Valor = @valor and ";
            }
            if (estoque != null)
            {
                sql += "Estoque = @estoque and ";
            }

            if (nome  == null && estoque == null && valor == null && comprimento == null && largura == null &&
                altura == null && peso == null && status == null)
            {
                var PosicaoDoWhere = sql.LastIndexOf("WHERE");
                sql = sql.Remove(PosicaoDoWhere);
            }
            else
            {
                var posicaoDoAnd = sql.LastIndexOf("and");
                sql = sql.Remove(posicaoDoAnd);
            }
            if (ordem != null)
            {
                if (ordem.ToUpper() != "DECRESCENTE")
                {
                    sql += " ORDER BY Nome";
                }
                else
                {
                    sql += " ORDER BY Nome DESC";
                }
            }
            if (itensPorPagina > 0)
            {
                sql.Take(itensPorPagina);
            }

            var result = _dbConnection.Query<Produto>(sql, new
            {
                Nome = nome,
                Status = status,
                Peso = peso,
                Altura = altura,
                Largura = largura,
                Comprimento = comprimento,
                Valor = valor,
                Estoque = estoque
            });
            if(pagina>0 && itensPorPagina>0 )
            {
                var resultado = result.Skip((pagina -1)* itensPorPagina).Take(itensPorPagina).ToList();
                return resultado;
            }
            var resultadoSemPaginacao = result.Skip(0).Take(itensPorPagina).ToList();
            return resultadoSemPaginacao;

        }
    }
}

