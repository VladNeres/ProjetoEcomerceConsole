using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CategoriaRefatoracao
{
    public class Produto:Categoria
    {
       public string Descricao { get; set; }
        public double Peso { get; set; } 
        public double Altura { get; set; }
        public double Comprimento {get; set; }
        public double Valor { get; set; }
        public int Estoque { get; set; }
        public string CentroDeDistribuicao { get; set; }

        public Produto(string nome)
        {
            Nome = nome;
            Peso = 0;
            Altura = 0;
            Comprimento = 0;
            Valor = 0;
            Estoque = 0;
            CentroDeDistribuicao = "matriz";
        }

        public Produto()
        {
            
            Peso = 0;
            Altura = 0;
            Comprimento = 0;
            Valor = 0;
            Estoque = 0;
            CentroDeDistribuicao = "matriz";
        }
        public bool VerificarDescricao(string descricao)
        {
            int regex = Regex.Matches(descricao, @"[a-zA-Z-á-úÁ-Ú ' ']").Count();
            if(descricao.Length>0 && descricao.Length<= 512 && descricao.Length == regex)
            {
                return true;
            }
            return false;
        }


        public double TryValores(string valor)
        {
            var deuCerto = double.TryParse(s: valor, out var valordouble);
            if (deuCerto == true)
            {
                return valordouble;
            }
            return 0;
        }
        public void CadastrarProduto(string nome , string descricao)
        {

            if (VerificarLetras(nome) && VerificarDescricao(descricao))
            {
                Nome=nome;
                Descricao=descricao;

                Console.Write("Digite a altura=> "); 
                Altura= TryValores(Console.ReadLine());

                Console.Write("Digite o peso => ");
                Peso= Convert.ToDouble(Console.ReadLine());

                Console.Write("Digite o comprimento =>");
                Comprimento = TryValores(Console.ReadLine());

                Console.Write("Digite o valor =>" );
                Valor = TryValores(Console.ReadLine());
                
                Console.Write("Digite a quantidade em estoque =>");
                Estoque = Convert.ToInt32(Console.ReadLine());
                
                Console.Write("Digite qual o centro de distribuição => ");
                CentroDeDistribuicao = Console.ReadLine();
                Console.WriteLine(' ');
                
               DisplayProduto();

            }
            
        }

        public void EditarProduto(string nome, string descricao)
        {
                       
            if (VerificarLetras(nome))
            {

                Nome = nome;
                Descricao = descricao;
                Console.Write("Digite a altura=> ");
                Altura = TryValores(Console.ReadLine());

                Console.Write("Digite o peso => ");
                Peso = Convert.ToDouble(Console.ReadLine());

                Console.Write("Digite o comprimento =>");
                Comprimento = TryValores(Console.ReadLine());

                Console.Write("Digite o valor =>");
                Valor = TryValores(Console.ReadLine());

                Console.Write("Digite a quantidade em estoque =>");
                Estoque = Convert.ToInt32(Console.ReadLine());

                Console.Write("Digite qual o centro de distribuição => ");
                CentroDeDistribuicao = Console.ReadLine();

                Console.WriteLine(" ");
                DisplayProduto();
                Console.WriteLine("A categoria Atualizada com sucesso");

            }
        }


        public void PesquisarProduto(List<Produto> produtos)
        {

            foreach(Produto produto in produtos)
            {
                Console.WriteLine(produto.Nome);
            }

        }
       
        public void DisplayProduto()
        {
            Console.WriteLine($"O nome do produto:============={Nome}");
            Console.WriteLine($"A altura do produto:==========={Altura}");
            Console.WriteLine($"O peso do produto:============={Peso}kg/L");
            Console.WriteLine($"O comprimento do produto:======{Comprimento} cm");
            Console.WriteLine($"O valor do produto:============{Valor} R$");
            Console.WriteLine($"A quantidade em estoque:======={Estoque}");
            Console.WriteLine($"No centro de distribuição:====={CentroDeDistribuicao}\n");
        }
    }

}
