using System.ComponentModel.DataAnnotations;

namespace MinhaAPI.Models {
    public class Cliente {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve conter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O email do cliente é obrigatório.")]
        [StringLength(100, ErrorMessage = "O email deve conter no máximo 100 caracteres.")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O CPF do cliente é obrigatório.")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "O CPF deve conter no máximo 14 caracteres.")]
        public string Cpf { get; set; } = string.Empty;

        [Required(ErrorMessage = "O telefone do cliente é obrigatório.")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "O telefone deve conter no máximo 14 caracteres.")]
        public string Telefone { get; set; } = string.Empty;

        public List<Endereco> Enderecos { get; set; } = new List<Endereco>(); // Relacionamento com Endereco
    }
}
