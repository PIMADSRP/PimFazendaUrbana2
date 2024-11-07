using PIMFazendaUrbanaLib;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace PIMFazendaUrbanaRadzen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CepController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        // Injetando HttpClient via dependência
        public CepController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://viacep.com.br/ws/");
        }

        [HttpGet("{cep}")]
        public async Task<ActionResult<EnderecoViaCep>> RetornarEndereco(string cep)
        {
            var endereco = new EnderecoViaCep();

            try
            {
                // Requisição assíncrona para obter o JSON
                var json = await _httpClient.GetStringAsync($"{cep}/json");

                // Desserializa o JSON na classe Endereco
                endereco = JsonConvert.DeserializeObject<EnderecoViaCep>(json);

                // Se a resposta for inválida ou erro, retorna um resultado de erro
                if (endereco == null || endereco.erro)
                {
                    return NotFound(new { message = "CEP não encontrado." });
                }

                return Ok(endereco);
            }
            catch (HttpRequestException)
            {
                // Caso a requisição falhe (por exemplo, erro de rede)
                return StatusCode(500, new { message = "Erro ao acessar o serviço de CEP." });
            }
            catch (JsonException)
            {
                // Caso haja um erro na desserialização
                return StatusCode(500, new { message = "Erro ao processar a resposta do serviço de CEP." });
            }
            catch (Exception ex)
            {
                // Captura exceções inesperadas
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
