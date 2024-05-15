using ControleInternet.Requisicoes;
using FluentValidation;

namespace ControleInternet.Validacao
{
    public class ValidarDadosRegistroUsuario : AbstractValidator<RequisicaoRegistrarUsuarioJson>
    {

        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
    }

}
