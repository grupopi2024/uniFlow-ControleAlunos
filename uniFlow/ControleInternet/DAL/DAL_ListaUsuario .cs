using ControleInternet.ConexaoBD;
using ControleInternet.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace ControleInternet.DLL
{
    public class DAL_ListaUsuario
    {
        public List<Usuario> ListarUsuario()
        {
            try
            {
                var colecao = ConectaBanco.GetAcessoUsuario();

                var filter = Builders<Usuario>.Filter.Empty;

                var retorno = colecao.Find(filter).ToList();
                string a = retorno[0]._id.ToString();
                return (retorno);

            }
            catch (Exception e)
            {
                throw new Exception($"Erro {e.Message}");
            }
        }
    }
}