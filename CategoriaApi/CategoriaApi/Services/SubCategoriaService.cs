using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoSubCategoria;
using CategoriaApi.Model;
using FluentResults;
using System;
using System.Collections.Generic;
using Dapper;
using Dapper.Contrib.Extensions;

using System.Linq;
using System.Data;

namespace CategoriaApi.Services
{
    public class SubCategoriaService
    {
        public DatabaseContext _context;
        public IMapper _mapper;
        public readonly IDbConnection _dbConnection;
        public SubCategoriaService(DatabaseContext context, IMapper mapper, IDbConnection dbConnection)
        {
            _context= context;
            _mapper = mapper;
            _dbConnection= dbConnection;
        }

        public ReadSubCategoriaDto CriarSubCategoria(CreateSubCategoriaDto subCategoriaDto)
        {
            SubCategoria subCategoriaNome = _context.SubCategorias.FirstOrDefault(subCategoria => subCategoria.Nome.ToUpper() == subCategoriaDto.Nome.ToUpper());
            SubCategoria subId = _context.SubCategorias.FirstOrDefault(sub => sub.CategoriaId == null);

            if(subId == null)
            {
                throw new NullReferenceException();
            }
            if (subCategoriaDto.Nome.Length >= 3 && subCategoriaDto.Nome.Length <= 50)
            {
                if (subCategoriaNome == null)
                {

                    SubCategoria subCategoria = _mapper.Map<SubCategoria>(subCategoriaDto);
                    subCategoria.Status = true;
                    subCategoria.DataCriacao = DateTime.Now;
                    _context.SubCategorias.Add(subCategoria);
                    _context.SaveChanges();
                    return _mapper.Map<ReadSubCategoriaDto>(subCategoriaDto);
                }
                throw  new ArgumentException ("A subCategoria já existe");
            }
            throw new Exception("A categoria deve conter entre 3 e 50 caracteres");
        }

        public Result EditarSubCategoria(int id, UpdateSubCategoriaDto subDto)
        {
            SubCategoria subCategoria = _context.SubCategorias.FirstOrDefault(subCategoria => subCategoria.Id == id);
            IEnumerable<Produto> produto = _context.Produtos.Where(produto => produto.SubCategoriaId == id);
            if (subCategoria == null)
            {
                return Result.Fail("Subcategoria não encontrada");
            }
            if (subCategoria.Status == false || subCategoria.Status == true)
            {
                foreach (var item in produto)
                {
                    item.Status = subCategoria.Status;
                }
            }
                _mapper.Map(subDto, subCategoria);
                subCategoria.DataAtualizacao = DateTime.Now;
                _context.SaveChanges();
                return Result.Ok();
        }
        public Result DeletarSubCategoria(int id)
        {
            SubCategoria subCategoria = _context.SubCategorias.FirstOrDefault(subCategoria => subCategoria.Id == id);
            if (subCategoria == null)
            {
                return Result.Fail("Subcategoria não encontrada");
            }
            _context.Remove(subCategoria);
            _context.SaveChanges();
            return Result.Ok();
        }


        public List<ReadSubCategoriaDto> GetSubCategoria(string nome, bool? status, string ordem, int quantidadePorPagina)
        {
            List<SubCategoria> subcategorias = _context.SubCategorias.ToList(); ;
            if (subcategorias == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(nome))
            {
                IEnumerable<SubCategoria> query = from categoria in subcategorias
                                               orderby categoria.Nome ascending
                                               where categoria.Nome.ToUpper().StartsWith(nome.ToUpper())
                                               select categoria;

                subcategorias = query.ToList();

            }
            if (status == true || status == false)
            {
                IEnumerable<SubCategoria> query = from categoria in subcategorias
                                               where categoria.Status == status
                                               select categoria;
                subcategorias = query.ToList();
            }
            if (quantidadePorPagina > 0)
            {
                IEnumerable<SubCategoria> query = from categoria in subcategorias.Take(quantidadePorPagina)
                                               select categoria;
                subcategorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordem) && ordem.ToUpper() == "CRESCENTE")
            {
                IEnumerable<SubCategoria> query = from categoria in subcategorias
                                               orderby categoria.Nome ascending
                                               select categoria;
                subcategorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordem) && ordem.ToUpper() == "DECRESCENTE")
            {
                IEnumerable<SubCategoria> querydecres = from categoria in subcategorias
                                                     orderby categoria.Nome descending
                                                     select categoria;
                subcategorias = querydecres.ToList();
            }

            List<ReadSubCategoriaDto> readDto = _mapper.Map<List<ReadSubCategoriaDto>>(subcategorias);
            return readDto;


        }

        public ReadSubCategoriaDto GetSubPorId(int id)
        {
            SubCategoria subCategoria = _context.SubCategorias.FirstOrDefault(subCategoria => subCategoria.Id == id);
            if (subCategoria != null)
            {
                ReadSubCategoriaDto readSubCategoria = _mapper.Map<ReadSubCategoriaDto>(subCategoria);
                return readSubCategoria;
            }
            return null;
        }
    }
}
