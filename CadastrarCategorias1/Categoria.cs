using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CadastrarCategorias
{
    public   class Categoria
    {

        public string Nome { get; private set; }
        public string Status { get; protected set; }
        public DateTime data_Hora { get; protected set; }




        public Categoria() 
        {
            TotalDeCategorias++;
        }
        public Categoria(string nome)
        {
            Nome = nome;
            Status = "Categoria ativa";
        }
        

        public virtual string CadastrarCategoria()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("digite o nome da categoria");
                String nomeCategoria = Console.ReadLine();

                //if (string.IsNullOrEmpty(nomeCategoria))
                //{
                //    throw new ArgumentException("O nome da Categoria não pode ser nula ou vazia");
                //}
                if (VerificarLetras(nomeCategoria))
                {
                    Nome = nomeCategoria;
                    Console.WriteLine("Categoria cadastrada: " + nomeCategoria);
                    Console.WriteLine("Status: " + (this.Status= "ativo"));
                    Console.WriteLine("A categoria " + nomeCategoria + " foi criada na data: " + (data_Hora = DateTime.Now));                   
                    loop = false;
                }
                else
                {
                    Console.WriteLine("Digite endere 1 e 128 caracteres");

                }
              
            }
            return "Categoria Criada com sucesso\n";

        }

        // verification about limite  and alphabet
        public bool VerificarLetras(string nome)
        {

            int regex = Regex.Matches(nome, @"[a-zA-Zà-úÀ-Ú' ']").Count;
            if (nome.Length <= 128 && nome.Length > 0 && regex == nome.Length)
            {
                return true;
            }
            return false;
        }
            
            public virtual string EditarCategoria()
            {
                Console.WriteLine("O nome da Categoria atual é : " + Nome);

                bool loopEditar = true;
                while (loopEditar)
                {
                Console.WriteLine("Digite o novo nome da categoria entre 1 e 128 caracteres (apenas letras)");
                string alterarNome = Console.ReadLine();
                    
                    if (VerificarLetras(alterarNome))
                    {
                            Nome = alterarNome;
                            loopEditar = false;
                    }

                }
             return ("O nome da categoria foi alterado na data : " + data_Hora + "\n"+
                                                           "para : "  + Nome + "\n"+
                                                           "Status : " + Status + "\n");

            }


        public static int TotalDeCategorias { get; private set; }
    }

}