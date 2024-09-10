namespace RelacionamentoPessoaPassaporte
{
    public class Pessoa
    {
        public int PessoaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public Passaporte Passaporte { get; set; } = new Passaporte();
    }
}
