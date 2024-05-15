using ControleInternet.ConexaoBD;
using ControleInternet.Models;
using ControleInternet.Validacao;
using MongoDB.Driver;
using System;

namespace ControleInternet.DLL
{
    public class DAL_CadastroAluno
    {
        public string CadastrarAluno(Aluno user)
        {
            try
            {
                var colecao = ConectaBanco.GetAcessoAluno();

                var verificaEmail = UtilitariosRegex.IsValidEmail(user.Email);

                if (verificaEmail)
                {
                    if (VerificaEmailJaCadastrado(user.Email))
                        return $"Email {user.Email} já cadastrado!";

                    user.Flag = "1";
                    colecao.InsertOne(user);
                    return "Cadastro realizado com sucesso!";
                }

                return "Digite um e-mail válido";
            }
            catch (Exception e)
            {
                return "Erro " + e.Message;
            }
        }

        public string EditarAluno(Aluno aluno)
        {
            try
            {
                var colecao = ConectaBanco.GetAcessoAluno();


                var filtro = Builders<Aluno>.Filter.Where(bancoDeDados => bancoDeDados.RA == aluno.RA);

                var updateAluno = Builders<Aluno>.Update
                    .Set(usuarioAtualizacao => usuarioAtualizacao.NomeCompleto, aluno.NomeCompleto)
                    .Set(usuarioAtualizacao => usuarioAtualizacao.RA, aluno.RA)
                    .Set(usuarioAtualizacao => usuarioAtualizacao.Email, aluno.Email)
                    .Set(usuarioAtualizacao => usuarioAtualizacao.Telefone, aluno.Telefone)
                    .Set(usuarioAtualizacao => usuarioAtualizacao.Serie, aluno.Serie)
                    .Set(usuarioAtualizacao => usuarioAtualizacao.Flag, aluno.Flag);

                colecao.UpdateOne(filtro, updateAluno);

                return "Aluno alterado com sucesso!";


            }
            catch (Exception e)
            {
                return "Erro " + e.Message;
            }
        }

        private bool VerificaEmailJaCadastrado(string email)
        {
            var colecao = ConectaBanco.GetAcessoAluno();

            var filtro = Builders<Aluno>.Filter.Where(bancoDeDados =>
                bancoDeDados.Email == email);

            var emailCadastrado = colecao.Find(filtro).FirstOrDefault();

            if (emailCadastrado != null)
            {
                return true;
            }

            return false;
        }
    }

}