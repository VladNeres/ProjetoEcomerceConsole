using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using UsuariosApi.Data.Requests;
using UsuariosApi.Requests;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult loginUsuario(LoginRequest request)
        {
            try
            {

                Result resultado = _loginService.LogarUsuario(request);
                if (resultado.IsFailed) return Unauthorized(resultado.Errors);
                return Ok(resultado.Successes);
            }
            catch(ArgumentException e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost("/solicita-reset")]
        public IActionResult SolicitaReseteSenhaUsuario(SolicitaResetRequest request)
        {
            Result resultado = _loginService.SolicitaReseteSenhaUsuario(request);
            if(resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Successes);
        }
        
        [HttpPost("/efetua-reset")]
        public IActionResult ReseteSenhaUsuario(EfetuaResetRequest request)
        {
            Result resultado = _loginService.ReseteSenhaUsuario(request);
            if(resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado.Successes);
        }
    }
}
