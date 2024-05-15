using System.Collections.Generic;

namespace ControleInternet.Exceptions.ExcptionsBase
{

    public class ErrosDeValidacao : ControleDeInternetException
    {
        public List<string> MensagensDeErro { get; set; }
        public ErrosDeValidacao(List<string> mensagemDeErro) : base(string.Empty)
        {
            MensagensDeErro = mensagemDeErro;
        }
    }
}

