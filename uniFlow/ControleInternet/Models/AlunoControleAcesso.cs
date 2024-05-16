namespace ControleInternet.Models
{
    public class AlunoControleAcesso
    {
        public string NomeCompleto { get; set; }
        public int RA { get; set; }
        public int Serie { get; set; }
        public string SerieDescricao { get; set; }
        public string DataHoraEntrada { get; set; }
        public string DataHoraSaida { get; set; }
        public string CPFEntrada { get; set; }
        public string UsuarioEntrada { get; set; }
        public string CPFSaida { get; set; }
        public string UsuarioSaida { get; set; }
    }
}