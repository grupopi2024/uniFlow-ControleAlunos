using ControleInternet.Models;
using MongoDB.Driver;
using System;
using System.Configuration;

namespace ControleInternet.ConexaoBD
{
    public class ConectaBanco
    {
        public static IMongoCollection<Usuario> GetAcessoUsuario()
        {
            string connectionUri = ConfigurationManager.ConnectionStrings["conexaoMongoDB"].ConnectionString;
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            var client = new MongoClient(settings);

            try
            {
                var db = client.GetDatabase("uniflowdb");
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
                IMongoCollection<Usuario> colecao = db.GetCollection<Usuario>("usuario");

                return colecao;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static IMongoCollection<Aluno> GetAcessoAluno()
        {
            string connectionUri = ConfigurationManager.ConnectionStrings["conexaoMongoDB"].ConnectionString;
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            var client = new MongoClient(settings);

            try
            {
                var db = client.GetDatabase("uniflowdb");
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
                IMongoCollection<Aluno> colecao = db.GetCollection<Aluno>("aluno");

                return colecao;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static IMongoCollection<ControleAcesso> GetAcessoControleAcesso()
        {
            string connectionUri = ConfigurationManager.ConnectionStrings["conexaoMongoDB"].ConnectionString;
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            var client = new MongoClient(settings);

            try
            {
                var db = client.GetDatabase("uniflowdb");
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
                IMongoCollection<ControleAcesso> colecao = db.GetCollection<ControleAcesso>("controleacesso");

                return colecao;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}

//< add name = "conexaoMongoDB" connectionString = "mongodb+srv://uniflow:uniflow@cluster0.tbdzh2s.mongodb.net/" />