﻿using ControleDeContatos.Helper;
using ControleDeContatos.ViewModels;
using ControleDeContatos.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }

        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");

            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(LoginViewModel loginViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioViewModel usuario = _usuarioRepositorio.BuscarPorLogin(loginViewModel.Login);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginViewModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MenssagemErro"] = "Senha do usuário inválida, tente novamente.";
                        return View("Index");
                    }

                    TempData["MenssagemErro"] = "Usuário e/ou senha inválido(s). Por favor, tente novamente.";
                    return View("Index");
                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos realizar seu login, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaViewModel redefinirSenha)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioViewModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenha.Email, redefinirSenha.Login);
                    AlterarSenhaViewModel alterarSenha = new AlterarSenhaViewModel();
                    alterarSenha.Id = usuario.Id;
                    alterarSenha.SenhaAtual = usuario.Senha;

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        alterarSenha.NovaSenha = novaSenha.GerarHash();
                        alterarSenha.ConfirmarNovaSenha = novaSenha.GerarHash();

                        string menssagem = $"Sua nova senha é: {novaSenha}";
                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de Contatos - Nova Senha", menssagem);

                        if (emailEnviado)
                        {
                            _usuarioRepositorio.AlterarSenha(alterarSenha);
                            TempData["MenssagemSucesso"] = "Enviamos para seu e-mail cadastrado uma nova senha.";
                        }
                        else
                        {
                            TempData["MenssagemErro"] = "Não conseguimos enviar o e-mail. Por favor, tente novamente.";
                        }

                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MenssagemErro"] = "Não conseguimos redefinir sua senha. Por favor, verifique os dados informados.";
                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MenssagemErro"] = $"Ops, não conseguimos redefinir sua senha, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}