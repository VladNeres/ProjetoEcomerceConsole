using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Data.Dto.CentroDto
{
    public class UpdateCentroDto
    {
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú0-9' '\s]{1,100}", ErrorMessage = "O campo nome deve conter apenas letras")]
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [StringLength(128, ErrorMessage = "Tamanho máximo de 128 caracteres excedido")]
        public string Nome { get; set; }

        [RegularExpression(@"[a-zA-Zá-úÁ-Ú0-9' '\s]{1,1000}", ErrorMessage = "O campo logradouro não deve conter caracteres  alfanuméricos")]
        [StringLength(258, ErrorMessage = "Tamanho máximo de 258 caracteres excedido")]
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo número é obrigatório")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "O campo complemento é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú0-9' '\s]{1,1000}", ErrorMessage = "O campo complemento deve conter apenas caracteres alfanuméricos")]
        [StringLength(128, ErrorMessage = "Voce excedeu o limide te 128 caracteres")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "O campo bairro  é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú0-9' '\s]{1,1000}", ErrorMessage = "O campo bairro deve conter apenas caracteres alfanuméricos")]
        [StringLength(128, ErrorMessage = "Voce excedeu o limide te 128 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "O campo cidade é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú0-9' '\s]{1,1000}", ErrorMessage = "O campo cidade deve conter apenas caracteres alfanuméricos")]
        [StringLength(128, ErrorMessage = "Voce excedeu o limide te 128 caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo UF é obrigatório")]
        [StringLength(2, ErrorMessage = "voce excedeu o limite maximo de 2 caracterees")]
        [RegularExpression(@"[a-zA-Z' '\s]{1,1000}", ErrorMessage = "O campo UF deve conter apenas letras (sem acentos)")]

        public string UF { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório")]
        public string CEP { get; set; }

        public bool Status { get; set; }
    }
}
