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
                    Nome = nomeCategoria;

                    Console.WriteLine("O nome da categoria é : " + Nome);
                    Console.WriteLine("Criada em :  " + (Data_hora = DateTime.Now));
                    Console.WriteLine("Status : " + (Status = " Ativo"));
                    loop = false;
                }
                else
                {
                    Console.WriteLine("A categoria deve conter de 1 a 128 carcteres (apenas letras)");
                }
               
            }
            return "Categoria criada com sucesso\n";
        }

        public bool VerificarLetras(string nome)
        {

            int regex = Regex.Matches(nome, @"[a-zA-Zá-úÁ-Ú' ']").Count;
                if (!String.IsNullOrWhiteSpace(nome) && nome.Length >0 && nome.Length <= 18 && regex == nome.Length ) 
                {
                    return true;
                }   
                return false;
        }
         public virtual string EditarCategoria()
         {
            Console.WriteLine("Escreva o novo nome da categoria");
            string novonome = Console.ReadLine();
            bool loopEditar = true;
            while (loopEditar)
            {
                if (VerificarLetras(novonome))
                {
                    Console.WriteLine("A categoria : " + Nome + " foi alterada na data: " + Data_hora + " para : " + (Nome = novonome));
                    Console.WriteLine("Data de alteração : " + (Data_hora = DateTime.Now));
                    Console.WriteLine("O Status da Categoria está : " + Status);
                    loopEditar = false;
                }
            }            
            return "Categoria atualizada com sucesso \n";


         }
    }

}

