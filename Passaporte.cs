namespace RelacionamentoPessoaPassaporte
{
    public class Passaporte
    {
        public int PassaporteId { get; set; }
        public string? Numero { get; set; }
        public DateTime DataEmissao { get; set; }
        public int PessoaId { get; set; }
        public Pessoa? Pessoa { get; set; }
    }
}
