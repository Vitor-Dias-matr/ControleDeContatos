using ControleDeContatos.ViewModels;

namespace ControleDeContatos.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(UsuarioViewModel usuario);
        void RemoverSessaoUsuario();
        UsuarioViewModel BuscarSessaoDoUsuario();
    }
}