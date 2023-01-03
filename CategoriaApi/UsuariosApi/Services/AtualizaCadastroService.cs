using AutoMapper;
using CategoriaApi.Model;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.Dtos;

namespace UsuariosApi.Services
{
    public class AtualizaCadastroService
    {
        private CadastroService _cadastroService;
        private UserManager<CustomIdentityUser> _userManager;
        private IMapper _mapper;
        public AtualizaCadastroService(CadastroService cadastroService,UserManager<CustomIdentityUser> manager, IMapper mapper)
        {
            _userManager = manager;
            _mapper = mapper;
            _cadastroService = cadastroService;
        }


        public async Task<Result> AtualizarCadastro(int id,UpdateUsuarioDto updateUserDto)
        {
            var username = _userManager.Users
                .FirstOrDefault(usuario => usuario.Id == id);

            if(username == null)
            {
                return Result.Fail("Usuario não encontrado");
            }

         var cpfValido=   _cadastroService.VerificaCPF(updateUserDto.CPF);
         var endereco =   _cadastroService.ViaCep(updateUserDto.CEP);
            updateUserDto.UF = endereco.Result.UF;
            updateUserDto.Localidade = endereco.Result.Localidade;
            updateUserDto.Bairro = endereco.Result.Bairro;
            updateUserDto.Logradouro = endereco.Result.Logradouro;
            updateUserDto.Numero = endereco.Result.Numero;
            updateUserDto.Complemento = endereco.Result.Complemento;
            updateUserDto.DataAtualizacao = System.DateTime.Now;
            _mapper.Map(updateUserDto,username);
            await _userManager.UpdateAsync(username);
            return Result.Ok();

        }
    }
}
