using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Categorias
{
    public class Categoria
    {
        public string Nome { get; protected set; }
        public string Status { get; protected set; }  
        public DateTime Data_hora { get; protected set; } 


        public Categoria()
        {

        }


        public virtual string CadastrarCategoria()
        {
            bool loop = true;
            while (loop)
            {

                Console.WriteLine("Digite o nome da Categoria: ");
                string nomeCategoria = Console.ReadLine();
                

                if (VerificarLetras(nomeCategoria))
                {

                    Console.WriteLine("deseja que a categoria seja ativa quando for criada ? \n" +
                        "(S) para Sim ou (N) para Não");
                    do
                    {
                        string statusAtivoInativo = Console.ReadLine();
                        switch (statusAtivoInativo)
                        {
                            case "S":
                                Status = "ativo";
                                break;
                            case "N":
                                Status = "inativo";
                                break;

                            default:
                                Console.WriteLine(" Escolha uma opcao valida");
                                break;

                        }
                    }
                    while (String.IsNullOrEmpty(Status));
                    
                    Nome = nomeCategoria;

                    Console.WriteLine("O nome da categoria é : " + Nome);
                    Console.WriteLine("Criada em :  " + (Data_hora = DateTime.Now));
                    Console.WriteLine("Status : " + Status );
                    loop = false;
                }
                else
                {
                    Console.WriteLine("A categoria deve conter de 1 a 128 carcteres (apenas letras)\n");
                }

            }
            return "Categoria criada com sucesso\n";
        }

        public bool VerificarLetras(string nome)
        {

            int regex = Regex.Matches(nome, @"[a-zA-Zá-úÁ-Ú' ']").Count;
                if (!String.IsNullOrWhiteSpace(nome) && nome.Length >0 && nome.Length <= 50 && regex == nome.Length ) 
                {
                    return true;
                }   
                return false;
        }
         public virtual string EditarCategoria()
         {
           
            bool loopEditar = true;
            while (loopEditar)
            {
                Console.WriteLine("Escreva o novo nome da categoria");
                string novonome = Console.ReadLine();
                if (VerificarLetras(novonome))
                {
                    
                    Console.WriteLine("A categoria : (" + Nome + " ) criada  na data: " + Data_hora );
                    Console.WriteLine("Foi alterada para : (" + (Nome = novonome) + " ) na Data : " + (Data_hora = DateTime.Now));
                    Console.WriteLine("O Status da Categoria está : " + Status);
                    loopEditar = false;
                }
                else
                {
                    Console.WriteLine("O novo nome da Categoria  deve conter entre 1 e 50 caracteres (apenas letras)\n");
                }
            }            
            return "Categoria atualizada com sucesso \n";


         }
    }

}

