using ControleInternet.ConexaoBD;
using ControleInternet.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleInternet.DAL
{
    public class DAL_Usuario
    {
        public List<Usuario> ObterUsuario()
        {
            try
            {
                var colecao = ConectaBanco.GetAcessoUsuario();

                var filtro = Builders<Usuario>.Filter.Where(x => x.Flag == "1");
                var result = colecao.Find<Usuario>(filtro).ToList<Usuario>();

                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                };
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public string ExcluirUsuario(UsuarioCPF request)
        {
            try
            {
                var colecao = ConectaBanco.GetAcessoUsuario();
                var filtro = Builders<Usuario>.Filter.Where(bancoDeDados =>
                    bancoDeDados.CPF == request.CPF);

                var usuarioDadosPessoais = colecao.Find(filtro).FirstOrDefault();

                if (usuarioDadosPessoais != null)
                {
                    colecao.DeleteOne(filtro);

                    return string.Empty;
                }
                else
                {
                    return "Houve algum erro";
                }
            }
            catch (Exception e)
            {
                return "Erro ao excluir usuário " + e;
            }
        }

        public Usuario ObterUsuarioCPF(string CPF)
        {
            try
            {
                var colecao = ConectaBanco.GetAcessoUsuario();
                var filtro = Builders<Usuario>.Filter.Where(bancoDeDados =>
                    bancoDeDados.CPF == CPF);

                return colecao.Find(filtro).FirstOrDefault();


            }
            catch (Exception e)
            {
                throw new Exception("Erro ao pesquisar usuário " + e);
            }
        }
    }
}