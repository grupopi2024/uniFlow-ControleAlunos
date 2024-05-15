using ControleInternet.DLL;
using ControleInternet.Models;
using System.Collections.Generic;

namespace ControleInternet.BLL
{
    public class BLL_ListarUsuario
    {
        DAL_ListaUsuario dalUsuario = new DAL_ListaUsuario();


        public List<Usuario> ListarUsuario()
        {
            return dalUsuario.ListarUsuario();
        }
    }
}