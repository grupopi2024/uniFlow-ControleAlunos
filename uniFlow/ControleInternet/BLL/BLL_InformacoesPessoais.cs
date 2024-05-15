using ControleInternet.DAL;
using ControleInternet.Models;

namespace ControleInternet.BLL
{
    public class BLL_InformacoesPessoais
    {
        DAL_InformacoesPessoais dalInformacoesPessoais = new DAL_InformacoesPessoais();
        public string AlteraDadosPessoais(Usuario user)
        {
            var retorno = dalInformacoesPessoais.AlteraDadosPessoais(user);

            return retorno;
        }
        public Usuario BuscarDadosPessoaisById(UsuarioId user)
        {
            var retorno = dalInformacoesPessoais.BuscarDadosPessoaisById(user);

            return retorno;
        }
        public Usuario BuscarDadosPessoaisByEmail(string email)
        {
            var retorno = dalInformacoesPessoais.BuscarDadosPessoaisByEmail(email);

            return retorno;
        }
    }
}