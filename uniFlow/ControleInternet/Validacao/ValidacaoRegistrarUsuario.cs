using ControleInternet.Exceptions;
using ControleInternet.Requisicoes;
using FluentValidation;


namespace ControleInternet.Validacao
{
    public class ValidacaoRegistrarUsuario : AbstractValidator<RequisicaoRegistrarUsuarioJson>
    {
        public ValidacaoRegistrarUsuario()
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage(ResourceMensagensDeErro.EMAIL_INVALIDO);
            When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
            {
                RuleFor(c => c.Email).EmailAddress().WithMessage(ResourceMensagensDeErro.EMAIL_INVALIDO);
            });
        }
    }
}