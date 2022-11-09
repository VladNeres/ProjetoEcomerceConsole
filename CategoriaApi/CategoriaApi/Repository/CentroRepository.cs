using CategoriaApi.Data;
using CategoriaApi.Data.Dto.CentroDto;
using CategoriaApi.Model;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CategoriaApi.Repository
{
    public class CentroRepository
    {
        private DatabaseContext _context;
        private readonly IDbConnection _dbConnection;

        public CentroRepository(DatabaseContext centroRepository, IDbConnection connection)
        {
            _dbConnection = connection;
            _context = centroRepository;
        }

        public void AddCentro(CentroDeDistribuicao centro, CentroDeDistribuicao endereco)
        {
            centro.UF = endereco.UF;
            centro.Localidade= endereco.Localidade;
            centro.CEP = endereco.CEP;
            centro.Logradouro= endereco.Logradouro;
            centro.Bairro = endereco.Bairro;

            _context.Add(centro);
            _context.SaveChanges();
        }

        public void ExcluirCentro(CentroDeDistribuicao centro)
        {
            _context.Remove(centro);
            _context.SaveChanges();
        }

        public CentroDeDistribuicao RecuperarCentroPorId(int id)
        {
            CentroDeDistribuicao centro = _context.Centros.FirstOrDefault(centro => centro.Id == id);
                return centro;
        }

        public CentroDeDistribuicao RetornarNomeDocentro(CreateCentroDto centroDto)
        {
            CentroDeDistribuicao centroNome = _context.Centros.FirstOrDefault(centro => centro.Nome.ToLower() == centroDto.Nome.ToLower());
            return centroNome;
        }

        public CentroDeDistribuicao RetornarEndereco(CreateCentroDto centroDto)
        {
            var endereco = _context.Centros.FirstOrDefault(centro => centro.CEP == centroDto.CEP);
            return endereco;
        }
        public void Salvar()
        {
            _context.SaveChanges();
        }

        public List<CentroDeDistribuicao> GetCentroDeDistribuicao(string nome, bool? status,string cep, string logradouro, int? numero, string uf,
            string bairro, string localidade,string complemento, string ordem, int itensPorPagina, int pagina)
        {
            var sql = "SELECT * FROM Centros WHERE";

            if(sql!= null)
            {
                sql += "Nome LIKE \"%" + nome + "%\" and"; 
            }
            if (sql!= null)
            {
                sql += "Status = @status and";
            }
            if (status != null)
            {
                sql = "CEP LIKE \"%" + cep + "%\" and";
            }
            if (sql!= null)
            {
                sql += "Logradouro \"%" + logradouro + "%\" and";
            }
            if (sql!= null)
            {
                sql += "Bairro LIKE \"%" + bairro + "%\" and";
            }
            if(sql!= null)
            {
                sql += "Numero = @numero and";
            }
            if(nome!= null)
            {
                sql += "UF= @uf and";
            }
            if(sql!= null)
            {
                sql += "Localidade LIKE \"%" + localidade + "%\" and";
            }
            if(complemento!= null)
            {
                sql += "Complemento  LIKE \"%" + complemento + "%\" and";
            }
            if(nome == null && cep== null&& status== null&& nome== null && status== null && logradouro== null && numero==null
                && uf== null&&localidade== null)
            {
                var indexWhere = sql.LastIndexOf("WHERE");
                var removerWhere = sql.Remove(indexWhere);
            }
            else
            {
                var indexAnd = sql.IndexOf("and");
                var removerAnd=sql.Remove(indexAnd);
            }
            if (sql != null)
            {
                if (ordem != "DECRESCENTE")
                {
                    sql += "ORDER BY Nome";
                }
                else
                {
                    sql += "ORDER BY Nome Desc";
                }
            }
                var result = _dbConnection.Query<CentroDeDistribuicao>(sql, new
                {
                    Nome = nome,
                    Status= status,
                    Logradouro = logradouro,
                    Numero = numero,
                    Uf= uf,
                    Localidade= localidade,
                    Bairro= bairro,
                    CEP= cep,
                    Complemento = complemento
                });

                if (pagina>0 && itensPorPagina > 0)
                {
                    var quantidade = result.Skip((pagina-1) * itensPorPagina).Take(itensPorPagina).ToList();
                    return quantidade;
                }
                var resultadoSemPaginacao = result.Skip(0).Take(10).ToList();
                return resultadoSemPaginacao;
            

        }
    }
}
