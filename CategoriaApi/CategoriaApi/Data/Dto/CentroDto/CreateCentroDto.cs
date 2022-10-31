using System.ComponentModel.DataAnnotations;

namespace CategoriaApi.Data.Dto.CentroDto
{
    public class CreateCentroDto
    {

        [Required(ErrorMessage = "O campo nomme é obrigatório")]
        [RegularExpression(@"[a-zA-Zá-úÁ-Ú0-9' ']{1,10000}", ErrorMessage = "O campo não permite caracteres especiais")]
        [StringLength(128, ErrorMessage = "Tamanho máximo de 128 caracteres excedido ")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório")]
       // [RegularExpression(@"[[0-9]{1,5}-[0-9]{0,3}]", ErrorMessage = "formato de CEP invalido, o CEP deve ser escrito no seguinte formato 00000-000 ")]
        [StringLength(9, ErrorMessage = "Tamanho máximo de 8 caracteres excedido")]
        public string CEP { get; set; }

        
        [Required(ErrorMessage = "O campo número é obrigatório")]
        public int Numero { get; set; }


    }
}
