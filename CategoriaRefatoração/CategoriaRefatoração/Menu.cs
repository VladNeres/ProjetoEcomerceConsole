using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoriaRefatoracao
{
    public class Menu
    {
        List<Categoria> listaDeCategoria = new List<Categoria>();


        public void ExbirMenu()
        {

            bool loopMenu = true;
            while (loopMenu)
            {
                Console.WriteLine("===========================\n" +
                                  "|                         |\n" +
                                  "|   Olá Seja Bem Vindo    |\n" +
                                  "===========================\n" +
                                  "| 0- Sair                  |\n" +
                                  "| 1- Cadastrar Categoria  |\n" +
                                  "| 2- Editar    Categoria  |\n" +
                                  "| 3- Pesquisar Categoria  |\n" +
                                  "| 4- Lista     completa  |\n");

                Console.Write(" Escolha a opção desejada => ");
                string opcaoMenu = Console.ReadLine();
                switch (opcaoMenu)
                {
                    case "1":
                        Console.WriteLine("= Cadastrar Categoria =");
                        CadastrarCategoria();
                        break;

                    case "2":
                        Console.WriteLine("= Editar Categoria =");
                        EditarCategoria();
                        break;

                    case "3":
                        Console.WriteLine("== Pesquisar na Lista de categoria ==");
                        PesquisarNaLista();
                        break;

                    case "4":
                        Console.WriteLine("= Menu =");
                        MostarLista();
                        break;

                    case "0":
                        Console.WriteLine("====================================");
                        Console.WriteLine("| Obrigado por acessar nosso sitema |");
                        Console.WriteLine("====================================");
                        loopMenu = false;
                        break;

                    default:
                        {
                            Console.WriteLine("A opção não é valida por favor digite uma opcao valida");
                            break;
                        }
                }
            }
        }

        public void CadastrarCategoria()
        {
            string nome = Console.ReadLine();
            Categoria categoria = new Categoria();
            categoria.Cadastrar(nome);
            listaDeCategoria.Add(categoria);

            if (categoria.VerificarLetras(nome))
            {
                foreach (Categoria item in listaDeCategoria)
                {
                    categoria._id++;
                }
                Console.WriteLine($"O id é : ================= {categoria._id}");
                Console.WriteLine($"O nome da Categoria é:==== {categoria._nome}");
                Console.WriteLine($"Criada em :=============== {categoria._dataCriacao}");
                Console.WriteLine($"O status está:============ {categoria._status} \n");
                Console.WriteLine("Aperte enter para retornar ao menu");
                LimpaTela();
            }
        }

        public void EditarCategoria()
        {
            MostarLista();

            Console.WriteLine("Qual o Id da categoria deseja alterar ?");
            int numeroId = Convert.ToInt32(Console.ReadLine());
            var categoria = listaDeCategoria.Where(item => item._id.Equals(numeroId));

            Console.WriteLine("digite o novo nome da categoria");
            string novoNome = Console.ReadLine();
            if (categoria.Count() == 0)
            {
                Console.WriteLine("A categoria não está cadastrada");
            }
            else
            {
                foreach (var item in categoria)
                {
                    item.Editar(novoNome);
                }
            }
            LimpaTela();
        }

        public void PesquisarNaLista()
        {
            Console.WriteLine("Digite o nome da categoria que está procurando");
            string nomeNaLista = Console.ReadLine();
            int numeroDeLetras = nomeNaLista.Length;

            var pesquisarNaLista = listaDeCategoria.FindAll(item => item._nome.ToUpper().
                Substring(0, numeroDeLetras).Equals(nomeNaLista.ToUpper()));

            if (pesquisarNaLista.Count() == 0)
            {
                Console.WriteLine("A Categoria pesquisada é nula ou não existe");
            }
            else
            {

                foreach (Categoria item in pesquisarNaLista)
                {

                    Console.WriteLine($"O id é : ================= {item._id}");
                    Console.WriteLine($"O nome da Categoria é:==== {item._nome}");
                    Console.WriteLine($"Criada em :=============== {item._dataCriacao}");
                    Console.WriteLine($"O status está:============ {item._status} ");
                    if (item._dateAtualizacao.HasValue)
                    {
                        Console.WriteLine($"Data da ultima atualização {item._dateAtualizacao}\n");
                    }
                    LimpaTela();
                }
            }
        }

        public void MostarLista()
        {

            foreach (Categoria item in listaDeCategoria)
            {

                Console.WriteLine($"O id é : ================= {item._id}");
                Console.WriteLine($"O nome da Categoria é:==== {item._nome}");
                Console.WriteLine($"Criada em :=============== {item._dataCriacao}");
                Console.WriteLine($"O status está:============ {item._status} ");
                if (item._dateAtualizacao.HasValue)
                {
                    Console.WriteLine($"Data da ultima atualização {item._dateAtualizacao}\n");
                }
                Console.WriteLine();
            }
        }

        public void LimpaTela()
        {
            Console.WriteLine();
            Console.ReadKey();
            Console.Clear();
        }
    }
}


