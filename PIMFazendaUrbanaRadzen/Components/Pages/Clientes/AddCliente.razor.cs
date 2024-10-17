using Microsoft.AspNetCore.Components;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaRadzen.Services;
using Radzen;

namespace PIMFazendaUrbanaRadzen.Components.Pages.Clientes
{
    public partial class AddCliente
    {
        [Inject]
        public ApiService<ClienteDTO> ClienteApiService { get; set; }

        [Inject]
        public DialogService DialogService { get; set; } // Inject DialogService

        protected ClienteDTO cliente;
        protected bool errorVisible;

        protected List<string> estados = new List<string>
        {
            "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA",
            "MT", "MS", "MG", "PA", "PB", "PE", "PI", "PR", "RJ", "RN",
            "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        };


        protected override async Task OnInitializedAsync()
        {
            cliente = new ClienteDTO
            {
                Endereco = new EnderecoDTO(), // Inicializa o Endereco
                Telefone = new TelefoneDTO()  // Inicializa o Telefone
            };
        }

        protected async Task FormSubmit()
        {
            try
            {
                await ClienteApiService.CreateAsync(cliente); // Chama o serviço para criar o cliente
                                                              // Redirecionar ou exibir mensagem de sucesso
            }
            catch (Exception ex)
            {
                errorVisible = true; // Mostra mensagem de erro
                Console.WriteLine($"Erro ao cadastrar cliente: {ex.Message}");
            }
        }

        protected async Task CancelButtonClick()
        {
            // Lógica para cancelar a ação, redirecionar ou fechar o formulário
            DialogService.Close(null);
        }
    }
}
