using ControleDeContatos.Filters;
using ControleDeContatos.Helper;
using ControleDeContatos.ViewModels;
using ControleDeContatos.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;

        public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            UsuarioViewModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();

            List<ContatoViewModel> contatos = _contatoRepositorio.BuscarTodos(usuarioLogado.Id);
            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            ContatoViewModel contato = _contatoRepositorio.BuscarPorId(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ContatoViewModel contato = _contatoRepositorio.BuscarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                var apgado = _contatoRepositorio.Apagar(id);

                if (apgado) TempData["MenssagemSucesso"] = "Contato apagado com sucesso!";
                else TempData["MenssagemErro"] = "Ops, não conseguimos apagar seu contato!";
                
                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos apagar seu contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ContatoViewModel contato)
        {
            try{
                if (ModelState.IsValid)
                {
                    UsuarioViewModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    var criado = _contatoRepositorio.Adicionar(contato);
                    if (criado)
                    {
                        TempData["MenssagemSucesso"] = "Contato cadastrado com sucesso!";
                        return RedirectToAction("Index");
                    }
                }
                
                TempData["MenssagemErro"] = "Ops, não conseguimos cadastrar seu contato!";
                return View(contato);
            }
            catch (System.Exception erro)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos cadastrar seu contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(ContatoViewModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioViewModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    var alterado = _contatoRepositorio.Atualizar(contato);
                    if (alterado)
                    {
                        TempData["MenssagemSucesso"] = "Contato alterado com sucesso!";
                        return RedirectToAction("Index");
                    }
                }
                    
                TempData["MenssagemErro"] = "Ops, não conseguimos alterar seu contato!";
                return View("Editar", contato);
            }
            catch (System.Exception erro)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos alterar seu contato, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}