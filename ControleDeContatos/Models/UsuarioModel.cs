using ControleDeContatos.Enums;
using ControleDeContatos.Helper;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public PerfilEnum? Perfil { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizalcao { get; set; }

        public virtual List<ContatoModel>? Contatos { get; set; }
    
        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}