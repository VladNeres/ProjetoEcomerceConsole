﻿using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Model
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string UF { get; set; }
        public string Localidade { get; set; }
        public string Cep  { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Status { get; set; }

        public DateTime DataAtualizacao { get; set; }

    }
}
