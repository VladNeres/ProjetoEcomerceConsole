using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Exceptions;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public bool VerificaCPF(string cpf)
        {
            CPFCNPJ.IMain checkCPF = new CPFCNPJ.Main();
            var resultCPF = checkCPF.IsValidCPFCNPJ(cpf);
            if (resultCPF == false) throw new NullException("Cpf Invalido! por favor digite um CPF valido");
            return resultCPF;
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var verificaEmail = new MailAddress(email);
                return verificaEmail.Address.Equals(email);
            }
            catch (FormatException)
            {
                throw new FormatException("formato de e-mail invalido");
            }
        }
        public async Task<Endereco> ViaCep(string cep)
        {
            HttpClient client = new HttpClient();
            var requisicao = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            var resposta = await requisicao.Content.ReadAsStringAsync();
            if (!requisicao.IsSuccessStatusCode)
            {
                throw new NullException();
            }
            var endereco = JsonConvert.DeserializeObject<Endereco>(resposta);
            return endereco;
        }
        public Result CadastroUsuario(CreateUsuarioDto createDto)
        {
            
            var endereco = ViaCep(createDto.Endereco.CEP);
            var emailValido = IsValidEmail(createDto.Email);
            var verificaCPF = VerificaCPF(createDto.CPF);
            Usuario  usuario= _mapper.Map<Usuario>(createDto);
            usuario.Status = true;
            usuario.DataCriacao= DateTime.Now;
            usuario.Endereco = endereco.Result;
            usuario.Endereco.Numero = createDto.Endereco.Numero;
            usuario.Endereco.Complemento = createDto.Endereco.Complemento;
            //mapeando de um Usuario para um Identityuser
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            var resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Repassword);
            if (resultadoIdentity.Result.Succeeded) return Result.Ok();
            return Result.Fail("Falha ao cadastrar usuario");
        }
    }
}
    