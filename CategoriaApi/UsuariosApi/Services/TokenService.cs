﻿using CategoriaApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApi.Exceptions;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class TokenService
    {
        public Token CreateToken(CustomIdentityUser usuario, string role)
        {
            
            
            if(role == null)
            {

              throw new ArgumentException("Requisições de autorização não foram preenchidas no token " +
                  "(usuario, id, tipo de usuario, Idade do usuario)");
            } 

                    Claim[] direitosUsuario = new Claim[]
                    {
                        new Claim("username", usuario.UserName),
                        new Claim("id",usuario.Id.ToString()),
                        new Claim(ClaimTypes.Role, role),
                        new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString())
                    };
                var chave = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn")
                    );

                var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
                var Token = new JwtSecurityToken(
                    claims: direitosUsuario,
                    signingCredentials: credenciais,
                    expires: DateTime.UtcNow.AddHours(1));

                var tokenString = new JwtSecurityTokenHandler().WriteToken(Token);
                return new Token(tokenString);
            
            
            
        }

    }
}
