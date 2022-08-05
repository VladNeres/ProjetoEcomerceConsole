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
        List<Categoria> listaDeCategoria = new List<Categoria>();
        public string Nome { get; set; }
        public string Status { get; protected set; }
        public int ID { get; protected set; }
        public DateTime Data_hora { get; protected set; }
        public DateTime DataAtualizada { get; protected set; }


        public Categoria(string nome)
        {
            Nome = nome;
            Status = "Ativo";


        }

        public Categoria()
        {
            Nome = " ";
            Status = "Ativo";
            ID = 0;
        }

        public bool VerificarLetras(string nome)
        {

            int regex = Regex.Matches(nome, @"[a-zA-Zá-úÁ-Ú' ']").Count;
            if (!String.IsNullOrWhiteSpace(nome) && nome.Length > 0 && nome.Length <= 50 && regex == nome.Length)
            {
                return true;
            }
            return false;
        }


        public virtual string Cadastrar()
        {


            bool loop = true;
            while (loop)
            {
                Categoria categoria = new Categoria();

                Console.WriteLine("Digite o nome da Categoria: ");
                string nomeCategoria = Console.ReadLine();

                categoria.Nome = nomeCategoria;
                Nome = nomeCategoria;
                listaDeCategoria.Add(categoria);
                categoria.ID = listaDeCategoria.Count;
                categoria.Data_hora = DateTime.Now;



                if (VerificarLetras(nomeCategoria))
                {


                    Console.WriteLine($"O ID da categoria é :  ({categoria.ID})");
                    Console.WriteLine($"O nome da categoria é : {categoria.Nome}");
                    Console.WriteLine($"Criada em : ({categoria.Data_hora})");
                    Console.WriteLine($"Status : {categoria.Status} \n ");


                    loop = false;
                }
                else
                {
                    Console.WriteLine("A categoria deve conter de 1 a 50 carcteres (apenas letras)\n");
                }
                Console.WriteLine("aperte enter para voltar ao menu");
                Console.ReadLine();
                Console.Clear();
            }

            return " ";

        }


        public virtual string Editar()
        {
            //Showing complet least to consult
            MostrarLista();

            //creating infinit loop to repeat while the function be true
            bool loopEditar = true;
            while (loopEditar)
            {

                //surch a "categoria" throwght teh name
                Console.WriteLine("qual a categoria que voce deseja alterar ?");

                string editarNomedaCategoriaEscolida = Console.ReadLine();
                var procurarListaPorNome = listaDeCategoria.Where(categoria => categoria.Nome.ToUpper().Equals(editarNomedaCategoriaEscolida.ToUpper()));
                

                if (VerificarLetras(editarNomedaCategoriaEscolida))
                {
                    if (procurarListaPorNome.Count() == 0)
                    {
                        Console.WriteLine("Categoria não encontrada");
                    }
                    else
                    {

                        Console.WriteLine("Escreva o novo nome da categoria");
                        string novonome = Console.ReadLine();
                        if (VerificarLetras(novonome))
                        {
                            //for each item inside the list will giv an choice option for Status and add the vallues with modification 
                            foreach (Categoria item in procurarListaPorNome)
                            {

                                do
                                {//creating  an option of choice option to Status
                                    Console.WriteLine("Deseja que a categoria fique ativa ao ser atualizada?\n" +
                                        " Digite (S) para Sim ou (N) para Não");
                                    string opçãoDoStatus = Console.ReadLine();
                                    switch (opçãoDoStatus.ToUpper())
                                    {
                                        case "S":

                                            item.Status = "Ativo";
                                            break;


                                        case "N":
                                            item.Status = "inativo";
                                            break;

                                        default:
                                            Console.WriteLine("Digite uma opção valida!");
                                            break;

                                    }
                                }
                                while (string.IsNullOrEmpty(item.Status));

                                Console.WriteLine($"Acategoria ({item.Nome}) criada na Data :  ({item.Data_hora})");
                                item.Nome = novonome;
                                Nome = novonome;
                                DataAtualizada = DateTime.Now;
                                Console.WriteLine($"Foi alterada para : ( {item.Nome}) na Data :  ({(item.DataAtualizada = DateTime.Now)})");
                                Console.WriteLine("O Status da Categoria está : " + item.Status);
                            }
                            loopEditar = false;
                        }
                        else
                        {
                            Console.WriteLine("O novo nome da Categoria  deve conter entre 1 e 50 caracteres (apenas letras)\n");
                        }
                    }
                }
                else
                {

                }
                //creating a verification to see if there is an iten inside the list
               
            }
            return "Categoria atualizada com sucesso \n";
        }

        public virtual string MostrarLista()
        {
            foreach (Categoria item in listaDeCategoria)
            {
                Console.WriteLine($"O Id : {(item.ID)}");
                Console.WriteLine($"O nome da categoria é : {item.Nome}");
                Console.WriteLine($"Criada em :   {item.Data_hora}");
                Console.WriteLine($"Status :  {item.Status}");
                Console.WriteLine($"Data da Ultima auteração {item.DataAtualizada}");
                Console.WriteLine();

            }
            return "lista Completa!";
        }
    }

}

