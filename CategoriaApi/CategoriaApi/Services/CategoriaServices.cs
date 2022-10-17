using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoCategoria;
using CategoriaApi.Model;
using Dapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CategoriaApi.Services
{
    public class CategoriaServices
    {
        private DatabaseContext _context;
        private IMapper _mapper;
        private readonly IDbConnection _dbConnection;

        public CategoriaServices(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadCategoriaDto AdicionarCategoria(CreateCategoriaDto categoriaDto)
        {
            Categoria categoriaNome = _context.Categorias.FirstOrDefault(categoriaNome => categoriaNome.Nome.ToUpper() == categoriaDto.Nome.ToUpper());

            if (categoriaDto.Nome.Length >= 3 && categoriaDto.Nome.Length <= 50)
            {
                if (categoriaNome == null)
                {
                    Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
                    categoria.DataCriacao = DateTime.Now;
                    categoria.Status = true;
                    _context.Categorias.Add(categoria);
                    _context.SaveChanges();
                    return _mapper.Map<ReadCategoriaDto>(categoria);

                }
                throw new ArgumentException();
            }
            throw new Exception();
        }

        public Result EditarCategoria(int id, UpdateCategoriaDto categoriaDto)
        {
            Categoria categorias = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            IEnumerable<SubCategoria> subCategorias = _context.SubCategorias.Where(sub => sub.CategoriaId == id);

            if (categorias == null)
            {
                return Result.Fail("Categoria não encontrada");
            }
            if (categoriaDto.Status == false || categoriaDto.Status == true)
            {
                foreach (var subCategoria in subCategorias)
                {
                    subCategoria.Status = categoriaDto.Status;
                }
            }
            _mapper.Map(categoriaDto, categorias);
            categorias.DataAtualizacao = DateTime.Now;
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result DeletarCategoria(int id)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            if (categoria == null)
            {
                return Result.Fail("Id não encontrado");
            }
            _context.Remove(categoria);
            _context.SaveChanges();
            return Result.Ok();
        }

        public List<ReadCategoriaDto> GetCategoria(string nome, bool? status, int quantidadePorPagina, string ordem)
        {
            List<Categoria> categorias = _context.Categorias.ToList(); ;
            if (categorias == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(nome))
            {
                IEnumerable<Categoria> query = from categoria in categorias
                                               orderby categoria.Nome ascending
                                               where categoria.Nome.ToUpper().StartsWith(nome.ToUpper())
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
            if (quantidadePorPagina > 0)
            {
                IEnumerable<Categoria> query = from categoria in categorias.Take(quantidadePorPagina)
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
            
             List<ReadCategoriaDto> readDto = _mapper.Map<List<ReadCategoriaDto>>(categorias);
            return readDto;

        
        }

        public ReadCategoriaDto GetCategoriaPorId(int id)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(cat => cat.Id == id);
            if (categoria != null)
            {
                ReadCategoriaDto readDto = _mapper.Map<ReadCategoriaDto>(categoria);
                return readDto;
            }
            return null;
        }
    }
}
