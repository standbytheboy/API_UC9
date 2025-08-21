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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Usuario? usuario = await _contextFromDb.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound($"Usuário com o id {id} não foi encontrado");
            }
            return Ok(usuario);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Usuario user)
        {

            if (id != user.Id)
            {
                return BadRequest("O ID enviado não corresponde ao ID do usuário no Banco de Dados");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool exist = await _contextFromDb.Usuario.AnyAsync(p => p.Id == id);

            if (!exist)
            {
                return NotFound($"Usuário com o id {id} não foi encontrado.");
            }

            _contextFromDb.Entry(user).State = EntityState.Modified; // verifica as modificações que fiz ao chamar meu update
            await _contextFromDb.SaveChangesAsync();

            return Ok("Usuário atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var user = await _contextFromDb.Usuario.FindAsync(id);

            if (user == null)
            {
                return NotFound($"Usuário com o id {id} não foi encontrado");

            }

            _contextFromDb.Usuario.Remove(user);
            await _contextFromDb.SaveChangesAsync();
            return Ok("Usuário deletado com sucesso");

        }

    }
}
