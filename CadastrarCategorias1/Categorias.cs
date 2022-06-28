using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CadastrarCategorias1 
{ 


    public class Categorias
    {

        public string nome;
        public string status;
        public string data_Hora;
        
        //creating constructor just allown create a new object if the person type the name of category
        public  Categorias(string nomeCategoria)
        {
            nome = nomeCategoria;
            Verificar_Letras();
            Status();
        }

        // verification limite  and alphabet
        public void Verificar_Letras()
        {
            while (!Regex.IsMatch(nome, @"^[ a-zA-Z á]*$") || nome.Length < 1 || nome.Length > 10)
            {

                if (!Regex.IsMatch(nome, @"^[ a-zA-Z á]*$"))
                {
                    Console.WriteLine("por favor digite apenas letras no nome da categoria");
                    Console.ReadLine();
                }
                else if (nome.Length < 1 || nome.Length > 5)
                {
                    Console.WriteLine("tamanho insuficiente ou maior do que o esperado\n digite de 1 ate 125 caraacteres");
                    nome = Console.ReadLine();
                }                           
            } 
            Data_hora();
        }
        public void Data_hora()
        {
            data_Hora = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            Console.WriteLine(" A Categoria: " + nome + ",  foi criado :" + data_Hora);
        }

        public bool Status()
        {
            Console.WriteLine("Status: ativo");
            return true;
        }
    }
}   