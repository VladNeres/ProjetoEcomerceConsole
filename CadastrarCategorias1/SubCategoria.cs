using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastrarCategorias
{ 
    public class SubCategoria: Categoria
    {
        public string NomeSubCategoria { get; private set; }
       

        public override string CadastrarCategoria()
        {
            bool loop = true;

            while (loop)
            {
                Console.WriteLine("digite o nome da sub-categoria entre 1 e 128 caracteres (apenas letras)");
                string subCategoria = Console.ReadLine();
                if (VerificarLetras(subCategoria))
                {
                    NomeSubCategoria = subCategoria;
                    Console.WriteLine("O nome da sub-Categoria: " + NomeSubCategoria);
                    Console.WriteLine("O Status da sub-Categoria está  : " + (Status= "ativo"));
                    Console.WriteLine("A sub-Categoria foi criada : " + (data_Hora = DateTime.Now));
                    loop = false;
                }
                
            }
            return "sub-categoria cadastrada com sucesso\n";
        }
        
        public override string EditarCategoria()
        {
            Console.WriteLine("O nome da sub-categoria atual é : " + NomeSubCategoria+ "\n");
            bool loopEditar = true;
            while (loopEditar)
            {
                Console.WriteLine("Digite o novo nome da sub-categoria");
                string novaSubCategoria = Console.ReadLine();
                if (VerificarLetras(novaSubCategoria))
                {
                    Console.WriteLine("A categoria " + NomeSubCategoria + " foi alterada na data: " + (data_Hora = DateTime.Now));
                    NomeSubCategoria = novaSubCategoria;
                    loopEditar = false;
                }
                  
            }
            return ("O nome da sub-categoria foi atualizado para: " + NomeSubCategoria + "\n" +
                                                                      "Status :"+Status + "\n") ;


        }
    }
}
