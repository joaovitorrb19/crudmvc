using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCurso.Model {
    public class EnderecoModel{
        [Key]
        public int EnderecoId {get;set;}
        [Required,MinLength(8),MaxLength(8)]
        public string cep {get;set;}
        [Required]
        public string? logradouro {get;set;}
        [Required,MinLength(5),MaxLength(50)]
        public string complemento {get;set;}
        [Required]
        public string? bairro {get;set;}
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