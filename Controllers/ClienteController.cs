using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Data;
using MinhaAPI.Models;

namespace MinhaAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase {
        private readonly AppDbContext _contextFromDb;
        public ClienteController(AppDbContext contextFromDb) {
            _contextFromDb = contextFromDb;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() { 
            List<Cliente> clientes = await _contextFromDb.Clientes.Include(c => c.Enderecos) // Inclui os endereços relacionados
                .ToListAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            Cliente? cliente = await _contextFromDb.Clientes.Include(c => c.Enderecos) // Inclui os endereços relacionados
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null) {
                return NotFound($"Cliente com o id {id} não foi encontrado");
            }
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            _contextFromDb.Clientes.Add(cliente);
            await _contextFromDb.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }
    }
}
