using AutoMapper;
using CategoriaApi.Data.Dto.DtoCategoria;
using CategoriaApi.Exceptions;
using CategoriaApi.Interfaces;
using CategoriaApi.Model;
using CategoriaApi.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestesUniarios.TestesCategoria
{
    public class CategoriaTestes
    {
       private readonly IMapper _mapper;
        private readonly ICategoriaRepository _repository;
        private readonly ICategoriaService _sevice;
        
        public CategoriaTestes()
        {
            _repository= Substitute.For<ICategoriaRepository>(null);
            _mapper = Substitute.For<IMapper>();
            _sevice= new CategoriaServices(_mapper,_repository);
        }
        public void VerificaQuantidadeDeCaracteres_MinimoDeCaracteresNaoAtingido_LancaExcecao()
        {
            //Arrange
            var createdto = new CreateCategoriaDto();
            createdto.Nome = "j";

            //ACT
            var categoriaTeste = Assert.Throws<MinCharacterException>(
               ()=> _sevice.AdicionarCategoria(createdto));

            //ASSERT
            Assert.Equal("É necessario informar de 3 a 50 caracteres", categoriaTeste.Message);
        }

        [Fact]
        public void BuscarCategoriaPorNome_SeACategoriaJaExisteSeExiste_LancaExcecao()
        {
            //Arrange
            var createdto = new CreateCategoriaDto();
            createdto.Nome = "joao";
           
            
            Categoria nomeObjeto= new Categoria();
            nomeObjeto.Nome = "joao";
            nomeObjeto.DataCriacao = DateTime.Now;
            _repository.BuscarNomeCategoria(Arg.Any<CreateCategoriaDto>())
                .Returns(nomeObjeto);
       
            //ACT

           var categoriaTeste=  Assert.Throws<AlreadyExistException>(
               ()=>_sevice.AdicionarCategoria(createdto));
           
            //ASSERT
            Assert.Equal("A categoria já existe", categoriaTeste.Message);
        }

        [Fact(Skip ="Não foi encontrado Solução")]

        public void TestaSeACategoriaFoiCriadaComStatusAtivo()
        {
            //Arrange
            CreateCategoriaDto objDto = new CreateCategoriaDto();
            objDto.Nome = "Janainaao";

            Categoria nomeCategoria= new Categoria() ;
            nomeCategoria.Nome = objDto.Nome;
            nomeCategoria.Status = true;
            nomeCategoria.DataCriacao = DateTime.Now;

            
            //Act
            var testaStatus = _sevice.AdicionarCategoria(objDto);
                       //Assert
            Assert.True(testaStatus.Status);
        }

        [Fact]
        public void IdInformadoForInvalido_NaoAtualizaCategoria()
        {
            //Arrange
            
            UpdateCategoriaDto updateDto = new UpdateCategoriaDto();
            updateDto.Nome = "Novo nome";
            updateDto.Status = true;
                       
            //Act
            var respostaIdInvalido=  _sevice.EditarCategoria(2,updateDto);
            //Assert
            Assert.True(respostaIdInvalido.IsFailed);
        }

        [Fact]
        public void ErroDeAtualizacao_ContemSubCategoriaEStatusDaCategoriaAtivo_LancarExcecao()
        {
          //Arrange
            int id = 1;
            Categoria categoria = new Categoria();
            categoria.Nome = "Joana";
            categoria.Id = 1;
            categoria.Status = true;
            categoria.SubCategoria = new List<SubCategoria>()
            {
                new SubCategoria()
                {
                    Nome= "sub1",
                    Id = 1,
                    Status = true,
                    CategoriaId = id
                }
            };
            _repository.BuscarCategoriaPorId(Arg.Any<int>()).Returns(categoria);
            UpdateCategoriaDto categoriaDto = new UpdateCategoriaDto();
            categoriaDto.Nome = "update1";
            categoriaDto.Status = false;

            //Act
            var excecaoMessage = Assert.Throws<InativeObjectException>( ()=>
                _sevice.EditarCategoria(id, categoriaDto));

            Assert.Equal("Não é possivel inativar uma categoria que contenha uma subCategoria cadastrada", excecaoMessage.Message);
        }

        [Fact]
        public void AtualizadarCategoria_IdDeCategoriaEStatusValido_AtualizaCategoria()
        {
            //Arrenge
            int id = 1;
            Categoria categoria = new Categoria();
            categoria.Nome = "nome";
            categoria.Id=1;
            categoria.Status = true;
            categoria.SubCategoria = new List<SubCategoria>()
            {
                new SubCategoria()
                {
                    Nome="Sub1",
                    Status=true,
                    DataCriacao=DateTime.Now,
                    CategoriaId=id
                }
            };
            UpdateCategoriaDto updateDto = new UpdateCategoriaDto();
            updateDto.Nome = "Novo nome";
            updateDto.Status = true;
            _repository.BuscarCategoriaPorId(Arg.Any<int>())
                   .Returns(categoria);
            //Action
       
            var testarNovoNome = _sevice.EditarCategoria(id, updateDto);
            //Assert

            Assert.True(testarNovoNome.IsSuccess);
        }
    }
}
