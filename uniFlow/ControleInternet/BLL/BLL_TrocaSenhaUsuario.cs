using ControleInternet.DAL;
using ControleInternet.Models;

namespace ControleInternet.BLL
{
    public class BLL_TrocaSenhaUsuario
    {
        DAL_TrocaSenhaUsuario dalTrocaSenhaUsuario = new DAL_TrocaSenhaUsuario();
        public string TrocaSenhaUsuario(TrocaSenhaUsuario user)
        {

            (var dadosInformadosCorretos, var dadosQueFaltam) = ValidarDadosTrocaSenha(user);

            if (!dadosInformadosCorretos)
            {
                return dadosQueFaltam;
            };

            var retorno = dalTrocaSenhaUsuario.TrocaSenhaUsuario(user);

            return retorno;
        }

        private (bool, string) ValidarDadosTrocaSenha(TrocaSenhaUsuario user)
        {
            //Nessa função valido os dados informados pelo usuário para troca de senha
            //Caso esteja faltando alguma informação
            //retorno FALSE como resultado e informo qual campo está faltando
            //Caso todos os dados sejam informados corretamente
            //retorno TRUE e uma string vazia no campo que falta

            if (string.IsNullOrEmpty(user.Email))
            {
                return (false, "É necessário informar o e-mail");
            }
            else
            if (string.IsNullOrEmpty(user.SenhaAtual))
            {
                return (false, "É necessário informar a senha atual");
            }
            else
            if (string.IsNullOrEmpty(user.NovaSenha))
            {
                return (false, "É necessário informar a nova senha");
            };

            return (true, "");
        }
    }
}