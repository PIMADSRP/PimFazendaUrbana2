using Microsoft.AspNetCore.Components;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaRadzen.Services;
using Radzen.Blazor;

namespace PIMFazendaUrbanaRadzen.Components.Pages.Funcionarios
{
    public partial class Funcionarios
    {
        [Inject]
        public FuncionarioApiService<FuncionarioDTO> FuncionarioApiService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; } // Inject NavigationManager

        protected List<FuncionarioDTO> funcionarios;
        protected string errorMessage = string.Empty;
        protected string searchQuery = string.Empty;

        protected RadzenDataGrid<FuncionarioDTO> grid0;

        protected override async Task OnInitializedAsync()
        {
            await LoadFuncionarios(); // Carrega funcionarios inicialmente
        }

        protected async Task LoadFuncionarios()
        {
            try
            {
                funcionarios = string.IsNullOrWhiteSpace(searchQuery)
                    ? await FuncionarioApiService.GetAllAsync() // Carrega todos os funcionarios
                    : await FuncionarioApiService.GetFuncionariosFiltradosAsync(searchQuery); // Busca funcionarios filtrados

                errorMessage = string.Empty; // Limpa mensagens de erro
            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar funcionarios: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
        }

        protected async Task OnSearch(string search)
        {
            searchQuery = search;
            await LoadFuncionarios(); // Atualiza a lista de funcionarios com base na pesquisa
        }

        protected void AddButtonClick()
        {
            // Ação ao clicar no botão "Adicionar"
            NavigationManager.NavigateTo("/add-funcionario"); // Redireciona para a página de cadastro de funcionario
        }

        protected void OnRowSelect(FuncionarioDTO funcionario)
        {
            // Ação ao selecionar uma linha (funcionario)
        }

        protected void EditarFuncionario(FuncionarioDTO funcionario)
        {
            // Implementar lógica de edição de funcionario
        }

        protected void ExcluirFuncionario(FuncionarioDTO funcionario)
        {
            // Implementar lógica de exclusão de funcionario
        }
    }
}
