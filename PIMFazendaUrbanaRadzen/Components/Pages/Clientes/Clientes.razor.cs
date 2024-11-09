using Microsoft.AspNetCore.Components;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaRadzen.Services;
using Radzen.Blazor;

namespace PIMFazendaUrbanaRadzen.Components.Pages.Clientes
{
    public partial class Clientes
    {
        [Inject]
        public ClienteApiService<ClienteDTO> ClienteApiService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; } // Inject NavigationManager

        protected List<ClienteDTO> clientes;
        protected string errorMessage = string.Empty;
        protected string searchQuery = string.Empty;

        protected RadzenDataGrid<ClienteDTO> grid0;

        protected override async Task OnInitializedAsync()
        {
            await LoadClientes(); // Carrega clientes inicialmente
        }

        protected async Task LoadClientes()
        {
            try
            {
                clientes = string.IsNullOrWhiteSpace(searchQuery)
                    ? await ClienteApiService.GetAllAsync() // Carrega todos os clientes
                    : await ClienteApiService.GetClientesFiltradosAsync(searchQuery); // Busca clientes filtrados

                errorMessage = string.Empty; // Limpa mensagens de erro
            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar clientes: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
        }

        protected async Task OnSearch(string search)
        {
            searchQuery = search;
            await LoadClientes(); // Atualiza a lista de clientes com base na pesquisa
        }

        protected void AddButtonClick()
        {
            // Ação ao clicar no botão "Adicionar"
            NavigationManager.NavigateTo("/cadastrar-cliente"); // Redireciona para a página de cadastro de cliente
        }

        protected void OnRowSelect(ClienteDTO cliente)
        {
            // Ação ao selecionar uma linha (cliente)
        }

        protected void EditarCliente(ClienteDTO cliente)
        {
            // Implementar lógica de edição de cliente
        }

        protected void ExcluirCliente(ClienteDTO cliente)
        {
            // Implementar lógica de exclusão de cliente
        }
    }
}
