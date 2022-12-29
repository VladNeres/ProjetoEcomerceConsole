using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Data.Requests;
using UsuariosApi.Exceptions;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController: ControllerBase
    {
        private CadastroService _cadastroService;
        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]

        public IActionResult CadastraUsuario(CreateUsuarioDto usuarioDto)
        {
            try
            {
                Result resultado = _cadastroService.CadastroUsuario(usuarioDto);
                    if (resultado.IsFailed) return StatusCode(500);
                return Ok(resultado.Successes);
            }
            catch (FormatException e )
            {
                return BadRequest(e.Message);
            }
            catch (NullException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/ativa")]
        public IActionResult AtivaCadastroUsuario(AtivaContaRequest request)
        {
            Result resultado = _cadastroService.AtivaContaUsuario(request);
            if(resultado.IsFailed) return StatusCode(500);
            return Ok(resultado.Successes);
        }
    }
}
