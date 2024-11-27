using PIMFazendaUrbanaAPI.DTOs;

namespace PIMFazendaUrbanaRadzen.Services
{
    public class ExportacaoApiService<T>
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpointUrl;

        public ExportacaoApiService(HttpClient httpClient, string endpointUrl)
        {
            _httpClient = httpClient;
            _endpointUrl = endpointUrl;
        }

        public async Task<byte[]> ExportarAsync(IEnumerable<T> dados, string formato, string nomeArquivo)
        {
            try
            {
                //Console.WriteLine("dados recebidos: " + Newtonsoft.Json.JsonConvert.SerializeObject(dados));

                var request = new ExportacaoRequestDTO
                {
                    Dados = dados.Cast<object>().ToList(),
                    Formato = formato,
                    NomeArquivo = nomeArquivo
                };

                //Console.WriteLine("request: " + Newtonsoft.Json.JsonConvert.SerializeObject(request));

                var response = await _httpClient.PostAsJsonAsync($"{_endpointUrl}/exportar", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Erro da API: {response.StatusCode} - {errorContent}");
                    throw new Exception($"Erro ao exportar: {errorContent}");
                }

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao exportar: {errorMessage}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha na comunicação com a API de exportação: {ex.Message}");
            }
        }
    }
}
