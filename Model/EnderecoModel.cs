using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCurso.Model {
    public class EnderecoModel{
        [Key]
        public int EnderecoId {get;set;}
        [Required]
        public string? Cep {get;set;}
        [Required]
        public string? Logradouro {get;set;}
        [Required]
        public string? complemento {get;set;}
        [Required]
        public string? localidade {get;set;}
        [Required]
        public string? uf {get;set;}
        [Required]
        public string? ddd {get;set;}
        [ForeignKey("Clientes")]
        public int ClienteId {get;set;}
        public ClienteModel? cliente{get;set;}


    }
}