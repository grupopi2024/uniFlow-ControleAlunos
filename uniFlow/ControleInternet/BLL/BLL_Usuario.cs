using ControleInternet.DAL;
using ControleInternet.Models;
using System.Collections.Generic;

namespace ControleInternet.BLL
{
    public class BLL_Usuario
    {
        DAL_Usuario dalUsuario = new DAL_Usuario();

        public List<Usuario> ObterUsuario()
        {
            var retorno = dalUsuario.ObterUsuario();

            return retorno;
        }

        public string ExcluirUsuario(UsuarioCPF request)
        {
            var retorno = dalUsuario.ExcluirUsuario(request);

            return retorno;
        }

        public Usuario ObterUsuarioCPF(string CPF)
        {
            var retorno = dalUsuario.ObterUsuarioCPF(CPF);

            return retorno;
        }

    }
}