using System.ComponentModel.DataAnnotations;

namespace ProjetoCurso.Model {

    public class CategoriaModel {

        public CategoriaModel()
        {
            dataCriacao = DateTime.Now;
        }

        [Key]
        public int CategoriaId {get;set;}
        [Required(ErrorMessage = "Nome da Categoria Requerida!"),MaxLength(40)]
        public string? nomeCategoria{get;set;}
        [Required(ErrorMessage = "Data da criação Requerida!")]
        public DateTime dataCriacao{get;set;}
    }

}