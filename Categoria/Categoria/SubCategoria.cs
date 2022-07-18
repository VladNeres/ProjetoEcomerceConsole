using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Categorias
{
    public class SubCategoria : Categoria
    {
        

        public SubCategoria()
        {

        }

        

        public override string CadastrarCategoria()
        {
            bool loopSubCategoria = true;
            while (loopSubCategoria)
            {
                Console.WriteLine("Digite o nome da sub-categoria: ");
                string nomeSubCategoria = Console.ReadLine();

                if (VerificarLetras(nomeSubCategoria))
                {
                    Nome = nomeSubCategoria;
                    Console.WriteLine("O nome da categoria é :" + Nome);
                    Console.WriteLine("O Status está: " + (Status = " Ativo"));
                    Console.WriteLine("Criado em : " + (Data_hora= DateTime.Now));
                    loopSubCategoria = false;
                }
                else
                {
                    Console.WriteLine("A categoria deve conter entre 1 e  50 caracteres (apenas letras)");
                }
            }
            return "Sub-categoria criada com sucesso\n";

        }

            public override string EditarCategoria()
            {
                bool loopEditarSubCateforia = true;
                while (loopEditarSubCateforia)
                {
                    Console.WriteLine("Digite o novo nome da subCategoria");
                    string novoNomeSubCategoria= Console.ReadLine();
                    if (VerificarLetras(novoNomeSubCategoria))
                    {
                        Console.WriteLine("A subCategoria " + Nome+  " Criada em :" + Data_hora + "\n"+
                                          "Foi atualizada para : " + (Nome=novoNomeSubCategoria) +"\n"+
                                          "Na data: "+ (Data_hora= DateTime.Now)+ "\n"+
                                          "O status da subcategoria é : " + (Status= "ativo"));
                        loopEditarSubCateforia=false;

                    }
                }
                return "Subcategoria Atualizada com sucesso!\n";
            }

    }

}
 