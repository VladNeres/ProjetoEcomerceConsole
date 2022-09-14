using AutoMapper;
using CategoriaApi.Data.Dto.DtoSubCategoria;
using CategoriaApi.Model;
using System;

namespace CategoriaApi.Profiles
{
    public class ProfileSubCategoria:Profile
    {

        public ProfileSubCategoria()
        {
            CreateMap<CreateSubCategoriaDto, SubCategoria>();
            CreateMap<UpdateSubCategoriaDto, SubCategoria>();
            CreateMap<SubCategoria, ReadSubCategoriaDto>()
                .ForMember(categoria => categoria.DataCriacao, opt => opt
                .MapFrom(src => ((DateTime)src.DataCriacao).ToString("dd-MM-yyyy HH:mm:ss")));
        }
    }
}
