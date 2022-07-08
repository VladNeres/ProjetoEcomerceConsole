using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastrarCategorias
{
    public  class  MenuNavegar
    {
        
        public static void ExibirMenu()
        {
            Categoria categoria = new Categoria();
           Categoria subCategoria = new SubCategoria();


            var opcaoValida = true;

            while (opcaoValida)
            {
                Console.WriteLine("Seja Bem-vindo ao sistema");
                Console.WriteLine("1- Cadastrar Categoria\n" +
                                   "2- Editar categoria\n" +
                                   "3- Cadastrar sub-categoria\n" +
                                   "4- Editar sub-categoria\n" +
                                   "0- sair");
                string numeroMenu = Console.ReadLine();
                switch (numeroMenu)
                {
                    case "1":
                        
                        Console.WriteLine(categoria.CadastrarCategoria()); 
                            break;
                    case "2":
                        
                        Console.WriteLine(categoria.EditarCategoria());
                        break;

                    case "3":
                            
                        Console.WriteLine(subCategoria.CadastrarCategoria());
                        break;

                        case "4": Console.WriteLine(subCategoria.EditarCategoria());
                        break;

                    case "0":
                        
                        opcaoValida = false;
                        break;

                    default:
                        Console.WriteLine("digite uma opção valida\n");
                        break;
                
                }

            }

        }
    }
}
