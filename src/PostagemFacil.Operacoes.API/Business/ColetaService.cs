using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using PostagemFacil.Operacoes.API.Business.DTOs;
using PostagemFacil.Operacoes.API.Business.Events;
using PostagemFacil.Operacoes.API.Data;
using PostagemFacil.Operacoes.API.Data.Models;
using System.Text.Json;

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
        private readonly ServiceBusClient _serviceBusClient;

        public ColetaService(OperacoesContext operacoesContext, ServiceBusClient serviceBusClient)
        {
            _operacoesContext = operacoesContext;
            _serviceBusClient = serviceBusClient;
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

            var evento = new SolicitacaoColetada { SolicitacaoId = coleta.SolicitacaoId };
            var mensagem = new ServiceBusMessage(JsonSerializer.Serialize(evento));
            var _serviceBusSender = _serviceBusClient.CreateSender("coletas");
            await _serviceBusSender.SendMessageAsync(mensagem);
            await _serviceBusSender.DisposeAsync();
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
