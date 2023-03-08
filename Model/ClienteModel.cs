using System.ComponentModel.DataAnnotations;

namespace ProjetoCurso.Model {
    public class ClienteModel {
        [Key]
        public int? ClienteId {get;set;}
        [Required]
        public string? NomeCliente {get;set;}
        [Required]
        public string? EmailCliente {get;set;}
        [Required]
        public string? CPFCliente {get;set;}
        [Required]
        public int IdadeCliente {get;set;}
        [Required]
        public DateTime DataNascimento {get;set;}
    }
}