using AutoMapper;
using CategoriaApi.Data;
using CategoriaApi.Data.Dto.DtoProduto;
using CategoriaApi.Model;
using CategoriaApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CategoriaApi.Controllers
{
    [ApiController]
    [Route("controller")]
    public class RepositoryProdutoController: ControllerBase
    {
        private readonly ProdutoRepository _produtoRepository;
        private DatabaseContext _context;
        private IMapper _mapper;


        public RepositoryProdutoController(ProdutoRepository produtoRepository, DatabaseContext context, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProdutos()
        {
            return Ok(await _produtoRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetProdutosById(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> AddProdutos([FromBody] Produto produtoDto)
        {
            return Ok(await _produtoRepository.AddAsync(produtoDto));

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            await _produtoRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto( int id,[FromBody] Produto produto)
        {
            return Ok(await _produtoRepository.UpdateAsync(produto));
        }
    }
}
