using Microsoft.AspNetCore.Mvc;
using PIMFazendaUrbanaLib;
using PIMFazendaUrbanaAPI.DTOs;
using AutoMapper;

namespace PIMFazendaUrbanaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly IVendaService _vendaService;
        private readonly IMapper _mapper; // Adiciona o AutoMapper

        // O controlador utiliza a interface IVendaService para acessar as operações de venda
        public VendaController(IVendaService vendaService, IMapper mapper)
        {
            _vendaService = vendaService;
            _mapper = mapper; // Inicializa o AutoMapper
        }

        [HttpGet("filtrados")]
        public ActionResult<List<PedidoVendaItemDTO>> ListarPedidoVendaItensComFiltros(string search)
        {
            try
            {
                var pedidoVendaItens = _vendaService.ListarPedidoVendaItensComFiltros(search);
                var pedidoVendaItensDto = _mapper.Map<List<PedidoVendaItemDTO>>(pedidoVendaItens); // Mapeia PedidoVendaItem para PedidoVendaItemDTO
                return Ok(pedidoVendaItensDto); // Retorna a lista de compras filtradas como resposta
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao listar vendas: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para listar vendas
        [HttpGet("listar")]
        public IActionResult ListarRegistrosDeVenda()
        {
            try
            {
                var pedidoVendaItens = _vendaService.ListarRegistrosDeVenda();
                var pedidoVendaItensDto = _mapper.Map<List<PedidoVendaItemDTO>>(pedidoVendaItens); // Mapeia PedidoVendaItem para PedidoVendaItemDTO
                return Ok(pedidoVendaItensDto);
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao listar vendas: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para cadastrar um venda
        [HttpPost("cadastrar")]
        public IActionResult CadastrarVenda([FromBody] PedidoVendaDTO pedidoVendaDto)
        {
            try
            {
                var pedidoVenda = _mapper.Map<PedidoVenda>(pedidoVendaDto); // Mapeia PedidoVendaDTO para PedidoVenda
                _vendaService.CadastrarPedidoVenda(pedidoVenda); // Chama o serviço para cadastrar o venda
                return Ok(new { message = "Venda cadastrada com sucesso." });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors }); // Retorna erros de validação
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para obter o último ID de pedido de venda
        [HttpGet("ultimoid-pedido")]
        public IActionResult ObterUltimoIdPedidoVenda()
        {
            try
            {
                var ultimoId = _vendaService.ObterUltimoIdPedidoVenda();
                return Ok(ultimoId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        // Método para obter o último ID de item de venda
        [HttpGet("ultimoid-item")]
        public IActionResult ObterUltimoIdPedidoVendaItem()
        {
            try
            {
                var ultimoId = _vendaService.ObterUltimoIdPedidoVendaItem();
                return Ok(ultimoId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

    }
}
