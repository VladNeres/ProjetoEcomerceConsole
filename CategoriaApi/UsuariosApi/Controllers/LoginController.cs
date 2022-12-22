using FluentResults;
using Microsoft.AspNetCore.Mvc;
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
            Result resultado = _loginService.LogarUsuario(request);
            if (resultado.IsFailed) return Unauthorized();
            return Ok();
        }
    }
}
