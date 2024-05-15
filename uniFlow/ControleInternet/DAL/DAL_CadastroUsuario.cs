using ControleInternet.BLL.UseCase;
using ControleInternet.ConexaoBD;
using ControleInternet.Models;
using ControleInternet.Validacao;
using MongoDB.Driver;
using System;

namespace ControleInternet.DLL
{
    public class DAL_CadastroUsuario
    {
        public string CadastrarUsuario(Usuario user)
        {
            try
            {
                var colecao = ConectaBanco.GetAcessoUsuario();

                var verificaEmail = UtilitariosRegex.IsValidEmail(user.Email);

                if (verificaEmail)
                {
                    if (VerificaEmailJaCadastrado(user.Email))
                        return $"Email {user.Email} já cadastrado!";

                    if (VerificaCPFJaCadastrado(user.CPF))
                        return $"CPF {user.CPF} já cadastrado!";

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
        public string CadastroUsuario(Usuario user)
        {
            try
            {
                var colecao = ConectaBanco.GetAcessoUsuario();

                RegistrarUsuarioUseCase validarDados = new RegistrarUsuarioUseCase();

                var verificaEmail = UtilitariosRegex.IsValidEmail(user.Email);

                if (verificaEmail)
                {
                    if (VerificaEmailJaCadastrado(user.Email))
                        return $"Email {user.Email} já cadastrado!";

                    colecao.InsertOne(user);
                    return "Cadastro realizado com sucesso! Faça login para continuar.";
                }

                return "Digite um e-mail válido";
            }
            catch (Exception e)
            {
                return "Erro " + e.Message;
            }
        }

        public string EditarUsuario(Usuario user)
        {
            try
            {
                var colecao = ConectaBanco.GetAcessoUsuario();

                var verificaEmail = UtilitariosRegex.IsValidEmail(user.Email);

                if (verificaEmail)
                {
                    if (VerificaEmailJaCadastradoEditar(user))
                        return $"Email {user.Email} já cadastrado!";

                    var filtro = Builders<Usuario>.Filter.Where(bancoDeDados => bancoDeDados.CPF == user.CPF);

                    var updateUsuario = Builders<Usuario>.Update
                        .Set(usuarioAtualizacao => usuarioAtualizacao.Nome, user.Nome)
                        .Set(usuarioAtualizacao => usuarioAtualizacao.NomeCompleto, user.NomeCompleto)
                        .Set(usuarioAtualizacao => usuarioAtualizacao.CPF, user.CPF)
                        .Set(usuarioAtualizacao => usuarioAtualizacao.Senha, user.Senha)
                        .Set(usuarioAtualizacao => usuarioAtualizacao.Email, user.Email)
                        .Set(usuarioAtualizacao => usuarioAtualizacao.Telefone, user.Telefone)
                        .Set(usuarioAtualizacao => usuarioAtualizacao.Funcao, user.Funcao)
                        .Set(usuarioAtualizacao => usuarioAtualizacao.Flag, user.Flag);

                    colecao.UpdateOne(filtro, updateUsuario);

                    return "Usuário alterado com sucesso!";
                }

                return "Digite um e-mail válido";
            }
            catch (Exception e)
            {
                return "Erro " + e.Message;
            }
        }

        private bool VerificaEmailJaCadastrado(string email)
        {
            var colecao = ConectaBanco.GetAcessoUsuario();

            var filtro = Builders<Usuario>.Filter.Where(bancoDeDados =>
                bancoDeDados.Email == email);

            var emailCadastrado = colecao.Find(filtro).FirstOrDefault();

            if (emailCadastrado != null)
            {
                return true;
            }

            return false;
        }

        private bool VerificaCPFJaCadastrado(string cpf)
        {
            var colecao = ConectaBanco.GetAcessoUsuario();

            var filtro = Builders<Usuario>.Filter.Where(bancoDeDados =>
                bancoDeDados.CPF == cpf);

            var cpfCadastrado = colecao.Find(filtro).FirstOrDefault();

            if (cpfCadastrado != null)
            {
                return true;
            }

            return false;
        }

        private bool VerificaEmailJaCadastradoEditar(Usuario request)
        {
            var colecao = ConectaBanco.GetAcessoUsuario();

            var filtro = Builders<Usuario>.Filter.Where(bancoDeDados =>
                bancoDeDados.Email == request.Email && bancoDeDados.CPF != request.CPF);

            var emailCadastrado = colecao.Find(filtro).FirstOrDefault();

            if (emailCadastrado != null)
            {
                return true;
            }

            return false;
        }
    }
}