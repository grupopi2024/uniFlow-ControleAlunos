using ControleInternet.DAL;
using ControleInternet.Models;

namespace ControleInternet.BLL
{
    public class BLL_LoginUsuario
    {
        DAL_LoginUsuario dalUsuario = new DAL_LoginUsuario();

        public Usuario LoginUsuario(Usuario user)
        {
            var retorno = dalUsuario.LoginUsuario(user);

            return retorno;
        }
    }
}