using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test4e.Data;
using Test4e.Models;

namespace Test4e.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.Produtos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto =
                await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            return produto;
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> CriarProduto(
            Produto produto
        )
        {
            _context.Produtos.Add(produto);

            await _context.SaveChangesAsync();

            return Ok(produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProduto(
            int id,
            Produto produto
        )
        {
            var produtoBanco =
                await _context.Produtos.FindAsync(id);

            if (produtoBanco == null)
            {
                return NotFound("Produto não encontrado.");
            }

            produtoBanco.Nome = produto.Nome;
            produtoBanco.Descricao = produto.Descricao;
            produtoBanco.Preco = produto.Preco;
            produtoBanco.Estoque = produto.Estoque;

            await _context.SaveChangesAsync();

            return Ok(produtoBanco);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirProduto(int id)
        {
            var produto =
                await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }

            _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();

            return Ok("Produto excluído.");
        }
    }
}