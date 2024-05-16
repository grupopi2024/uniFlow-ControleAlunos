using ControleInternet.DLL;
using ControleInternet.Models;
using System;
using System.Collections.Generic;

namespace ControleInternet.BLL
{
    public class BLL_ListarAluno
    {
        DAL_ListaAluno dalAluno = new DAL_ListaAluno();


        public List<Aluno> ListarAluno()
        {
            return dalAluno.ListarAluno();
        }

        public List<AlunoControleAcesso> ListarControleAcesso()
        {
            return dalAluno.ListarControleAcesso();
        }

        public List<AlunoControleAcesso> ListarControleFrequencia(DateTime data)
        {
            return dalAluno.ListarControleFrequencia(data);
        }
    }
}