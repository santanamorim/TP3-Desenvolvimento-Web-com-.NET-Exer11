using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RelacionamentoPessoaPassaporte;
public class PessoaContext : DbContext
{
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Passaporte> Passaportes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=pessoas.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pessoa>()
            .HasOne(p => p.Passaporte)
            .WithOne(p => p.Pessoa)
            .HasForeignKey<Passaporte>(p => p.PessoaId);
    }
}

