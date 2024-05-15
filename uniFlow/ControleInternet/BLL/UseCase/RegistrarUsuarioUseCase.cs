using ControleInternet.Exceptions.ExcptionsBase;
using ControleInternet.Requisicoes;
using ControleInternet.Validacao;
using System.Linq;
using System.Threading.Tasks;

namespace ControleInternet.BLL.UseCase
{
    public class RegistrarUsuarioUseCase : IRegistrarUsuarioUseCase
    {
        public async Task<string> Executar(RequisicaoRegistrarUsuarioJson requisicao)
        {
            await Validar(requisicao);

            return "Cadastro realizado com sucesso";
        }

        private async Task Validar(RequisicaoRegistrarUsuarioJson requisicao)
        {
            var validator = new ValidacaoRegistrarUsuario();
            var resultado = validator.Validate(requisicao);

            if (!resultado.IsValid)
            {
                var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrosDeValidacao(mensagensDeErro);
            }
        }
    }
}