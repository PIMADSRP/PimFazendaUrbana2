using Microsoft.AspNetCore.Mvc;
using PIMFazendaUrbanaLib;
using PIMFazendaUrbanaAPI.DTOs;
using AutoMapper;

namespace PIMFazendaUrbanaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompraController : ControllerBase
    {
        private readonly ICompraService _compraService;
        private readonly IMapper _mapper; // Adiciona o AutoMapper

        // O controlador utiliza a interface ICompraService para acessar as operações de compra
        public CompraController(ICompraService compraService, IMapper mapper)
        {
            _compraService = compraService;
            _mapper = mapper; // Inicializa o AutoMapper
        }

        // Método para cadastrar um compra
        [HttpPost("cadastrar")]
        public IActionResult CadastrarCompra([FromBody] PedidoCompraDTO pedidoCompraDto)
        {
            try
            {
                var pedidoCompra = _mapper.Map<PedidoCompra>(pedidoCompraDto); // Mapeia CompraDTO para Compra
                _compraService.CadastrarPedidoCompra(pedidoCompra); // Chama o serviço para cadastrar o compra
                return Ok(new { message = "Compra cadastrada com sucesso." });
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

        // Método para listar compras
        [HttpGet("listar")]
        public IActionResult ListarPedidosCompra()
        {
            try
            {
                var pedidosCompra = _compraService.ListarPedidosCompra();
                var pedidosCompraDto = _mapper.Map<List<PedidoCompraDTO>>(pedidosCompra); // Mapeia Compra para PedidoCompraDTO
                return Ok(pedidosCompraDto);
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao listar compras: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

        [HttpGet("filtrados")]
        public ActionResult<List<PedidoCompraDTO>> ListarComprasFiltradas(string search)
        {
            try
            {
                var pedidosCompra = _compraService.ListarComprasComFiltros(search);
                var pedidosCompraDto = _mapper.Map<List<PedidoCompraDTO>>(pedidosCompra); // Mapeia Compra para CompraDTO
                return Ok(pedidosCompraDto); // Retorna a lista de compras filtradss como resposta
            }
            catch (Exception ex)
            {
                // Log detalhado do erro
                Console.WriteLine($"Erro ao listar compras: {ex.Message}\n{ex.StackTrace}");
                return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
            }
        }

    }
}
