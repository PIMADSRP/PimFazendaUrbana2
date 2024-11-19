using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaRadzen.Components.Pages.Clientes;
using PIMFazendaUrbanaRadzen.Services;
using Radzen;
using Radzen.Blazor;

namespace PIMFazendaUrbanaRadzen.Components.Pages.Fornecedores
{
    public partial class Fornecedores
    {
        [Inject]
        public FornecedorApiService<FornecedorDTO> FornecedorApiService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; } // Inject NavigationManager

        [Inject]
        public NotificationService NotificationService { get; set; }

        [Inject]
        private ExportacaoApiService<object> exportacaoApiService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

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
            NavigationManager.NavigateTo("/cadastrar-fornecedor"); // Redireciona para a página de cadastro de fornecedor
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

        protected async Task OnExportarClick(RadzenSplitButtonItem args)
        {
            if (args == null || string.IsNullOrEmpty(args.Value.ToString()))
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Por favor, selecione um formato de exportação.");
                return;
            }

            string format = args.Value.ToString(); // "xlsx" ou "csv"
            string fileName = $"Fornecedores_{DateTime.Now:yyyyMMdd_HHmmss}";

            try
            {
                // Verifique se há dados
                if (fornecedores == null || !fornecedores.Any())
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Erro", "Não há dados para exportar.");
                    return;
                }

                // Chama o serviço para exportação com base no formato selecionado
                var fileBytes = await exportacaoApiService.ExportarAsync(fornecedores, format, fileName);

                if (fileBytes != null)
                {
                    // Gera o download no navegador
                    await JSRuntime.InvokeVoidAsync("downloadFile", fileName + "." + format, Convert.ToBase64String(fileBytes));
                }
                else
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Erro ao exportar", "Nenhum arquivo foi gerado.");
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro ao exportar", ex.Message);
            }
        }

    }
}
