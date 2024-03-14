using ControleDeContatos.ViewModels;

namespace ControleDeContatos.Repositorios
{
    public interface IUsuarioRepositorio
    {
        List<UsuarioViewModel> BuscarTodos();
        UsuarioViewModel BuscarPorId(int id);
        UsuarioViewModel BuscarPorLogin(string login);
        UsuarioViewModel BuscarPorEmailELogin(string email, string login);
        bool Adicionar(UsuarioViewModel usuario);
        bool Atualizar(UsuarioSemSenhaViewModel usuarioSemSenhaModel);
        bool Apagar(int id);
        bool AlterarSenha(AlterarSenhaViewModel alterarSenhaModel);
    }
}