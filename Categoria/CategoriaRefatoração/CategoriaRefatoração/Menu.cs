using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoriaRefatoracao
{
    public class Menu
    {
        List<Categoria> listaDeCategoria  = new List<Categoria>();


        public void ExbirMenu()
        {

            bool loopMenu = true;
            while (loopMenu)
            {
                Console.WriteLine("===========================\n" +
                                  "|                         |\n" +
                                  "|   Olá Seja Bem Vindo    |\n" +
                                  "===========================\n" +
                                  "| 0- Sair                 |\n" +
                                  "| 1- Cadastrar Categoria  |\n" +
                                  "| 2- Cadastrar subCateg.  |\n" +
                                  "| 3- Editar    Categoria  |\n" +
                                  "| 4- Pesquisar na Lista   |\n" +
                                  "| 5- Lista     completa  |\n");

                Console.Write(" Escolha a opção desejada => ");
                string opcaoMenu = Console.ReadLine();
                switch (opcaoMenu)
                {
                    case "1":
                        Console.WriteLine("= Cadastrar Categoria =");
                        CadastrarCategoria();
                        LimpaTela();
                        break;

                    case "2":
                        Console.WriteLine("= Cadastrar SubCategoria");
                        CadastrarSub();
                        LimpaTela();
                        break ;
                    case "3":
                        Console.WriteLine("= Editar Categoria =");
                        Editar();
                        LimpaTela();
                        break;

                    case "4":
                        Console.WriteLine("== Pesquisar na Lista de categoria ==");
                        PesquisarNaLista();
                        LimpaTela();
                        break;

                    case "5":
                        Console.WriteLine("= Lista Completa =");
                        MostarLista();
                        LimpaTela();
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
            Categoria categoria = new Categoria();
            string nome = Console.ReadLine();
            categoria.Cadastrar(nome);

            if (categoria.VerificarLetras(nome))
            {
                listaDeCategoria.Add(categoria);
                foreach (Categoria item in listaDeCategoria)
                {
                    categoria.Id++;
                }
                Console.WriteLine($"O id é : ================= {categoria.Id}");
                Console.WriteLine($"O nome da Categoria é:==== {categoria.Nome}");
                Console.WriteLine($"Criada em :=============== {categoria.DataCriacao}");
                Console.WriteLine($"O status está:============ {categoria.Status} \n");
                Console.WriteLine("Aperte enter para retornar ao menu");
            }
        }

        public void CadastrarSub()
        {
            SubCategoria subCategoria = new SubCategoria();
            Console.Write("Digite o nome da subCategoria :");
            string nome = Console.ReadLine();
            subCategoria.Cadastrar(nome);

            if (subCategoria.VerificarLetras(nome))
            {
                listaDeCategoria.Add(subCategoria);
                foreach (Categoria item in listaDeCategoria)
                {
                    subCategoria.Id++;
                }
                Console.WriteLine($"O id é : ==================== {subCategoria.Id}");
                Console.WriteLine($"O nome da SubCategoria é:==== {subCategoria.Nome}");
                Console.WriteLine($"Criada em :================== {subCategoria.DataCriacao}");
                Console.WriteLine($"O status está:=============== {subCategoria.Status} \n");
                Console.WriteLine("Aperte enter para retornar ao menu");
            }
        }
        public void Editar()
        {
            MostarLista();
            Console.WriteLine("Qual o Id da categoria deseja alterar ?");
            int numeroId = Convert.ToInt32(Console.ReadLine());
            var categoria = listaDeCategoria.Where(item => item.Id.Equals(numeroId));

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
        }

        public void PesquisarNaLista()
        {
            Console.WriteLine("Digite o nome que está procurando");
            string nomeNaLista = Console.ReadLine();
            var pesquisarNaLista = listaDeCategoria.FindAll(item => item.Nome.ToUpper().StartsWith(nomeNaLista.ToUpper()));

            if (pesquisarNaLista.Count() == 0)
            {
                Console.WriteLine($"O nome pesquisado é nula ou não existe");
            }
            else
            {
                foreach (Categoria item in pesquisarNaLista)
                {
                    Console.WriteLine($"O id é : ================= {item.Id}");
                    Console.WriteLine($"O nome da Categoria é:==== {item.Nome}");
                    Console.WriteLine($"Criada em :=============== {item.DataCriacao}");
                    Console.WriteLine($"O status está:============ {item.Status} ");
                    if (item.DateAtualizacao.HasValue)
                    {
                        Console.WriteLine($"Data da ultima atualização {item.DateAtualizacao}\n");
                    }
                }
            }
        }

        public void MostarLista()
        {

            foreach (Categoria item in listaDeCategoria)
            {
                Console.WriteLine($"O id é : ================= {item.Id}");
                Console.WriteLine($"O nome da Categoria é:==== {item.Nome}");
                Console.WriteLine($"Criada em :=============== {item.DataCriacao}");
                Console.WriteLine($"O status está:============ {item.Status} ");
                if (item.DateAtualizacao.HasValue)
                {
                    Console.WriteLine($"Data da ultima atualização {item.DateAtualizacao}\n");
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


