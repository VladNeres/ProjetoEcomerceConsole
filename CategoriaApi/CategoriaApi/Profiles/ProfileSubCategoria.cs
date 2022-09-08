using AutoMapper;
using CategoriaApi.Data.Dto.DtoSubCategoria;
using CategoriaApi.Model;

namespace CategoriaApi.Profiles
{
    public class ProfileSubCategoria:Profile
    {

        public ProfileSubCategoria()
        {
            CreateMap<CreateSubCategoriaDto, SubCategoria>();
            CreateMap<SubCategoria, ReadSubCategoriaDto>();
            CreateMap<UpdateSubCategoriaDto, SubCategoria>();
        }
    }
}
