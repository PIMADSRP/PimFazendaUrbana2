namespace PIMFazendaUrbanaRadzen.Services
{
    public class CompraApiService<T>
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpointUrl;

        public CompraApiService(HttpClient httpClient, string endpointUrl)
        {
            _httpClient = httpClient;
            _endpointUrl = endpointUrl;
        }

        public async Task<List<T>> GetComprasFiltradasAsync(string search)
        {
            try
            {
                Console.WriteLine($"Chamando API em: {_endpointUrl}/filtrados?search={Uri.EscapeDataString(search)}");
                return await _httpClient.GetFromJsonAsync<List<T>>($"{_endpointUrl}/filtrados?search={Uri.EscapeDataString(search)}");
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Erro de requisição: {httpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao chamar API: {ex.Message}");
                throw;
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                Console.WriteLine($"Chamando API em: {_endpointUrl}/listar");

                return await _httpClient.GetFromJsonAsync<List<T>>($"{_endpointUrl}/listar");
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Erro de requisição: {httpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao chamar API: {ex.Message}");
                throw;
            }
        }

        public async Task<HttpResponseMessage> CreateAsync(T entity)
        {
            return await _httpClient.PostAsJsonAsync($"{_endpointUrl}/cadastrar", entity);
        }

        public async Task<int> GetUltimoIdPedidoCompra()
        {
            try
            {
                Console.WriteLine($"Chamando API em: {_endpointUrl}/ultimoid-pedido");

                return await _httpClient.GetFromJsonAsync<int>($"{_endpointUrl}/ultimoid-pedido");
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Erro de requisição: {httpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao chamar API: {ex.Message}");
                throw;
            }
        }

        public async Task<int> GetUltimoIdPedidoCompraItem()
        {
            try
            {
                Console.WriteLine($"Chamando API em: {_endpointUrl}/ultimoid-item");

                return await _httpClient.GetFromJsonAsync<int>($"{_endpointUrl}/ultimoid-item");
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Erro de requisição: {httpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao chamar API: {ex.Message}");
                throw;
            }
        }

    }
}
