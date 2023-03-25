using System.Text.Json;
using ProjetoCurso.Model;

namespace ProjetoCurso.Services {

    public class RetornarEnderecoModelService {

        public async static Task<EnderecoModel> GerarEnderecoModel(string cep,string complemento){
           HttpClient auxBuscarCepPost = new HttpClient();
            var teste = await auxBuscarCepPost.GetAsync($"https://viacep.com.br/ws/{cep}/json/"); 
            var jsonNaoSerializado = await  teste.Content.ReadAsStringAsync();
            var jsonSerializado = JsonSerializer.Deserialize<EnderecoModel>(jsonNaoSerializado);
            if(jsonSerializado != null){
            jsonSerializado.cep = jsonSerializado.cep.Remove(5,1);
            EnderecoModel auxEndereco = new EnderecoModel{
                cep = jsonSerializado.cep,
                logradouro = jsonSerializado.logradouro,
                complemento = complemento,
                localidade = jsonSerializado.localidade,
                uf = jsonSerializado.uf,
                bairro = jsonSerializado.bairro,
                ddd = jsonSerializado.ddd};
                return auxEndereco;
            } else {
                return null;
            }
        }

    }

}
