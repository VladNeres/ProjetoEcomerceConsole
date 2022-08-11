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
        public int _id { get; set; }
        public string _nome { get; protected set; }
        public string _status { get; protected set; }
        public DateTime _dataCriacao { get; protected set; }
        public DateTime? _dateAtualizacao { get; protected set; }
        List<Categoria> _categorias = new List<Categoria>();

        public Categoria(string nome)
        {
            _nome = nome;
            _status = "Ativo";
            _dataCriacao = DateTime.Now;
        }
        public Categoria()
        {
            _nome = "";
            _id = 0;
            _status = "Ativo";
            _dataCriacao = DateTime.Now;

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
                _nome = nomeDaClasse;
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
                            _status = "Inativo";
                            loopStatus = false;
                            break;

                        case "N":
                            _status = "Ativo";
                            loopStatus = false;
                            break;

                        default:
                            Console.WriteLine("Opção invalida, por favor escolha uma das opções");
                            break;
                    }
                }

                Console.WriteLine($"|A Categoria  ({_nome}) criada na data: ({_dataCriacao})|");
                Console.WriteLine("==========================================================");
                _nome = nomeEditar;
                _dateAtualizacao = DateTime.Now;
                Console.WriteLine($"A categoria de ID========= ({_id})");
                Console.WriteLine($"Foi atualizada para======= ({_nome})");
                Console.WriteLine($"Na data=================== ({_dateAtualizacao})");
                Console.WriteLine($"O status está============= ({_status})");
            }

        }


    }
}
