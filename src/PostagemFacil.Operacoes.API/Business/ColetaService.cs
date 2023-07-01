using Microsoft.EntityFrameworkCore;
using PostagemFacil.Operacoes.API.Business.DTOs;
using PostagemFacil.Operacoes.API.Data;
using PostagemFacil.Operacoes.API.Data.Models;

namespace PostagemFacil.Operacoes.API.Business
{
    public interface IColetaService
    {
        Task CriarColeta(CriarColetaDTO dto);
        Task<IEnumerable<Coleta>> ObterColetas(int pagina, int itensPorPagina);
    }

    public class ColetaService : IColetaService
    {
        private readonly OperacoesContext _operacoesContext;

        public ColetaService(OperacoesContext operacoesContext)
        {
            _operacoesContext = operacoesContext;
        }

        public async Task CriarColeta(CriarColetaDTO dto)
        {
            var coleta = new Coleta
            {
                SolicitacaoId = dto.SolicitacaoId,
                ResponsavelId = dto.ResponsavelId,
                Observacao = dto.Observacao,
                DataColeta = DateTime.Now,
            };

            await _operacoesContext.Coletas.AddAsync(coleta);
            await _operacoesContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Coleta>> ObterColetas(int pagina, int itensPorPagina)
        {
            return await _operacoesContext.Coletas
                           .Skip((pagina - 1) * itensPorPagina)
                           .Take(itensPorPagina)
                           .ToListAsync();
        }
    }
}
