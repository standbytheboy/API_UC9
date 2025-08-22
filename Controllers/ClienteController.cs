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
    }
}
