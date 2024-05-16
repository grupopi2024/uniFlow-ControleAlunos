using MongoDB.Bson;
using System;

namespace ControleInternet.Models
{
    public class ControleAcesso
    {
        public ObjectId _id { get; set; }
        public int RA { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime DataHoraSaida { get; set; }
        public string CPFEntrada { get; set; }
        public string CPFSaida { get; set; }
        public string DataCadastro { get; set; }
    }
}