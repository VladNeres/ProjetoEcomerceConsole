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
        public string Nome { get; protected set; }
        public string Status { get; protected set; }
        public int ID { get; protected set; }
        public DateTime Data_hora { get; protected set; }
        public DateTime DataAtualizada { get; protected set; }


        public Categoria(string nome)
        {
            Nome = nome;
            Status = "Ativo";
            Data_hora = DateTime.Now;
            DataAtualizada = DateTime.Now;

        }

        public Categoria()
        {
            Nome = " ";
            Status = "Ativo";
            ID = 0;
            Data_hora = DateTime.Now;
            DataAtualizada = DateTime.Now;
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


                Console.WriteLine("Digite o nome da Categoria: ");
                string nomeCategoria = Console.ReadLine();

                if (VerificarLetras(nomeCategoria))
                {
                    Categoria categoria = new Categoria();
                     categoria.Nome = nomeCategoria;
                    Nome = nomeCategoria;
                    categoria.ID = listaDeCategoria.Count;
                    categoria.Data_hora = DateTime.Now;


                    Console.WriteLine($"O ID da categoria é :  ({categoria.ID})");
                    Console.WriteLine($"O nome da categoria é : {categoria.Nome}");
                    Console.WriteLine($"Criada em : ({categoria.Data_hora})");
                    Console.WriteLine($"Status : {categoria.Status} \n ");
                    listaDeCategoria.Add(categoria);



                    loop = false;
                }
                else
                {
                    Console.WriteLine("A categoria deve conter de 1 a 50 carcteres (apenas letras)\n");
                }
                Console.WriteLine("Aperte enter para voltar ao menu");
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
                Console.WriteLine("Qual a categoria que voce deseja alterar ?");

                
                string editarNomedaCategoriaEscolida = Console.ReadLine();
                var procurarListaPorNome = listaDeCategoria.Where(categoria => categoria.Nome.ToUpper().Equals(editarNomedaCategoriaEscolida.ToUpper()));

               
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
                                            item.Status = "Inativo";
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
                                
                                Console.WriteLine($"Foi alterada para : ( {item.Nome}) na Data :  ({(item.DataAtualizada)})");
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

                

            }
            return "lista Completa!";
        }


        public virtual string RemoverDaLista()
        {
            MostrarLista();

            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Digite o Id que deseja remover");
                string remover = Console.ReadLine();
                int numeroId = Convert.ToInt32((remover));
                var removerPorID = listaDeCategoria.Where(categoria => categoria.ID.Equals(numeroId));
                if (removerPorID.Count() == 0)
                {
                    Console.WriteLine("Esse id não foi cadastrado na lista");
                }
                else
                {
                    for (int i = 0; i < removerPorID.Count(); i++)
                    {
                        if(removerPorID.All(categoria => categoria.ID.Equals(numeroId)))
                        {
                            //listaDeCategoria.Remove();
                        }
                               
                    }
                }
            }
                     
            return "";
           
        } 

        public string Pesquisar()
        {
            bool loopPesquisar = true;
            while (loopPesquisar)
            {
                Console.WriteLine("Por qual o meio que voce deseja pesquistar (ID, Nome ou Status)");
                string escolhaLoop = Console.ReadLine();
                switch (escolhaLoop.ToUpper())
                {
                    case "ID":

                        Console.WriteLine("Digite o ID");
                        int procurarPorID = Convert.ToInt32(Console.ReadLine());
                        var mostar = listaDeCategoria.FindAll(item => item.ID.Equals(procurarPorID));

                        if (mostar.Count() == 0)
                        {
                            Console.WriteLine("Categotia não encontrada");
                        }
                        else
                        {
                            foreach (var categoria in mostar)
                            {
                                Console.WriteLine($"O ID {categoria.ID}");
                                Console.WriteLine($"Nome da categoria {categoria.Nome}");
                                Console.WriteLine($"Status da categoria {categoria.Status}");
                                Console.WriteLine(categoria.Data_hora);
                                loopPesquisar = false;
                            }
                        }
                        break;

                    case "STATUS":

                        bool loop = true;
                        while (loop)
                        {
                            Console.WriteLine("Digite por qual status voce deseja pesquisar");
                            string escolha = Console.ReadLine();
                            switch (escolha.ToUpper())
                            {

                                case "ATIVO":
                                    var procurarStatusAtivo = listaDeCategoria.FindAll(item => item.Status.ToUpper().Equals(escolha.ToUpper()));
                                    foreach (var categoria in procurarStatusAtivo)
                                    {
                                        Console.WriteLine($"O ID {categoria.ID}");
                                        Console.WriteLine($"Nome da categoria {categoria.Nome}");
                                        Console.WriteLine($"Status da categoria {categoria.Status}");
                                        Console.WriteLine(categoria.Data_hora);
                                        loop = false;
                                        loopPesquisar = false;
                                    }
                                    break;

                                case "INATIVO":
                                    var procurarStatusInativo = listaDeCategoria.FindAll(item => item.Status.ToUpper().Equals(escolha.ToUpper()));

                                    foreach (var categoria in procurarStatusInativo)
                                    {
                                        Console.WriteLine($"O ID {categoria.ID}");
                                        Console.WriteLine($"Nome da categoria {categoria.Nome}");
                                        Console.WriteLine($"Status da categoria {categoria.Status}");
                                        Console.WriteLine(categoria.Data_hora);
                                        loop = false;
                                        loopPesquisar = false;

                                    }
                                    break;


                                default:
                                    Console.WriteLine("Digite uma opção valida");
                                    break;
                            }
                        }

                        break;

                    case "NOME":

                        bool loopNome = true;
                        while (loopNome)
                        {
                            Console.WriteLine("Digite o nome que deseja verificar");
                            string nomePesquisar = Console.ReadLine();
                            int contaNome = nomePesquisar.Length;

                            var encontrarPorNome= listaDeCategoria.FindAll(item => item.Nome.ToUpper().Substring(0,contaNome).Equals(nomePesquisar.ToUpper()));

                            if (encontrarPorNome.Count()==0)
                            {
                                Console.WriteLine("Categoria não encontrada");
                            }
                            else
                            {
                                foreach (Categoria item in encontrarPorNome)
                                {
                                    Console.WriteLine($"O ID {item.ID}");
                                    Console.WriteLine($"Nome da categoria {item.Nome}");
                                    Console.WriteLine($"Status da categoria {item.Status}");
                                    Console.WriteLine(item.Data_hora);
                                    loop = false;
                                }
                            } 
                            loopNome = false;
                            loopPesquisar = false; 
                        }
                        break;


                    default:
                        Console.WriteLine("Escolha uma opção valida (NOME,STAUS ou ID)");
                        break;

                }
            }
            
            return "";
        }
    }

}

