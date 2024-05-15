using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ControleInternet.Models
{
    [BsonIgnoreExtraElements]
    public class Aluno
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int RA { get; set; }
        public int Serie { get; set; }
        public string SerieDescricao { get; set; }
        public string Flag { get; set; } = "1"; //0- Inativo 1- Ativo
    }
    public class AlunoId
    {
        public string _id { get; set; }
    }
}

