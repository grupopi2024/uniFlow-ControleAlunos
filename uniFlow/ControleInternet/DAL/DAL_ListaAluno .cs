using ControleInternet.ConexaoBD;
using ControleInternet.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleInternet.DLL
{
    public class DAL_ListaAluno
    {
        public List<Aluno> ListarAluno()
        {
            try
            {
                var colecao = ConectaBanco.GetAcessoAluno();

                var filter = Builders<Aluno>.Filter.Empty;

                var retorno = colecao.Find(filter).ToList();
                string a = retorno[0]._id.ToString();
                var retornoOrdenado = retorno.OrderBy(o => o.Serie).ToList();
                for (int i = 0; i < retornoOrdenado.Count; i++)
                {
                    retornoOrdenado[i].SerieDescricao = VerificaSerie(retornoOrdenado[i].Serie);
                }

                return (retornoOrdenado);

            }
            catch (Exception e)
            {
                throw new Exception($"Erro {e.Message}");
            }
        }

        public List<AlunoControleAcesso> ListarControleAcesso()
        {
            try
            {
                var retorno = new List<AlunoControleAcesso>();

                var colecaoAluno = ConectaBanco.GetAcessoAluno();
                var colecaoFrequencia = ConectaBanco.GetAcessoControleAcesso();

                var filter = Builders<Aluno>.Filter.Empty;

                var alunos = colecaoAluno.Find(filter).ToList();

                var dataAtual = DateTime.Today;

                foreach (var aluno in alunos)
                {
                    var dataAtualMeiaNoite = new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, 0, 0, 0);

                    var filtroFrequencia = Builders<ControleAcesso>.Filter.And(
                        Builders<ControleAcesso>.Filter.Eq(x => x.RA, aluno.RA),
                        Builders<ControleAcesso>.Filter.Eq(x => x.DataCadastro, dataAtualMeiaNoite.ToString())
                    );


                    var frequencia = colecaoFrequencia.Find(filtroFrequencia).FirstOrDefault() ?? new ControleAcesso();

                    aluno.SerieDescricao = VerificaSerie(aluno.Serie);

                    var instanciaAluno = new AlunoControleAcesso()
                    {
                        NomeCompleto = aluno.NomeCompleto,
                        RA = aluno.RA,
                        Serie = aluno.Serie,
                        SerieDescricao = aluno.SerieDescricao,
                        DataHoraEntrada = frequencia.DataHoraEntrada.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("pt-BR")),
                        DataHoraSaida = frequencia.DataHoraSaida.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("pt-BR")),
                        CPFEntrada = frequencia.CPFEntrada ?? string.Empty,
                        CPFSaida = frequencia.CPFSaida ?? string.Empty,
                    };

                    retorno.Add(instanciaAluno);
                }

                var retornoOrdenado = retorno.OrderBy(o => o.SerieDescricao).ToList();

                return (retornoOrdenado);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro {e.Message}");
            }
        }

        public List<AlunoControleAcesso> ListarControleFrequencia(DateTime data)
        {
            try
            {
                var retorno = new List<AlunoControleAcesso>();

                var colecaoAluno = ConectaBanco.GetAcessoAluno();
                var colecaoFrequencia = ConectaBanco.GetAcessoControleAcesso();
                var colecaoUsuario = ConectaBanco.GetAcessoUsuario();

                var dataPesquisa = new DateTime(data.Year, data.Month, data.Day, 0, 0, 0);

                var filtroFrequencia = Builders<ControleAcesso>.Filter.Eq(x => x.DataCadastro, dataPesquisa.ToString());

                var frequencias = colecaoFrequencia.Find(filtroFrequencia).ToList();

                foreach (var frequencia in frequencias)
                {
                    var filtroAluno = Builders<Aluno>.Filter.Where(a => a.RA==frequencia.RA);

                    var aluno = colecaoAluno.Find(filtroAluno).FirstOrDefault();
                    
                    var filtroUsuarioEntrada = Builders<Usuario>.Filter.Where(u => u.CPF==frequencia.CPFEntrada);

                    var usuarioEntrada = colecaoUsuario.Find(filtroUsuarioEntrada).FirstOrDefault();

                    var filtroUsuarioSaida = Builders<Usuario>.Filter.Where(u => u.CPF==frequencia.CPFSaida);

                    var usuarioSaida = colecaoUsuario.Find(filtroUsuarioSaida).FirstOrDefault();
                   
                    aluno.SerieDescricao = VerificaSerie(aluno.Serie);

                    var instanciaFrequencia = new AlunoControleAcesso()
                    {
                        NomeCompleto = aluno.NomeCompleto,
                        RA = aluno.RA,
                        Serie = aluno.Serie,
                        SerieDescricao = aluno.SerieDescricao,
                        //DataHoraEntrada = frequencia.DataHoraEntrada.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("pt-BR")),
                        DataHoraEntrada = string.IsNullOrEmpty(frequencia.DataHoraEntrada.ToString()) ? "1753-01-01 00:00:00" : frequencia.DataHoraEntrada.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("pt-BR")),
                        //DataHoraSaida = frequencia.DataHoraSaida.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("pt-BR")),
                        DataHoraSaida = string.IsNullOrEmpty(frequencia.DataHoraSaida.ToString()) ? "1753-01-01 00:00:00" : frequencia.DataHoraSaida.ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss", new System.Globalization.CultureInfo("pt-BR")),
                        CPFEntrada = usuarioEntrada?.CPF ?? string.Empty,
                        CPFSaida = usuarioSaida?.CPF ?? string.Empty,
                    };

                    retorno.Add(instanciaFrequencia);
                }

                var retornoOrdenado = retorno.OrderBy(o => o.SerieDescricao).ToList();

                return (retornoOrdenado);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro {e.Message}");
            }
        }

        private string VerificaSerie(int serie)
        {
            switch (serie)
            {
                case 0:
                    return "1ª Série";

                case 1:
                    return "2ª Série";

                case 2:
                    return "3ª Série";

                default:
                    return "1ª Série";

            }
        }
    }
}