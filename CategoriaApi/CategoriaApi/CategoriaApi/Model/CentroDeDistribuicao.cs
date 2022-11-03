﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Model
{
    public class CentroDeDistribuicao
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' '\s]{1,100}", ErrorMessage = "O campo nome deve conter apenas letras")]
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(128, ErrorMessage ="Tamanho máximo de 128 caracteres excedido")]
        public string Nome { get; set; }

        [RegularExpression(@"[a-zA-Zá-úÁ-Ú' '\s]{1,1000}", ErrorMessage = "O campo logradouro não deve conter caracteres especiais")]
        [StringLength(258, ErrorMessage ="Tamanho máximo de 258 caracteres excedido")]
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Logradouro { get; set; }
       
        [Required(ErrorMessage = "O campo número é obrigatório")]
        public int Numero { get; set; }
        
        [Required(ErrorMessage = "O campo complemento é obrigatório")]
        public string Complemento { get; set; }
        
        [Required(ErrorMessage = "O campo bairro  é obrigatório")]
        public string Bairro { get; set; } 
        
        [Required(ErrorMessage ="O campo cidade é obrigatório")]
        public string Localidade { get; set; }
        [Required(ErrorMessage = "O campo UF é obrigatório")]
        public string UF { get; set; }
        [Required(ErrorMessage = "O campo CEP é obrigatório")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "O campo Status é obrigatório")]
        public bool Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
       
        [JsonIgnore]
        public virtual List<Produto> Produtos { get; set; }

    }
}
