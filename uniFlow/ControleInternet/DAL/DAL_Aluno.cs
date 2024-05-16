using ControleInternet.ConexaoBD;
using ControleInternet.Models;
using MongoDB.Driver;
using System;
using System.Linq;

namespace ControleInternet.DAL
{
    public class DAL_Aluno
    {
        public string ExcluirAluno(int RA)
        {
            try
            {
                var colecao = ConectaBanco.GetAcessoAluno();
                var filtro = Builders<Aluno>.Filter.Where(bancoDeDados =>
                    bancoDeDados.RA == RA);

                var alunoDadosPessoais = colecao.Find(filtro).FirstOrDefault();

                if (alunoDadosPessoais != null)
                {
                    colecao.DeleteOne(filtro);

                    return string.Empty;
                }
                else
                {
                    return "Houve algum erro";
                }
            }
            catch (Exception e)
            {
                return "Erro ao excluir aluno " + e;
            }
        }

        public Aluno ObterAlunoRA(int RA)
        {
            try
            {
                var colecao = ConectaBanco.GetAcessoAluno();
                var filtro = Builders<Aluno>.Filter.Where(bancoDeDados =>
                    bancoDeDados.RA == RA);

                return colecao.Find(filtro).FirstOrDefault();


            }
            catch (Exception e)
            {
                throw new Exception("Erro ao pesquisar aluno " + e);
            }
        }

        public string EntradaAluno(EntradaSaidaAluno request)
        {
            try
            {
                var dataAtual = DateTime.Now;

                var colecaoFrequencia = ConectaBanco.GetAcessoControleAcesso();

                var dataAtualMeiaNoite = new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, 0, 0, 0);

                var filtroFrequencia = Builders<ControleAcesso>.Filter.And(
                    Builders<ControleAcesso>.Filter.Eq(x => x.RA, request.RA),
                    Builders<ControleAcesso>.Filter.Gte(x => x.DataCadastro, dataAtualMeiaNoite.ToString())
                );

                var frequenciaBD = colecaoFrequencia.Find(filtroFrequencia).FirstOrDefault();


                if (frequenciaBD != null)
                {
                    var atualizarFrequencia = Builders<ControleAcesso>.Update
                            .Set(entradaAluno => entradaAluno.DataHoraEntrada, DateTime.Now)
                            .Set(entradaAluno => entradaAluno.CPFEntrada, request.CPF);

                    colecaoFrequencia.UpdateOne(filtroFrequencia, atualizarFrequencia);
                }
                else
                {
                    var cadastrarFrequencia = new ControleAcesso()
                    {
                        CPFEntrada = request.CPF,
                        RA = request.RA,
                        DataCadastro = dataAtualMeiaNoite.ToString(),
                        DataHoraEntrada = DateTime.Now,
                    };

                    colecaoFrequencia.InsertOne(cadastrarFrequencia);
                }

                return "Entrada realizada com sucesso";
            }
            catch (Exception e)
            {

                throw new Exception($"Erro ao dar entrada no aluno: {e.Message}");
            }
        }

        public string SaidaAluno(EntradaSaidaAluno request)
        {
            try
            {
                var dataAtual = DateTime.Now;

                var colecaoFrequencia = ConectaBanco.GetAcessoControleAcesso();

                var dataAtualMeiaNoite = new DateTime(dataAtual.Year, dataAtual.Month, dataAtual.Day, 0, 0, 0);

                var filtroFrequencia = Builders<ControleAcesso>.Filter.And(
                    Builders<ControleAcesso>.Filter.Eq(x => x.RA, request.RA),
                    Builders<ControleAcesso>.Filter.Gte(x => x.DataCadastro, dataAtualMeiaNoite.ToString())
                );

                var frequenciaBD = colecaoFrequencia.Find(filtroFrequencia).FirstOrDefault();


                if (frequenciaBD != null)
                {
                    var atualizarFrequencia = Builders<ControleAcesso>.Update
                            .Set(entradaAluno => entradaAluno.DataHoraSaida, DateTime.Now)
                            .Set(entradaAluno => entradaAluno.CPFSaida, request.CPF);

                    colecaoFrequencia.UpdateOne(filtroFrequencia, atualizarFrequencia);

                    return "Saida realizada com sucesso";
                }
                else
                {
                    return "Não é possível fazer a saida de um aluno que não entrou na escola";
                }

            }
            catch (Exception e)
            {

                throw new Exception($"Erro ao dar entrada no aluno: {e.Message}");
            }
        }
    }
}