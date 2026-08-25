using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test4e.Data;
using Test4e.Models;

namespace Test4e.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            return cliente;
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> CriarCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);

            await _context.SaveChangesAsync();

            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCliente(
            int id,
            Cliente cliente
        )
        {
            var clienteBanco =
                await _context.Clientes.FindAsync(id);

            if (clienteBanco == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            clienteBanco.Nome = cliente.Nome;
            clienteBanco.Email = cliente.Email;
            clienteBanco.Telefone = cliente.Telefone;

            await _context.SaveChangesAsync();

            return Ok(clienteBanco);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirCliente(int id)
        {
            var cliente =
                await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            _context.Clientes.Remove(cliente);

            await _context.SaveChangesAsync();

            return Ok("Cliente excluído.");
        }
    }
}