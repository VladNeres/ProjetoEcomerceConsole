using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Categorias
{
    public class Menu
    {
        public static void MenuNavegar()
        {
            
            Categoria categoria = new Categoria();
            SubCategoria subCategoria = new SubCategoria();
           
            bool loopMenu = true;
            while (loopMenu)
            {
             

                Console.WriteLine("Digite a opção desejada: \n" +
                                                            "1- Cadastrar categoria \n" +
                                                            "2- Editar categoria \n" +
                                                            "3- Cadastrar subcategoria \n" +
                                                            "4- Editar subCategoria \n" +
                                                            "5- Mostrar Lista De Categoria\n"+ 
                                                            "0- Para sair");





                string opcaoMenu = Console.ReadLine();
                switch (opcaoMenu)
                {
                    case "1":

                        bool controleProsseguirEVoltar = true;
                        while (controleProsseguirEVoltar)
                        {
                            Console.WriteLine("Deseja Cadastrar a categoria digite (S) para prosseguir ou (N) para para retornar ao menu ");
                            string opcaoSimOuNao = Console.ReadLine();
                            switch (opcaoSimOuNao.ToUpper())
                            {
                                case "S":
                                    
                                    Console.WriteLine(categoria.Cadastrar());
                                    controleProsseguirEVoltar = false;
                                    
                                    break;

                                case "N":
                                    Console.Clear();
                                    controleProsseguirEVoltar = false;

                                    break;


                                default:
                                    Console.WriteLine("escolha uma opçao valida");
                                    break;
                            }
                        }
                        break;



                    case "2":

                        if (string.IsNullOrWhiteSpace(categoria.Nome))
                        {
                            Console.WriteLine("Nenhuma Categoria cadastrada ainda, por favor Cadastre primeiro uma categoria");
                            break;
                        }
                        else
                        {
                            Console.WriteLine(categoria.Editar());
                            break;
                        }

                    case "3":
                        if (string.IsNullOrWhiteSpace(categoria.Nome))
                        {
                            Console.WriteLine("Para cadastrar uma subcategoria é necessario ter uma categoria cadastrada.\n" +
                                              "por favor primeiro cadastre uma categoria!\n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine(subCategoria.Cadastrar());
                            break;
                        }

                    case "4":

                        if (string.IsNullOrWhiteSpace(subCategoria.Nome))
                        {
                            Console.WriteLine("Não há subcategoria cadastrada, por favor primeiro cadastre uma categoria!\n ");
                            break;
                        }
                        else
                        {
                            Console.WriteLine(subCategoria.Editar); 
                            break;

                        }

                    case "5":
                        Console.WriteLine(categoria.MostrarResultado());
                        break;

                    case "0":
                        Console.WriteLine("Obrigado por utilizar nosso Sistema");
                        loopMenu = false;
                        break;

                    default:
                        Console.WriteLine("(ATENÇÂO!)  Opção invalida, digite uma das opções disponíveis\n");
                        break;
                }



            }
        }

    }
} 

        
