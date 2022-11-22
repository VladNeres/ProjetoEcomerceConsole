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
            if (centroDto.CEP.Length == 8)
            {
               centroDto.CEP = centroDto.CEP.Insert(5, "-");
               System.Console.WriteLine(centroDto.CEP);
            }
           var endereco = _context.Centros.FirstOrDefault(centro=> centro.CEP == centroDto.CEP && centroDto.Numero == centro.Numero);
            return endereco;
        }
        public void Salvar()
        {
            _context.SaveChanges();
        }

        public List<CentroDeDistribuicao> GetCentroDeDistribuicao(ReadCentroDto readDto,string ordem, int itensPagina, int paginaAtual)
        {
            var sql = "SELECT * FROM Centros WHERE ";

            if (readDto.Nome != null)
            {
                sql += "Nome LIKE \"%" + readDto.Nome + "%\" and ";
            }
            if (readDto.Logradouro != null)
            {
                sql += "Logradouro LIKE \"%" + readDto.Logradouro + "%\" and ";
            }
            if (readDto.Status != null)
            {
                sql += "Status = @status and ";
            }
            if (readDto.CEP != null)
            {
                sql += "Cep = @cep and ";
            }
            if (readDto.Bairro != null)
            {
                sql += "Bairro LIKE \"%" + readDto.Bairro + "%\" and ";
            }
            if (readDto.Localidade != null)
            {
                sql += "Localidade LIKE \"%" + readDto.Localidade + "%\" and ";
            }
            if (readDto.Complemento != null)
            {
                sql += "Complemento LIKE \"%" + readDto.Complemento + "%\" and ";
            }
            if (readDto.UF != null)
            {
                sql += "Uf = @uf and ";
            }
            if (readDto.Numero != null)
            {
                sql += "Numero = @numero and ";
            }

            if (readDto.Nome == null && readDto.Logradouro == null && readDto.CEP == null && readDto.Bairro == null 
                && readDto.Localidade == null && readDto.Complemento == null &&
                readDto.UF == null && readDto.Numero == null && readDto.Status == null)
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
                Nome = readDto.Nome,
                Status = readDto.Status,
                Logradouro = readDto.Logradouro,
                Cep = readDto.CEP,
                Bairro = readDto.Bairro,
                Localidade = readDto.Localidade,
                Complemento = readDto.Complemento,
                Uf = readDto.UF,
                Numero = readDto.Numero
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

