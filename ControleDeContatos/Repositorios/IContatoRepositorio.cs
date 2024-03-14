using ControleDeContatos.ViewModels;

namespace ControleDeContatos.Repositorios
{
    public interface IContatoRepositorio
    {
        List<ContatoViewModel> BuscarTodos(int usuarioId);
        ContatoViewModel BuscarPorId(int id);
        bool Adicionar(ContatoViewModel contato);
        bool Atualizar(ContatoViewModel contato);
        bool Apagar(int id);
    }
}