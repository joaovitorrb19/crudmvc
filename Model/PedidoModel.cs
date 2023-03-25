using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjetoCurso.Model;

namespace ProjetoCurso.Model {
    
    public class PedidoModel {

        public PedidoModel()
        {
            this.DataAbertura = DateTime.Now;
        }

        [Key]
        public int? PedidoId{get;set;}
        [ForeignKey("Clientes")]
        public int? ClienteId{get;set;}

        public ClienteModel? Cliente{get;set;}

        [ForeignKey("Enderecos")]
        public int? EnderecoId {get;set;}

        public EnderecoModel? Endereco{get;set;}

        public double ValorTotal {get;set;}

        public List<ItemPedidoModel>? ItemsPedidos {get;set;}

        public DateTime DataAbertura{get;set;}

        public DateTime? DataFechamento{get;set;}

        public DateTime? DataEntrega {get;set;}
    }

}