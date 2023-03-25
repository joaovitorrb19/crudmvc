using System.Text.Json;
using ProjetoCurso.Model;

namespace ProjetoCurso.Services{
    
    public class PopularPartialMensagemService{

        public static MensagemPartialModel RetornarPartial(string mensagem,string tipomsg = "Sucesso"){

            //ViewData["Mensagem"];
            var teste = new MensagemPartialModel{
                TipoMSG = tipomsg,
                Mensagem = mensagem
            };

            return teste;
        }

        public static string Serializar(string mensagem,string tipomsg = "Sucesso"){
            var objeto = RetornarPartial(mensagem,tipomsg);
            var serializado = JsonSerializer.Serialize(objeto);
            return serializado;
        }

        public static MensagemPartialModel Desserializar(string model){
            var desserializado = JsonSerializer.Deserialize<MensagemPartialModel>(model);
            return desserializado;
        }

       
    }

}