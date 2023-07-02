using Microsoft.AspNetCore.Mvc;
using PostagemFacil.Operacoes.API.Business;
using PostagemFacil.Operacoes.API.Business.DTOs;

namespace PostagemFacil.Operacoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColetasController : ControllerBase
    {
        private readonly IColetaService _service;

        public ColetasController(IColetaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObterColetas(int pagina = 1, int itensPorPagina = 10)
        {
            var solicitacoes = await _service.ObterColetas(pagina, itensPorPagina);
            return Ok(solicitacoes);
        }

        [HttpPost]
        public async Task<IActionResult> CriarColeta(CriarColetaDTO dto)
        {
            await _service.CriarColeta(dto);
            return Ok();
        }
    }
}
