﻿using AutoMapper;
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
using CategoriaApi.Repository;
using CategoriaApi.Exceptions;

namespace CategoriaApi.Services
{
    public class SubCategoriaService
    {
        public SubCategoriaRepository _repository;
        public IMapper _mapper;
        public readonly IDbConnection _dbConnection;
        public SubCategoriaService(SubCategoriaRepository repository, IMapper mapper, IDbConnection dbConnection)
        {
            _repository = repository;
            _mapper = mapper;
            _dbConnection= dbConnection;
        }

        public ReadSubCategoriaDto CriarSubCategoria(CreateSubCategoriaDto subCategoriaDto)
        {
            SubCategoria subCategoriaNome = _repository.RecuperarSubNome(subCategoriaDto);
            Categoria subId =  _repository.RecuperarCategoriaId(subCategoriaDto);

            if(subId == null || subId.Status== false )
            {
                throw new InativeObjectException();
            }
            if (subCategoriaDto.Nome.Length >= 3 && subCategoriaDto.Nome.Length <= 50)
            {
                if (subCategoriaNome == null)
                {

                    SubCategoria subCategoria = _mapper.Map<SubCategoria>(subCategoriaDto);
                    subCategoria.Status = true;
                    subCategoria.DataCriacao = DateTime.Now;
                    _repository.AddSubCategoria(subCategoria);
                    return _mapper.Map<ReadSubCategoriaDto>(subCategoria);
                }
                throw  new AlreadyExistsExceprion ("A já existe uma subCategoria com esse nome");
            }
            throw new MinCaracterException("A categoria deve conter entre 3 e 50 caracteres");
        }

        public Result EditarSubCategoria(int id, UpdateSubCategoriaDto subDto)
        {
            SubCategoria subCategoria = _repository.RecuperarSubId(id);
            if (subCategoria == null)
            {
                return Result.Fail("Subcategoria não encontrada");
            }
            if(subCategoria.Produtos.Count()> 0 && subDto.Status!= true)
            {
                return Result.Fail("Não é possivel desativar essa subcategoria pois existem produtos cadastrados");
            }
                _mapper.Map(subDto, subCategoria);
                subCategoria.DataAtualizacao = DateTime.Now;
                _repository.Salvar();
                return Result.Ok();
        }
        public Result DeletarSubCategoria(int id)
        {
            SubCategoria subCategoria = _repository.RecuperarSubId(id);
            if (subCategoria == null)
            {
                return Result.Fail("Subcategoria não encontrada");
            }
            _repository.DeletarSubCategoria(subCategoria);
            return Result.Ok();
        }


        public List<ReadSubCategoriaDto> GetSubCategoria(string nome, bool? status, string ordem, int quantidadePorPagina)
        {
            List<SubCategoria> subcategorias = _repository.RecuperarListaDeSub() ;
            if (subcategorias == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(nome))
            {
                IEnumerable<SubCategoria> query = from subcategoria in subcategorias
                                               orderby subcategoria.Nome ascending
                                               where subcategoria.Nome.ToUpper().StartsWith(nome.ToUpper())
                                               select subcategoria;

                subcategorias = query.ToList();

            }
            if (status == true || status == false)
            {
                IEnumerable<SubCategoria> query = from subcategoria in subcategorias
                                               where subcategoria.Status == status
                                               select subcategoria;
                subcategorias = query.ToList();
            }
            if (quantidadePorPagina > 0)
            {
                IEnumerable<SubCategoria> query = from subcategoria in subcategorias.Take(quantidadePorPagina)
                                               select subcategoria;
                subcategorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordem) && ordem.ToUpper() == "CRESCENTE")
            {
                IEnumerable<SubCategoria> query = from subcategoria in subcategorias
                                               orderby subcategoria.Nome ascending
                                               select subcategoria;
                subcategorias = query.ToList();
            }
            if (!string.IsNullOrEmpty(ordem) && ordem.ToUpper() == "DECRESCENTE")
            {
                IEnumerable<SubCategoria> querydecres = from subcategoria in subcategorias
                                                     orderby subcategoria.Nome descending
                                                     select subcategoria;
                subcategorias = querydecres.ToList();
            }

            List<ReadSubCategoriaDto> readDto = _mapper.Map<List<ReadSubCategoriaDto>>(subcategorias);
            return readDto;


        }

        public ReadSubCategoriaDto GetSubPorId(int id)
        {
            SubCategoria subCategoria = _repository.RecuperarSubId(id);
            if (subCategoria != null)
            {
                ReadSubCategoriaDto readSubCategoria = _mapper.Map<ReadSubCategoriaDto>(subCategoria);
                return readSubCategoria;
            }
            return null;
        }
    }
}
