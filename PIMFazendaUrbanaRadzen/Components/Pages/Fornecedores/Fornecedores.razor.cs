using Microsoft.AspNetCore.Components;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaRadzen.Services;
using Radzen.Blazor;

namespace PIMFazendaUrbanaRadzen.Components.Pages.Fornecedores
{
    public partial class Fornecedores
    {
        [Inject]
        public FornecedorApiService<FornecedorDTO> FornecedorApiService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; } // Inject NavigationManager

        protected List<FornecedorDTO> fornecedores;
        protected string errorMessage = string.Empty;
        protected string searchQuery = string.Empty;

        protected RadzenDataGrid<FornecedorDTO> grid0;

        protected override async Task OnInitializedAsync()
        {
            await LoadFornecedores(); // Carrega clientes inicialmente
        }

        protected async Task LoadFornecedores()
        {
            try
            {
                fornecedores = string.IsNullOrWhiteSpace(searchQuery)
                    ? await FornecedorApiService.GetAllAsync() // Carrega todos os fornecedores
                    : await FornecedorApiService.GetFornecedoresFiltradosAsync(searchQuery); // Busca fornecedores filtrados

                errorMessage = string.Empty; // Limpa mensagens de erro
            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar fornecedores: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
        }

        protected async Task OnSearch(string search)
        {
            searchQuery = search;
            await LoadFornecedores(); // Atualiza a lista de fornecedores com base na pesquisa
        }

        protected void AddButtonClick()
        {
            // Ação ao clicar no botão "Adicionar"
            NavigationManager.NavigateTo("/add-fornecedor"); // Redireciona para a página de cadastro de fornecedor
        }

        protected void OnRowSelect(FornecedorDTO fornecedor)
        {
            // Ação ao selecionar uma linha (fornecedor)
        }

        protected void EditarFornecedor(FornecedorDTO fornecedor)
        {
            // Implementar lógica de edição de fornecedor
        }

        protected void ExcluirFornecedor(FornecedorDTO fornecedor)
        {
            // Implementar lógica de exclusão de fornecedor
        }
    }
}
