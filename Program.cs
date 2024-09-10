using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RelacionamentoPessoaPassaporte;

class Program
{
    static void Main(string[] args)
    {
        using (var db = new PessoaContext())
        {
            db.Database.EnsureCreated();

            int opcao = 0;
            do
            {
                Console.WriteLine("\n1. Criar Pessoa");
                Console.WriteLine("2. Criar Passaporte e Associar à Pessoa");
                Console.WriteLine("3. Listar Pessoas e seus Passaportes");
                Console.WriteLine("4. Sair");
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        CriarPessoa(db);
                        break;
                    case 2:
                        CriarPassaporte(db);
                        break;
                    case 3:
                        ListarPessoasComPassaportes(db);
                        break;
                }
            } while (opcao != 4);
        }
    }

    static void CriarPessoa(PessoaContext db)
    {
        Console.Write("Digite o nome da pessoa: ");
        string nome = Console.ReadLine();

        var pessoa = new Pessoa { Nome = nome };
        db.Pessoas.Add(pessoa);
        db.SaveChanges();
        Console.WriteLine("Pessoa criada com sucesso!");
    }

    static void CriarPassaporte(PessoaContext db)
    {
        Console.Write("Digite o ID da pessoa para associar o passaporte: ");
        int pessoaId = int.Parse(Console.ReadLine());
        var pessoa = db.Pessoas.Find(pessoaId);

        if (pessoa != null)
        {
            Console.Write("Digite o número do passaporte: ");
            string numero = Console.ReadLine();
            Console.Write("Digite a data de emissão do passaporte (yyyy-MM-dd): ");
            DateTime dataEmissao = DateTime.Parse(Console.ReadLine());

            var passaporte = new Passaporte
            {
                Numero = numero,
                DataEmissao = dataEmissao,
                PessoaId = pessoa.PessoaId
            };

            db.Passaportes.Add(passaporte);
            db.SaveChanges();
            Console.WriteLine("Passaporte criado e associado à pessoa com sucesso!");
        }
        else
        {
            Console.WriteLine("Pessoa não encontrada.");
        }
    }

    static void ListarPessoasComPassaportes(PessoaContext db)
    {
        var pessoas = db.Pessoas.Include(p => p.Passaporte).ToList();

        Console.WriteLine("\nLista de Pessoas e seus Passaportes:");
        foreach (var pessoa in pessoas)
        {
            Console.WriteLine($"Pessoa: {pessoa.Nome} (ID: {pessoa.PessoaId})");

            if (pessoa.Passaporte != null)
            {
                Console.WriteLine($"  Passaporte: {pessoa.Passaporte.Numero}, Emissão: {pessoa.Passaporte.DataEmissao.ToShortDateString()}");
            }
            else
            {
                Console.WriteLine("  Sem passaporte associado.");
            }
        }
    }
}
