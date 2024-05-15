using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleInternet.Models
{
    [BsonIgnoreExtraElements]
    public class Usuario
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string NomeCompleto { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int Funcao { get; set; } = 1; //0- Administrador 1- Monitor
        public string Flag { get; set; } = "1"; //0- Inativo 1- Ativo
    }

    public class UsuarioId
    {
        public string _id { get; set; }
    }

}