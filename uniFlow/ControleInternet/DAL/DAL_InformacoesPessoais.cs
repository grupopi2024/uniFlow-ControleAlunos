using ControleInternet.ConexaoBD;
using ControleInternet.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace ControleInternet.DAL
{
    public class DAL_InformacoesPessoais
    {
        public string AlteraDadosPessoais(Usuario user)
        {

            var colecao = ConectaBanco.GetAcessoUsuario();
            var filtro = Builders<Usuario>.Filter.Where(bancoDeDados =>
                bancoDeDados._id == user._id);

            var usuarioDadosPessoais = colecao.Find(filtro).FirstOrDefault();

            if (usuarioDadosPessoais != null)
            {
                user.Flag = "1";
                ReplaceOneResult result = colecao.ReplaceOne(filtro, user);

                return "Dados Alterados com sucesso!";
            }
            else
            {
                return "Houve algum erro";
            }
        }

        public Usuario BuscarDadosPessoaisById(UsuarioId user)
        {

            var colecao = ConectaBanco.GetAcessoUsuario();
            var filtro = Builders<Usuario>.Filter.Where(bancoDeDados =>
                bancoDeDados._id == ObjectId.Parse(user._id));

            var usuarioDadosPessoais = colecao.Find(filtro).FirstOrDefault();

            if (usuarioDadosPessoais != null)
            {
                return usuarioDadosPessoais;
            }
            else
            {
                return null;
            }
        }

        public Usuario BuscarDadosPessoaisByEmail(string email)
        {

            var colecao = ConectaBanco.GetAcessoUsuario();
            var filtro = Builders<Usuario>.Filter.Where(bancoDeDados =>
                bancoDeDados.Email == email);

            var usuarioDadosPessoais = colecao.Find(filtro).FirstOrDefault();

            if (usuarioDadosPessoais != null)
            {
                return usuarioDadosPessoais;
            }
            else
            {
                return null;
            }
        }
    }
}