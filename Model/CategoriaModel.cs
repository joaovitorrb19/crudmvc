using System.ComponentModel.DataAnnotations;

namespace ProjetoCurso.Model {

    public class CategoriaModel {

        public CategoriaModel()
        {
            DataCriacao = DateTime.Now;
        }

        [Key]
        public int? CategoriaId {get;set;}
        [Required(ErrorMessage = "Nome da Categoria Requerida!")]
        public string? NomeCategoria{get;set;}
        [Required(ErrorMessage = "Data da criação Requerida!")]
        public DateTime DataCriacao{get;set;}
    }

}