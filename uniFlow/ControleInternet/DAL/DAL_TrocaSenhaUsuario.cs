using ControleInternet.ConexaoBD;
using ControleInternet.Models;
using MongoDB.Driver;
using System.Linq;

namespace ControleInternet.DAL
{
    public class DAL_TrocaSenhaUsuario
    {
        public string TrocaSenhaUsuario(TrocaSenhaUsuario user)
        {
            (var verificaSenhaAtual, var usuarioRetornoVerificaSenha) = VerificacaoSenhaAtual(user.Email, user.SenhaAtual);

            if (!verificaSenhaAtual)
                return "Senha atual incorreta!";


            var colecao = ConectaBanco.GetAcessoUsuario();
            var filtro = Builders<Usuario>.Filter.Where(bancoDeDados =>
                bancoDeDados._id == usuarioRetornoVerificaSenha._id);

            var usuarioTrocaSenha = colecao.Find(filtro).FirstOrDefault();
            if (usuarioTrocaSenha != null)
            {
                usuarioTrocaSenha.Senha = user.NovaSenha;
                ReplaceOneResult result = colecao.ReplaceOne(filtro, usuarioTrocaSenha);

                return "Senha alterada com sucesso";
            }
            else
            {
                return "Houve algum erro";
            }
        }

        private (bool, Usuario) VerificacaoSenhaAtual(string email, string senhaAtual)
        {
            var colecao = ConectaBanco.GetAcessoUsuario();
            var filtro = Builders<Usuario>.Filter.Where(bancoDeDados =>
                bancoDeDados.Email == email && bancoDeDados.Senha == senhaAtual);
            var usuario = colecao.Find(filtro).FirstOrDefault();

            if (usuario == null)
            {
                return (false, usuario);
            }

            return (true, usuario);
        }
    }
}