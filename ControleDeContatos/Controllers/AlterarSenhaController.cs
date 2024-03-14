using ControleDeContatos.Helper;
using ControleDeContatos.ViewModels;
using ControleDeContatos.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class AlterarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterarSenhaViewModel alterarSenha)
        {
            try
            {
                UsuarioViewModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                alterarSenha.Id = usuarioLogado.Id;
                alterarSenha.SenhaAtual = alterarSenha.SenhaAtual.GerarHash();
                alterarSenha.NovaSenha = alterarSenha.NovaSenha.GerarHash();
                alterarSenha.ConfirmarNovaSenha = alterarSenha.ConfirmarNovaSenha.GerarHash();

                if (ModelState.IsValid)
                {
                    var alterado = _usuarioRepositorio.AlterarSenha(alterarSenha);
                    if (alterado)
                    {
                        TempData["MenssagemSucesso"] = "Senha alterada com sucesso!";
                        return View("Index", alterarSenha);
                    }
                }

                TempData["MenssagemErro"] = "Ops, não conseguimos alterar sua senha!";
                return View("Index", alterarSenha);
            }
            catch (Exception erro)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos alterar sua senha, tente novamente, detalhe do erro: {erro.Message}";
                return View("Index", alterarSenha);
            }
        }
    }
}