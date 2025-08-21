using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaAPI.Data;
using MinhaAPI.Models;

namespace MinhaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _contextFromDb;

        public UsuarioController(AppDbContext contextFromDb)
        {
            _contextFromDb = contextFromDb;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Usuario user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _contextFromDb.Usuario.Add(user);
            await _contextFromDb.SaveChangesAsync();

            return Ok("Usuário criado com sucesso");

        }

    }
}
