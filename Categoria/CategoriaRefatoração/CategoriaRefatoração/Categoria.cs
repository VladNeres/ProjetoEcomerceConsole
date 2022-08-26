using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CategoriaRefatoracao
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; protected set; }
        public string Status { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime? DateAtualizacao { get; protected set; }
        

        public Categoria(string nome)
        {
            Nome = nome;
            Status = "Ativo";
            DataCriacao = DateTime.Now;
        }
        public Categoria()
        {
            Nome = "";
            Id = 0;
            Status = "Ativo";
            DataCriacao = DateTime.Now;

        }

        public bool VerificarLetras(string nome)
        {
            int regex = Regex.Matches(nome, @"[a-zA-Zá-úÁ-Ú' ']").Count;
            if (!string.IsNullOrWhiteSpace(nome) && nome.Length > 0 && nome.Length <= 50 && nome.Length == regex)
            {
                return true;
            }
            return false;
        }


        public virtual void Cadastrar(string nomeDaClasse)
        {
            Console.WriteLine("Digite o nome da categoria");
            if (VerificarLetras(nomeDaClasse))
            {
                Nome = nomeDaClasse;
            }
            else
            {
                Console.WriteLine("(Atenção!)\n" +
                                  "A categoria deve ser cadastrada apenas com letras (digite entre 1 e 50 caracteres)");
            }
        }

        public virtual void Editar(string nomeEditar)
        {

            if (VerificarLetras(nomeEditar))
            {

                bool loopStatus = true;
                while (loopStatus)
                {
                    Console.WriteLine("Deseja alterar o status?\n" +
                                     "Digite (S) para Sim ou (N) para Não");
                    string menuOpcao = Console.ReadLine();
                    switch (menuOpcao.ToUpper())
                    {
                        case "S":
                            Status = "Inativo";
                            loopStatus = false;
                            break;

                        case "N":
                            Status = "Ativo";
                            loopStatus = false;
                            break;

                        default:
                            Console.WriteLine("Opção invalida, por favor escolha uma das opções");
                            break;
                    }
                }

                Console.WriteLine($"|A Categoria  ({Nome}) criada na data: ({DataCriacao})|");
                Console.WriteLine("==========================================================");
                Nome = nomeEditar;
                DateAtualizacao = DateTime.Now;
                Console.WriteLine($"A categoria de ID========= ({Id})");
                Console.WriteLine($"Foi atualizada para======= ({Nome})");
                Console.WriteLine($"Na data=================== ({DateAtualizacao})");
                Console.WriteLine($"O status está============= ({Status})");
            }
            else
            { Console.WriteLine("(Atenção!)\n" +
                                  "A categoria deve ser cadastrada apenas com letras (digite entre 1 e 50 caracteres)");
            
            }

        }


    }
}
