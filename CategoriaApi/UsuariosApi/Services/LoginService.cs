using CategoriaApi.Model;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UsuariosApi.Data.Requests;
using UsuariosApi.Models;
using UsuariosApi.Requests;

namespace UsuariosApi.Services
{
    public class LoginService
    {
        private SignInManager<CustomIdentityUser> _signInManager;
        private TokenService _tokenService;
        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogarUsuario(LoginRequest request)
        {
            var resultadoIdentity = _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            var identityResult = _signInManager.UserManager.Users.FirstOrDefault(identity => identity.UserName == request.UserName);
            if(identityResult.EmailConfirmed == false)
            {
               return Result.Fail("A conta não foi validada");
            }
            if (resultadoIdentity.Result.Succeeded)
            {
                    var identityUser = _signInManager.UserManager.Users .FirstOrDefault(usuario => usuario.NormalizedUserName == request.UserName.ToUpper());
                    Token token =_tokenService.CreateToken(identityUser, _signInManager.UserManager
                        .GetRolesAsync(identityUser).Result.FirstOrDefault());
                    return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Login ou senha incorreto");
        }

        public CustomIdentityUser RecuperaUsuarioPeloEmail(string email)
        {
            return _signInManager.UserManager.Users
                            .FirstOrDefault(usuario => usuario.NormalizedEmail == email.ToUpper());
        }
        

        public Result ReseteSenhaUsuario(EfetuaResetRequest request)
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPeloEmail(request.Email);
            IdentityResult resultadoIdentity = _signInManager.UserManager
                .ResetPasswordAsync(identityUser, request.Token, request.Password).Result;

            if (resultadoIdentity.Succeeded) return Result.Ok().WithSuccess("Senha alterada com sucesso");
            return Result.Fail("Falha ao solicitar a troca de senha");
        }


        public Result SolicitaReseteSenhaUsuario(SolicitaResetRequest request)
        {
            CustomIdentityUser identityUser = RecuperaUsuarioPeloEmail(request.Email);

            if(identityUser!= null)
            {
                string codigoDeRecuperacao = _signInManager.UserManager
                    .GeneratePasswordResetTokenAsync(identityUser).Result;

                return Result.Ok().WithSuccess(codigoDeRecuperacao);
            }
            return Result.Fail("Falha ao solicitar redefinição da senha");
        }
    }
}
