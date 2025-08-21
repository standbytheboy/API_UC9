using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Data;
using MinhaAPI.Models;

namespace MinhaAPI.Controllers
{
    [ApiController] // Define a rota como Controller de API
    [Route("api/[controller]")] // api/Produto
     
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _contextDb;

        public ProdutoController(AppDbContext contextDb)
        {
            _contextDb = contextDb;

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Produto produto) {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _contextDb.Produtos.Add(produto);
            await _contextDb.SaveChangesAsync();

            return Ok("Produto criado com sucesso");

        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {  
        
            var produtos = await _contextDb.Produtos.ToListAsync();
            return Ok(produtos); 

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            Produto? produto = await _contextDb.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound($"Produto com o id {id} não foi encontrado");
            }
            return Ok(produto);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, Produto produto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            bool exist = await _contextDb.Produtos.AnyAsync(p => p.Id == id); // busca o id do produto no banco de dados

            if(!exist) {
                return NotFound($"Produto com o id {id} não foi encontrado.");
            }

            _contextDb.Entry(produto).State = EntityState.Modified; // verifica as modificações que fiz ao chamar meu update
            await _contextDb.SaveChangesAsync();

            return Ok("Produto atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {

            var produto = await _contextDb.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound($"Produto com o id {id} não foi encontrado");

            }

            _contextDb.Produtos.Remove(produto);
            await _contextDb.SaveChangesAsync();
            return Ok("Produto deletado com sucesso");
        
        }

    }
}
