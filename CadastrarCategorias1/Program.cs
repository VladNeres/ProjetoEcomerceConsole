using System;

namespace CadastrarCategorias
{
    class Program
    {
        static void Main(String[] args)
        {
            //string nome = Console.ReadLine();
            //Categoria categoria = new Categoria(nome, false) ;
            MenuNavegar.ExibirMenu();

            Console.WriteLine("total de Categorias criadas:" + Categoria.TotalDeCategorias);
        }
    }
}