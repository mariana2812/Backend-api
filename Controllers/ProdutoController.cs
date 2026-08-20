using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test4e.Data; 
using Test4e.models; 

namespace Test4e.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            
            var listaDeProdutos = await _context.Produtos.ToListAsync();

            return Ok(listaDeProdutos);
        }
    }
}
