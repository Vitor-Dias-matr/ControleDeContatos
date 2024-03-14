using ControleDeContatos.Filters;
using ControleDeContatos.ViewModels;
using ControleDeContatos.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [PaginaRestritaSomenteAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IContatoRepositorio _contatoRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IContatoRepositorio contatoRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _contatoRepositorio = contatoRepositorio;
        }

        public IActionResult Index()
        {
            List<UsuarioViewModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            UsuarioViewModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioViewModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }
        
        public IActionResult Apagar(int id)
        {
            try
            {
                var apagado = _usuarioRepositorio.Apagar(id);

                if (apagado) TempData["MenssagemSucesso"] = "Usuário apagado com sucesso!";
                else TempData["MenssagemErro"] = "Ops, não conseguimos apagar seu usuário!";

                return RedirectToAction("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos apagar seu usuário, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ListarContatosPorUsuarioId(int id)
        {
            List<ContatoViewModel> contatos = _contatoRepositorio.BuscarTodos(id);
            return PartialView("_ContatosUsuario", contatos);
        }

        [HttpPost]
        public IActionResult Criar(UsuarioViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var adicionado = _usuarioRepositorio.Adicionar(usuario);

                    if(adicionado)
                    {
                        TempData["MenssagemSucesso"] = "Usuário cadastrado com sucesso!";
                        return RedirectToAction("Index");
                    }
                }

                TempData["MenssagemErro"] = $"Ops, não conseguimos cadastrar seu usuário!";
                return View(usuario);
            }
            catch (System.Exception erro)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos cadastrar seu usuário, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Editar(UsuarioSemSenhaViewModel usuarioSemSenha)
        {
            try
            {
                UsuarioViewModel usuario = null;

                if (ModelState.IsValid)
                {
                    var editado = _usuarioRepositorio.Atualizar(usuarioSemSenha);

                    if (editado)
                    {
                        TempData["MenssagemSucesso"] = "Usuário alterado com sucesso!";
                        return RedirectToAction("Index");
                    }
                }

                TempData["MenssagemErro"] = $"Ops, não conseguimos editar seu usuário!";
                return View(usuario);
            }
            catch (System.Exception erro)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos editar seu usuário, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}