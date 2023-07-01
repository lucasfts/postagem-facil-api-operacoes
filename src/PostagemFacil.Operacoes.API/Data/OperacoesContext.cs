using Microsoft.EntityFrameworkCore;
using PostagemFacil.Operacoes.API.Data.Models;

namespace PostagemFacil.Operacoes.API.Data
{
    public class OperacoesContext : DbContext
    {
        public OperacoesContext(DbContextOptions<OperacoesContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Coleta> Coletas { get; set; }
    }
}
