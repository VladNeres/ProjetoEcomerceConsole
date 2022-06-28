using System;

namespace CadastrarCategorias1
{
    class program
    {
        static void Main(String[] args)
        {
            Console.WriteLine("Ola Seja Bem vindao ao sisteme.\nDigide o nome da Categoria");

            string nome = Console.ReadLine();
            Categorias categoria = new Categorias(nome);
         
           
        } 
    }
}