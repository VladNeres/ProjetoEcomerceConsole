using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Categorias
{
    public class SubCategoria : Categoria
    {
        
        List<SubCategoria> listaSubCategoria =new List<SubCategoria>();
        public SubCategoria()
        {
            Nome = " ";
            Status = "Ativo";
            Data_hora = DateTime.Now;
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
                    subCategoria.ID = listaSubCategoria.Count+1;
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
            }
            return "Sub-categoria criada com sucesso\n";

        }

            public override string Editar()
            {
               

                bool loopEditarSubCateforia = true;
                while (loopEditarSubCateforia)
                {

                    Console.WriteLine("digite o nome da sub categoria que deseja alterar");
                    string alterarNaLista= Console.ReadLine();

                   var editarNaLista = listaSubCategoria.Where(subCategoria => subCategoria.Nome.ToLower().Equals(alterarNaLista.ToUpper()));
                    // if(editarNaLista == 0)
                    //{
                    //    Console.WriteLine(" sub categoria não encontrada");
                    //}
                //else
                //{
                //    Console.WriteLine("Digite o novo nome da subCategoria");
                    string novoNomeSubCategoria = Console.ReadLine();
                    if (VerificarLetras(novoNomeSubCategoria))
                    {
                        Console.WriteLine("A subCategoria " + Nome + " Criada em :" + Data_hora + "\n" +
                                          "Foi atualizada para : " + (Nome = novoNomeSubCategoria) + "\n" +
                                          "Na data: " + (DataAtualizada = DateTime.Now) + "\n" +
                                          "O status da subcategoria é : " + (Status = "ativo"));
                        loopEditarSubCateforia = false;

                    }
                //}
            }
                    
                return " ";
            }

        //public override string MostarNaTela()
        //{

        //}

    }

}
 