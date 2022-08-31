using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto;
using CategoriaApi.Model;

namespace CategoriaApi.Profiles
{
    public class CategoriaProfile : Profile
    {
     
        public CategoriaProfile()
        {

            CreateMap<CreateCategoriaDto, Categoria>();
            CreateMap<Categoria, ReaderCategoriaDto>();
            CreateMap<UpdateCategoriaDto, Categoria >();
        }
    }
}
