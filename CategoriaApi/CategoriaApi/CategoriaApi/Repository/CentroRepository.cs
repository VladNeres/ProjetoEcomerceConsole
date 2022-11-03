using AutoMapper;
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
        DatabaseContext _context;
        IMapper _mapper;
        private readonly IDbConnection _dbConnection;

        public CentroRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddCentro(CentroDeDistribuicao centro, CentroDeDistribuicao endereco)
        {
            centro.Logradouro = endereco.Logradouro;
            centro.Localidade = endereco.Localidade;
            centro.Bairro = endereco.Bairro;
            centro.UF = endereco.UF;
            _context.Centros.Add(centro);
            _context.SaveChanges();
        }

        public void ExcluirCentro(CentroDeDistribuicao centro)
        {
            _context.Remove(centro);
            _context.SaveChanges();
        }

        public CentroDeDistribuicao RecuperarCentroId(int id)
        {
            var centro = _context.Centros.FirstOrDefault(centro => centro.Id == id);
            return centro;
        }

        public CentroDeDistribuicao RecuperarCentroNome(CreateCentroDto centroDto)
        {
            var centroNome = _context.Centros.FirstOrDefault(centro => centro.Nome.ToUpper() == centroDto.Nome.ToUpper());
            return centroNome;
        }

        public List<CentroDeDistribuicao> RecuperarListaDeCentro()
        {
            var listaDeCentro = _context.Centros.ToList();
            return listaDeCentro;
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

        public List<CentroDeDistribuicao> GetCentros(string nome, bool? status, string logradouro, int? numero,
            string bairro, string localidade, string uf, string cep, string ordem, int itensPorPagina, int pagina)
        {
            var sql = "SELECT * FROM Where";

            if (sql != null)
            {
                sql += "Nome Like \"%" + nome + "%\" and ";
            }
            if (sql != null)
            {
                sql += "Status = @status";
            }
            if (sql != null)
            {
                sql += "Logradouro Like \"%" + logradouro + "%\" and";
            }
            if (sql != null)
            {
                sql += "Bairro Like \"%" + bairro + "%\" and";
            }
            if (sql != null)
            {
                sql += "Localidade Like \"%" + localidade + "%\"and";
            }
            if (sql != null)
            {
                sql += "UF = @uf";
            }
            if (sql != null)
            {
                sql = "CEP LIKE \"%" + cep + "%\"and";
            }
            if (sql != null)
            {
                sql += "Numero = @numero";
            }
            if(nome == null && status == null && logradouro == null && bairro == null && localidade== null && uf == null && numero== null)
            {
                var posicaodoWhere = sql.IndexOf("Where");
                sql = sql.Remove(posicaodoWhere);
            }
            else
            {
                var posicaoDoAnd = sql.IndexOf("and");
                sql= sql.Remove(posicaoDoAnd);
            }
            if(sql!= null)
            {
                if (ordem== "Decrescente")
                {
                    sql += "Order By Nome Desc";
                }
                else
                {
                    sql += "Order By Nome";   
                }
            }
           var result = _dbConnection.Query<CentroDeDistribuicao>(sql, new
            {
                Nome = nome,
                CEP = cep,
                Localidade = localidade,
                logradouro = logradouro,
                Bairro = bairro,
                Numero = numero,
                UF = uf,
                Status = status
            });
            if (pagina > 0 && itensPorPagina > 0)
            {
                var resultado = result.Skip((pagina - 1) * itensPorPagina).Take(itensPorPagina).ToList();
                return resultado;
            }
            var resultadoSemPaginacao = result.Skip(0).Take(itensPorPagina).ToList();
            return resultadoSemPaginacao;
        }

    }
}
