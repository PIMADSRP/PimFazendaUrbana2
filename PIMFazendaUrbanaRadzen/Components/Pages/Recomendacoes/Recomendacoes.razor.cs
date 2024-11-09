using Microsoft.AspNetCore.Components;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaRadzen.Services;
using Radzen;
using Radzen.Blazor;
using static PIMFazendaUrbanaRadzen.Components.Pages.Vendas.CadastrarVenda;

namespace PIMFazendaUrbanaRadzen.Components.Pages.Recomendacoes
{
    public partial class Recomendacoes
    {
        private string selectedRegiao;
        private string selectedEstacao;
        private string selectedAmbienteControlado = "Sim"; // Definindo "Sim" como padrão
        private bool selectedAmbienteControladoBool;

        private List<Item> comboBoxRegiaoItems = new List<Item>
        {
            new Item { Text = "Norte", Value = "Norte" },
            new Item { Text = "Nordeste", Value = "Nordeste" },
            new Item { Text = "Centro-Oeste", Value = "Centro-Oeste" },
            new Item { Text = "Sudeste", Value = "Sudeste" },
            new Item { Text = "Sul", Value = "Sul" }
        };

        private List<Item> comboBoxEstacaoItems = new List<Item>
        {
            new Item { Text = "Verão", Value = "Verão" },
            new Item { Text = "Outono", Value = "Outono" },
            new Item { Text = "Inverno", Value = "Inverno" },
            new Item { Text = "Primavera", Value = "Primavera" }
        };

        private List<CultivoDTO> dataGridItems = new List<CultivoDTO>();

        [Inject]
        public NotificationService NotificationService { get; set; }

        [Inject]
        private RecomendacaoApiService<CultivoDTO> recomendacaoApiService { get; set; }

        protected bool errorVisible;


        private async Task OnConfirmarClick()
        {
            try
            {
                // Verificar se os campos obrigatórios foram preenchidos antes de enviar a requisição
                if (string.IsNullOrEmpty(selectedRegiao))
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Erro", "Por favor, selecione uma região.", duration: 2000);
                    return;
                }

                if (string.IsNullOrEmpty(selectedEstacao))
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Erro", "Por favor, selecione uma estação.", duration: 2000);
                    return;
                }

                // Definir o valor do ambiente controlado
                selectedAmbienteControladoBool = selectedAmbienteControlado == "Sim";

                // Chame a API para obter as recomendações
                var recomendacoes = await recomendacaoApiService.GetRecomendacoesAsync(
                    selectedRegiao,
                    selectedEstacao,
                    selectedAmbienteControladoBool
                );

                // Atualize o DataGrid com os dados recebidos
                dataGridItems = recomendacoes;
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro ao obter recomendações", ex.Message, duration: 5000);
            }
        }




    }
}
