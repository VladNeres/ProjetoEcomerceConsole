﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Categorias
{
    internal class Menu : Categoria
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
                                                            "0- Para sair");


                
                    

                    string opcaoMenu = Console.ReadLine();
                    switch (opcaoMenu)
                    {
                        case "1":
                        
                        bool controleProsseguirEVoltar = true;
                        while(controleProsseguirEVoltar)
                        {
                        Console.WriteLine("Deseja Cadastrar a categoria digite (S) para prosseguir ou (N) para para retornar ao menu ");
                            string opcaoSimOuNao = Console.ReadLine();
                            switch (opcaoSimOuNao)
                            {
                                case "S":
                                    Console.WriteLine(categoria.CadastrarCategoria());
                                    controleProsseguirEVoltar=false;
                                    break;

                                case "N":
                                    controleProsseguirEVoltar = false;

                                    break;


                                default:
                                    Console.WriteLine("escolha uma opçao valida");
                                    break;
                            }
                        }
                        break;



                    case "2":

                        if (string.IsNullOrEmpty(categoria.Nome))
                        {
                            Console.WriteLine("Nenhuma Categoria cadastrada ainda, por favor Cadastre primeiro uma categoria");
                            break;
                        }   
                        else
                        {
                            Console.WriteLine(categoria.EditarCategoria());
                            break;
                        }

                    case "3":
                        if (string.IsNullOrEmpty(categoria.Nome))
                        {
                            Console.WriteLine("Para cadastrar uma subcategoria é necessario ter uma categoria cadastrada.\n" +
                                              "por favor primeiro cadastre uma categoria!\n");
                            break;
                        }
                        else
                        {
                            Console.WriteLine(subCategoria.CadastrarCategoria());
                            break;
                        }

                    case "4":

                        if (string.IsNullOrEmpty(subCategoria.Nome))
                        {
                            Console.WriteLine("Não há subcategoria cadastrada, por favor primeiro cadastre uma categoria!\n ");
                            break;
                        }
                        else
                        {
                            Console.WriteLine(subCategoria.EditarCategoria());
                            break;

                        }

                    case "0":
                            Console.WriteLine("Obrigado por utilizar nosso Sistema");
                            loopMenu = false;
                        break;

                    default:
                            Console.WriteLine("Opção invalida, digite uma das opções disponíveis");
                            break;
                    }

                

            }
        }

    }
}