using AutoMapper;
using CategoriaApi.Data.Dto.DtoCategoria;
using CategoriaApi.Model;
using System;
using System.Linq;

namespace CategoriaApi.Profiles
{
    public class CategoriaProfile : Profile
    {
     
        public CategoriaProfile()
        {

            CreateMap<CreateCategoriaDto, Categoria>();
            CreateMap<UpdateCategoriaDto, Categoria>();
            CreateMap<Categoria, ReaderCategoriaDto>()
               .ForMember(categoria => categoria.SubCategoria, opts => opts
                .MapFrom(categoria => categoria.SubCategoria.Select(subCategoria => new
                {
                    subCategoria.Id,
                    subCategoria.Nome,
                    subCategoria.Status
                })));
        //.ForMember(categoria => categoria.DataCriacao, opt => opt
        //        .MapFrom(src => ((DateTime)src.DataCriacao).ToString("dd-MM-yyyy HH:mm:ss")));
        }
    }
}       
