using AutoMapper;

using CategoriaApi.Data;
using CategoriaApi.Model;
using Dapper;
using Dapper.Contrib.Extensions;
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
        private readonly IDbConnection _dbConnection;

        public ProdutoRepository( DatabaseContext context, IDbConnection conection)
        {
           
            _context = context;
            _dbConnection = conection;
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            var result = await _dbConnection.GetAsync<Produto>(id);
            return result;
        }

        public async Task<IReadOnlyList<Produto>>GetAllAsync()
        {
            
            var result = await _dbConnection.GetAllAsync<Produto>();
            return result.ToList();
        }

        public async Task<Produto> AddAsync(Produto entity)
        {
          
            entity.DataCriacao = DateTime.Now;
            entity.Id = await _dbConnection.InsertAsync( entity); 
            return entity;
        }

        public  async Task<bool> DeleteAsync(int id)
        {
            var result = await _dbConnection.DeleteAsync( new Produto {Id = id });
                return result;

        }

        public async Task<bool> UpdateAsync(Produto produto)
        {

            var result = await _dbConnection.UpdateAsync(produto);
            return result;
        }
    }
}

