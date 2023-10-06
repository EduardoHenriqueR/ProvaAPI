using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDataContext : DbContext
{
     public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Folha> Folhas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Como popular uma base de dados utilizando EF no método
        //OnModelCreating, quero dados reais de produto, com os seguintes
        //atributos


        base.OnModelCreating(modelBuilder);
    }
}