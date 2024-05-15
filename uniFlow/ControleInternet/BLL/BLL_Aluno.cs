using ControleInternet.DAL;
using ControleInternet.Models;

namespace ControleInternet.BLL
{
    public class BLL_Aluno
    {
        DAL_Aluno dalAluno = new DAL_Aluno();
        public string ExcluirAluno(int RA)
        {
            var retorno = dalAluno.ExcluirAluno(RA);

            return retorno;
        }

        public Aluno ObterAlunoRA(int RA)
        {
            var retorno = dalAluno.ObterAlunoRA(RA);

            return retorno;
        }

        public string EntradaAluno(EntradaSaidaAluno request)
        {
            var retorno = dalAluno.EntradaAluno(request);

            return retorno;
        }

        public string SaidaAluno(EntradaSaidaAluno request)
        {
            var retorno = dalAluno.SaidaAluno(request);

            return retorno;
        }
    }
}