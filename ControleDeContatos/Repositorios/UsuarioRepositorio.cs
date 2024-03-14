using ControleDeContatos.Helper;
using ControleDeContatos.ViewModels;
using ControleDeContatos.Repositorios.Base;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace ControleDeContatos.Repositorios
{
    public class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
    {
        public UsuarioRepositorio() : base("API-CONTROLECONTATOS") { }

        public List<UsuarioViewModel> BuscarTodos()
        {
            RestClient client = new RestClient(_urlBase);
            RestRequest request = new RestRequest($"/Usuario");
            IRestResponse response = client.Get(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ResponseViewModel responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                var usuarioSerialize = JsonConvert.SerializeObject(responseViewModel.data);
                return JsonConvert.DeserializeObject<List<UsuarioViewModel>>(usuarioSerialize);
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                throw new Exception(obj.status.message);
            }
        }
        
        public UsuarioViewModel BuscarPorId(int id)
        {
            RestClient client = new RestClient(_urlBase);
            RestRequest request = new RestRequest($"/Usuario/{id}");
            IRestResponse response = client.Get(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ResponseViewModel responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                var usuarioSerialize = JsonConvert.SerializeObject(responseViewModel.data);
                return JsonConvert.DeserializeObject<UsuarioViewModel>(usuarioSerialize);
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                throw new Exception(obj.status.message);
            }
        }

        public UsuarioViewModel BuscarPorLogin(string login)
        {
            RestClient client = new RestClient(_urlBase);
            RestRequest request = new RestRequest($"/Usuario/BuscarPorLogin/{login}");
            IRestResponse response = client.Get(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ResponseViewModel responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                var usuarioSerialize = JsonConvert.SerializeObject(responseViewModel.data);
                return JsonConvert.DeserializeObject<UsuarioViewModel>(usuarioSerialize);
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                throw new Exception(obj.status.message);
            }
        }

        public UsuarioViewModel BuscarPorEmailELogin(string email, string login)
        {
            RestClient client = new RestClient(_urlBase);
            RestRequest request = new RestRequest($"/Usuario/BuscarPorEmailELogin/{email}&{login}");
            IRestResponse response = client.Get(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ResponseViewModel responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                var usuarioSerialize = JsonConvert.SerializeObject(responseViewModel.data);
                return JsonConvert.DeserializeObject<UsuarioViewModel>(usuarioSerialize);
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                throw new Exception(obj.status.message);
            }
        }

        public bool Adicionar(UsuarioViewModel usuario)
        {
            usuario.Senha = usuario.Senha.GerarHash();
            RestClient client = new RestClient(_urlBase);
            RestRequest request = new RestRequest($"/Usuario");
            var usuarioJson = JsonConvert.SerializeObject(usuario);
            request.AddParameter("application/json; charset=utf-8", usuarioJson, ParameterType.RequestBody);
            IRestResponse response = client.Post(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ResponseViewModel responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                var usuarioSerialize = JsonConvert.SerializeObject(responseViewModel.data);
                return JsonConvert.DeserializeObject<bool>(usuarioSerialize);
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                throw new Exception(obj.status.message);
            }
        }

        public bool Atualizar(UsuarioSemSenhaViewModel usuarioSemSenha)
        {
            RestClient client = new RestClient(_urlBase);
            RestRequest request = new RestRequest($"/Usuario");
            var usuarioSemSenhaJson = JsonConvert.SerializeObject(usuarioSemSenha);
            request.AddParameter("application/json; charset=utf-8", usuarioSemSenhaJson, ParameterType.RequestBody);
            IRestResponse response = client.Put(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ResponseViewModel responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                var usuarioSerialize = JsonConvert.SerializeObject(responseViewModel.data);
                return JsonConvert.DeserializeObject<bool>(usuarioSerialize);
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
            RestRequest request = new RestRequest($"/Usuario/{id}");
            IRestResponse response = client.Delete(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ResponseViewModel responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                var usuarioSerialize = JsonConvert.SerializeObject(responseViewModel.data);
                return JsonConvert.DeserializeObject<bool>(usuarioSerialize);
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                throw new Exception(obj.status.message);
            }
        }

        public bool AlterarSenha(AlterarSenhaViewModel alterarSenha)
        {
            RestClient client = new RestClient(_urlBase);
            RestRequest request = new RestRequest($"/Usuario/AlterarSenhaUsuario");
            var alterarSenhaJson = JsonConvert.SerializeObject(alterarSenha);
            request.AddParameter("application/json; charset=utf-8", alterarSenhaJson, ParameterType.RequestBody);
            IRestResponse response = client.Put(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                ResponseViewModel responseViewModel = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                var usuarioSerialize = JsonConvert.SerializeObject(responseViewModel.data);
                return JsonConvert.DeserializeObject<bool>(usuarioSerialize);
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<ResponseViewModel>(response.Content);
                throw new Exception(obj.status.message);
            }
        }
    }
}