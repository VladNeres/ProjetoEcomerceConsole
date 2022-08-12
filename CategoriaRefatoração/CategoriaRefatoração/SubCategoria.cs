using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoriaRefatoracao
{
    public class SubCategoria: Categoria
    {
        public SubCategoria(string nome): base(nome)
        {

        }

        public SubCategoria():base ()
        {

        }

        public  override void Cadastrar(string nome)
        {

            
            if (VerificarLetras(nome))
            {
                Nome = nome;

                bool loop = true;
                while (loop)
                {
                    Console.WriteLine("Deseja que o status seja :\n" +
                                      "(A) ativo ou (I) inativo");
                    string opcaoStatus = Console.ReadLine();
                    switch (opcaoStatus.ToUpper())
                    {
                        case "A":
                            Status = "Ativo";
                            loop = false;
                            break;
                        
                        case "I":
                            Status = "Inativo";
                            loop=false;
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("O nome deve ser cadastrado utilizando apenas letras entre (1 e 50 caracteres)");
            }
        }

        public override void Editar(string nomeEditar)
        {
            Console.WriteLine("Digite o novo nome da subCategoria");
            if (VerificarLetras(nomeEditar))
            {
                Console.WriteLine($"|A subcategoria ({Nome}) criada na data ({DataCriacao})|");
                Console.WriteLine("=======================================================");
                Nome=nomeEditar;
                DateAtualizacao = DateTime.Now;
                Console.WriteLine($"O nome da subcategoria foi atualizado para ==== ({Nome})");
                Console.WriteLine($"O status está ================================= ({Status})");
                Console.WriteLine($"Na data ======================================= ({DateAtualizacao})");
            }
            else
            {
                Console.WriteLine("(Atenção!)\n" +
                                  "A categoria deve ser cadastrada apenas com letras (digite entre 1 e 50 caracteres)");

            }
        }
    }
}
