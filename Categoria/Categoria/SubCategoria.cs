using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Categorias
{
    public class SubCategoria : Categoria
    {

        List<SubCategoria> listaSubCategoria = new List<SubCategoria>();
        public SubCategoria()
        {
            Nome = " ";
            Status = "Ativo";
            Data_hora = DateTime.Now;
            DataAtualizada=DateTime.Now;
        }

        public override string Cadastrar()
        {

            bool loopSubCategoria = true;
            while (loopSubCategoria)
            {
                Console.WriteLine("Digite o nome da sub-categoria: ");
                string nomeSubCategoria = Console.ReadLine();

                if (VerificarLetras(nomeSubCategoria))
                {
                    SubCategoria subCategoria = new SubCategoria();

                    subCategoria.Nome = nomeSubCategoria;
                    Nome = nomeSubCategoria;
                    subCategoria.ID = listaSubCategoria.Count + 1;
                    Status = " Ativo";
                    Data_hora = DateTime.Now;


                    Console.WriteLine($"O nome da categoria é : {subCategoria.ID}");
                    Console.WriteLine($"O nome da categoria é : {subCategoria.Nome}");
                    Console.WriteLine($"O Status está:  {subCategoria.Status}");
                    Console.WriteLine($"Criado em :   {subCategoria.Data_hora}");
                    listaSubCategoria.Add(subCategoria);
                    loopSubCategoria = false;
                }
                else
                {
                    Console.WriteLine("A categoria deve conter entre 1 e  50 caracteres (apenas letras)");
                }

                Console.WriteLine();
                Console.WriteLine("Aperte enter para voltar ao menu");
                Console.ReadLine();
                Console.Clear();
            }
            return "Sub-categoria criada com sucesso\n";

        }

        public override string Editar()
        {
            MostrarLista();

            bool loopEditarSubCateforia = true;
            while (loopEditarSubCateforia)
            {

                Console.WriteLine("digite o nome da sub categoria que deseja alterar");
                string alterarNaLista = Console.ReadLine();
                var editarNaLista = listaSubCategoria.Where(subCategoria => subCategoria.Nome.ToUpper().Equals(alterarNaLista.ToUpper()));

                    
                if (editarNaLista.Count() == 0)
                {
                    Console.WriteLine(" sub categoria não encontrada");
                }
                else
                {

                    foreach (Categoria item in listaSubCategoria)
                    {
                        Console.WriteLine("Digite o novo nome da subCategoria");
                        string novoNomeSubCategoria = Console.ReadLine();
                        if (VerificarLetras(novoNomeSubCategoria))
                        {
                            Console.WriteLine($"A subCategoria  {item.Nome}  Criada em :_______  {item.Data_hora}");
                            Nome = novoNomeSubCategoria;
                            
                            Status = "ativo";
                            Console.WriteLine($"Foi atualizada para:_______ ({item.Nome}");
                            Console.WriteLine($"Na data:___________________ ({(item.DataAtualizada)})");
                            Console.WriteLine($"O status da subcategoria é : ({Status}");
                            loopEditarSubCateforia = false;

                        }
                    }

                }
            }
            return " ";
        }
        public override string MostrarLista()
        {
            foreach(var item in listaSubCategoria)
            {
                
                Console.WriteLine($"ID da subCategoria:______{item.ID}");
                Console.WriteLine($"Nome da subCategoria:____{item.Nome}");
                Console.WriteLine($"Status da subCategoria:__{item.Status}");
                Console.WriteLine($"Criado na data: _________{item.Data_hora}");

                if (String.IsNullOrEmpty(item.DataAtualizada.ToLongDateString()))
                {

                }
                else
                {
                    Console.WriteLine($"Data da ultima alteração{item.DataAtualizada= DateTime.Now}");
                }
            }
          
            return " ";
        }
    }
}
 