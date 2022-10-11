using CategoriaApi.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CategoriaApi.Data.Dto.DtoProduto
{
    public class ReadProdutoDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualiazacao { get; set; }
        
       
    }
}
