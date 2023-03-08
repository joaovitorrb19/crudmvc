using ProjetoCurso.Model;

namespace ProjetoCurso.Services {

    public class RetornarEnderecoModelService {

        public static EnderecoModel GerarEndereco(string cep,string complemento){
            //PostCEP();

            return new EnderecoModel();
        }

        public async static void PostCEP(string cep){
            HttpClient auxBuscarCepPost = new HttpClient();
            var data = "useragent=projeto";

            StringContent jsonteste = new StringContent("text");

           await auxBuscarCepPost.PostAsJsonAsync($"viacep.com.br/ws/{cep}/",jsonteste);

        }

    }

}
