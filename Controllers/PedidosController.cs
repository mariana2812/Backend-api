using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test4e.Data;
using Test4e.Models;

namespace Test4e.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        // GET - Mostra todos os pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        // GET POR ID - Mostra um pedido específico
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound("Pedido não encontrado.");
            }

            return pedido;
        }

        // POST - Cria um novo pedido
        [HttpPost]
        public async Task<ActionResult<Pedido>> CriarPedido(Pedido pedido)
        {
            // Procura o cliente
            var cliente = await _context.Clientes.FindAsync(pedido.ClienteId);

            if (cliente == null)
            {
                return BadRequest("Cliente não encontrado.");
            }

            // Procura o produto
            var produto = await _context.Produtos.FindAsync(pedido.ProdutoId);

            if (produto == null)
            {
                return BadRequest("Produto não encontrado.");
            }

            // Verifica a quantidade
            if (pedido.Quantidade <= 0)
            {
                return BadRequest("A quantidade deve ser maior que zero.");
            }

            // Verifica o estoque
            if (produto.Estoque < pedido.Quantidade)
            {
                return BadRequest("Estoque insuficiente.");
            }

            
            pedido.DataPedido = DateTime.Now;

           
            pedido.ValorTotal = produto.Preco * pedido.Quantidade;

            
            produto.Estoque = produto.Estoque - pedido.Quantidade;

            
            _context.Pedidos.Add(pedido);

            await _context.SaveChangesAsync();

            return Ok(pedido);
        }

        // DELETE - Exclui um pedido
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound("Pedido não encontrado.");
            }

            _context.Pedidos.Remove(pedido);

            await _context.SaveChangesAsync();

            return Ok("Pedido excluído com sucesso.");
        }
    }
}