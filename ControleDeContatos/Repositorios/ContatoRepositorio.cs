using ControleDeContatos.ViewModels;
using ControleDeContatos.Repositorios.Base;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace ControleDeContatos.Repositorios
{
    public class ContatoRepositorio : BaseRepositorio, IContatoRepositorio
    {
        public ContatoRepositorio() : base("API-CONTROLECONTATOS") { }

        public List<ContatoViewModel> BuscarTodos(int usuarioId)
        {
            RestClient client = new RestClient(_urlBase);
            RestRequest request = new RestRequest($"/Contato/BuscarContatosDeUsuario/{usuarioId}");
            IRestResponse response = client.Get(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ResponseViewModel responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                var contatoSerialize = JsonConvert.SerializeObject(responseViewModel.data);
                return JsonConvert.DeserializeObject<List<ContatoViewModel>>(contatoSerialize);
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                throw new Exception(obj.status.message);
            }
        }

        public ContatoViewModel BuscarPorId(int id)
        {
            RestClient client = new RestClient(_urlBase);
            RestRequest request = new RestRequest($"/Contato/{id}");
            IRestResponse response = client.Get(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ResponseViewModel responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                var contatoSerialize = JsonConvert.SerializeObject(responseViewModel.data);
                return JsonConvert.DeserializeObject<ContatoViewModel>(contatoSerialize);
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                throw new Exception(obj.status.message);
            }
        }

        public bool Adicionar(ContatoViewModel contato)
        {
            RestClient client = new RestClient(_urlBase);
            RestRequest request = new RestRequest($"/Contato");
            var contatoJson = JsonConvert.SerializeObject(contato);
            request.AddParameter("application/json; charset=utf-8", contatoJson, ParameterType.RequestBody);
            IRestResponse response = client.Post(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ResponseViewModel responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                var contatoSerialize = JsonConvert.SerializeObject(responseViewModel.data);
                return JsonConvert.DeserializeObject<bool>(contatoSerialize);
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                throw new Exception(obj.status.message);
            }
        }

        public bool Atualizar(ContatoViewModel contato)
        {
            RestClient client = new RestClient(_urlBase);
            RestRequest request = new RestRequest($"/Contato");
            var contatoJson = JsonConvert.SerializeObject(contato);
            request.AddParameter("application/json; charset=utf-8", contatoJson, ParameterType.RequestBody);
            IRestResponse response = client.Put(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ResponseViewModel responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                var contatoSerialize = JsonConvert.SerializeObject(responseViewModel.data);
                return JsonConvert.DeserializeObject<bool>(contatoSerialize);
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                throw new Exception(obj.status.message);
            }
        }

        public bool Apagar(int id)
        {
            RestClient client = new RestClient(_urlBase);
            RestRequest request = new RestRequest($"/Contato/{id}");

            IRestResponse response = client.Delete(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ResponseViewModel responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                var contatoSerialize = JsonConvert.SerializeObject(responseViewModel.data);
                return JsonConvert.DeserializeObject<bool>(contatoSerialize);
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                throw new Exception(obj.status.message);
            }
        }
    }
}