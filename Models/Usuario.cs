using System.ComponentModel.DataAnnotations;

namespace MinhaAPI.Models
{
    public class Usuario {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(20, ErrorMessage = "O nome do usuário deve ter no máximo 20 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O sobrenome do usuário é obrigatório.")]
        [StringLength(20, ErrorMessage = "O sobrenome do usuário deve ter no máximo 20 caracteres.")]
        public string Sobrenome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email do usuário é obrigatório.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "O senha deve ter no entre 6 e 30 caracteres.")]
        public string Senha { get; set; } = string.Empty;
        
    }
}
