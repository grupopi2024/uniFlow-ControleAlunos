using ControleInternet.Exceptions.ExcptionsBase;
using System;
using System.Net;
using System.Web.Mvc;

namespace ControleInternet.FiltroDeExceptions
{
    public class FiltroDasExceptions : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ControleDeInternetException)
            {
                TratarControleDeInternetException(context);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void TratarControleDeInternetException(ExceptionContext context)
        {
            var erroDeValidacaoException = context.Exception as ErrosDeValidacao;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //context.Result = new ObjectResult(new RespostaErroJson(erroDeValidacaoException.MensagensDeErro));
        }
    }
}