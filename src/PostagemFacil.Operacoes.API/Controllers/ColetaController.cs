using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostagemFacil.Operacoes.API.Business;
using PostagemFacil.Operacoes.API.Business.DTOs;
using PostagemFacil.Operacoes.API.Data.Models;

namespace PostagemFacil.Operacoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColetaController : ControllerBase
    {
        private readonly IColetaService _service;

        public ColetaController(IColetaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObterSolicitacoes(int pagina = 1, int itensPorPagina = 10)
        {
            var solicitacoes = await _service.ObterColetas(pagina, itensPorPagina);
            return Ok(solicitacoes);
        }

        [HttpPost]
        public async Task<IActionResult> CriarSolicitacao(CriarColetaDTO dto)
        {
            await _service.CriarColeta(dto);
            return Ok();
        }
    }
}
