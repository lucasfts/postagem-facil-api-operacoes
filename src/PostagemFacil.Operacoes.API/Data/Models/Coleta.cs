namespace PostagemFacil.Operacoes.API.Data.Models
{
    public class Coleta
    {
        public int Id { get; set; }
        public int SolicitacaoId { get; set; }
        public int ResponsavelId { get; set; }
        public DateTime DataColeta { get; set; }
        public string Observacao { get; set; }
    }
}
