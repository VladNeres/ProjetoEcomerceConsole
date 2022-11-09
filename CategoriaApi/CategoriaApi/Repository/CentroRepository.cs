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

        public List<CentroDeDistribuicao> GetCentroDeDistribuicao(string nome, string logradouro, string cep,
            string bairro, string localidade, string complemento, string uf, int? numero, bool? status,
            string ordem, int itensPagina, int paginaAtual)
        {
            var sql = "SELECT * FROM Centros WHERE ";

            if (nome != null)
            {
                sql += "Nome LIKE \"%" + nome + "%\" and ";
            }
            if (logradouro != null)
            {
                sql += "Logradouro LIKE \"%" + logradouro + "%\" and ";
            }
            if (status != null)
            {
                sql += "Status = @status and ";
            }
            if (cep != null)
            {
                sql += "Cep = @cep and ";
            }
            if (bairro != null)
            {
                sql += "Bairro LIKE \"%" + bairro + "%\" and ";
            }
            if (localidade != null)
            {
                sql += "Localidade LIKE \"%" + localidade + "%\" and ";
            }
            if (complemento != null)
            {
                sql += "Complemento LIKE \"%" + complemento + "%\" and ";
            }
            if (uf != null)
            {
                sql += "Uf = @uf and ";
            }
            if (numero != null)
            {
                sql += "Numero = @numero and ";
            }

            if (nome == null && logradouro == null && cep == null && bairro == null && localidade == null && complemento == null &&
                uf == null && numero == null && status == null)
            {
                var PosicaoDoWhere = sql.LastIndexOf("WHERE");
                sql = sql.Remove(PosicaoDoWhere);
            }
            else
            {
                var posicaoDoAnd = sql.LastIndexOf("and");
                sql = sql.Remove(posicaoDoAnd);
            }
            if (ordem != null)
            {
                if (ordem != "desc")
                {
                    sql += " ORDER BY Nome";
                }
                else
                {
                    sql += " ORDER BY Nome DESC";
                }
            }

            var result = _dbConnection.Query<CentroDeDistribuicao>(sql, new
            {
                Nome = nome,
                Status = status,
                Logradouro = logradouro,
                Cep = cep,
                Bairro = bairro,
                Localidade = localidade,
                Complemento = complemento,
                Uf = uf,
                Numero = numero
            });

            if (paginaAtual > 0 && itensPagina > 0 && itensPagina <= 10)
            {
                var resultado = result.Skip((paginaAtual - 1) * itensPagina).Take(itensPagina).ToList();
                return resultado;

            }

            var resultadoSemPaginacao = result.Skip(0).Take(25).ToList();
            return resultadoSemPaginacao;

        }


    }
}

