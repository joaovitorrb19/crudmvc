using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCurso.Model {
    public class ProdutoModel {

        public ProdutoModel()
        {
            DataCriacao = DateTime.Now;
        }

        [Key]
        public int? ProdutoId{get;set;}
        [Required(ErrorMessage ="Nome do Produto Requerido!"),MaxLength(40)]
        public string? NomeProduto {get;set;}
        [Required(ErrorMessage ="Estoque do Produto Requerido!")]
        public int QuantidadeEstoque {get;set;}
        [Required(ErrorMessage ="Preço do Produto Requerido!")]
        public double PrecoProduto {get;set;}
        [ForeignKey("Categorias")]
        [Required(ErrorMessage ="Categoria do Produto Requerido!")]
        public int CategoriaId{get;set;}
        public CategoriaModel? Categoria {get;set;}

        [Required(ErrorMessage ="Nome do Produto Requerido!")]
        public DateTime DataCriacao {get;set;}
    }
}