using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaRadzen.Services;
using Radzen;

namespace PIMFazendaUrbanaRadzen.Components.Pages
{
    public partial class Dashboard
    {
        [Inject]
        public VendaApiService<PedidoVendaItemDTO> VendaApiService { get; set; } // serviço que chama a API

        [Inject]
        public NavigationManager NavigationManager { get; set; } // serviço de navegação

        [Inject]
        public NotificationService NotificationService { get; set; } // serviço de notificação

        [Inject]
        private ExportacaoApiService<object> exportacaoApiService { get; set; } // serviço de exportação para xlsx e csv

        [Inject]
        public IJSRuntime JSRuntime { get; set; } // para chamadas JavaScript

        protected override async Task OnInitializedAsync()
        {
            await LoadPedidoVendaItens();
        }


        //**************** VENDAS ****************

        protected List<PedidoVendaItemDTO> vendaitens; // lista de vendaitens

        protected async Task LoadPedidoVendaItens()
        {
            try
            {
                vendaitens = await VendaApiService.GetAllAsync();

                errorMessage = string.Empty; // Limpa mensagens de erro

                CalcularVendasTrimestrais();

                CalcularVendasMensais();

                CalculateProductSales();

                CalculateClientSales();

                CalcularValorTotalVendas();
                CalcularValorTotalVendasUltimaSemana();
                CalcularValorTotalVendasUltimoMes();
                CalcularValorTotalVendasUltimoAno();
            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar vendas: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
        }

        protected string errorMessage = string.Empty; // mensagem de erro

        protected decimal valorTotalVendas;
        protected string valorTotalVendasString;

        protected decimal valorTotalVendasUltimaSemana;
        protected string valorTotalVendasUltimaSemanaString;

        protected decimal valorTotalVendasUltimoMes;
        protected string valorTotalVendasUltimoMesString;

        protected decimal valorTotalVendasUltimoAno;
        protected string valorTotalVendasUltimoAnoString;

        protected void CalcularValorTotalVendas()
        {
            valorTotalVendas = vendaitens.Sum(v => v.ValorTotal);
            valorTotalVendasString = valorTotalVendas.ToString("C2");
        }

        protected void CalcularValorTotalVendasUltimaSemana()
        {
            var dataInicio = DateTime.Now.Date.AddDays(-7); // Últimos 7 dias, ignorando horas

            valorTotalVendasUltimaSemana = vendaitens
                .Where(v => v.Data >= dataInicio) // Filtro por data
                .Sum(v => v.ValorTotal); // Soma os valores

            valorTotalVendasUltimaSemanaString = valorTotalVendasUltimaSemana.ToString("C2");
        }


        protected void CalcularValorTotalVendasUltimoMes()
        {
            var ultimoMes = DateTime.Now.AddMonths(-1); // Mês anterior
            var dataInicio = new DateTime(ultimoMes.Year, ultimoMes.Month, 1); // Primeiro dia do mês anterior
            var dataFim = dataInicio.AddMonths(1).AddSeconds(-1); // Último dia do mês anterior (23:59:59)

            valorTotalVendasUltimoMes = vendaitens
                .Where(v => v.Data >= dataInicio && v.Data <= dataFim) // Filtro por intervalo
                .Sum(v => v.ValorTotal); // Soma os valores

            valorTotalVendasUltimoMesString = valorTotalVendasUltimoMes.ToString("C2");
        }


        protected void CalcularValorTotalVendasUltimoAno()
        {
            var ultimoAno = DateTime.Now.AddYears(-1).Year;

            valorTotalVendasUltimoAno = vendaitens
                .Where(v => v.Data.Year == ultimoAno)
                .Sum(v => v.ValorTotal);

            valorTotalVendasUltimoAnoString = valorTotalVendasUltimoAno.ToString("C2");
        }

        //------------- Lista de vendas trimestrais
        protected List<VendasTrimestral> vendasTrimestrais;

        public class VendasTrimestral
        {
            public string Trimestre { get; set; }
            public decimal TotalVendas { get; set; }
        }

        protected void CalcularVendasTrimestrais()
        {
            // Agrupar os itens de venda por ano e trimestre
            vendasTrimestrais = vendaitens
                .GroupBy(v => new
                {
                    Ano = v.Data.Year, // Ano da venda
                    Trimestre = (v.Data.Month - 1) / 3 + 1 // Calcula o trimestre
                })
                .Select(g => new VendasTrimestral
                {
                    Trimestre = $"{g.Key.Ano} - T{g.Key.Trimestre}", // Formato: "Ano - T1", "Ano - T2", etc.
                    TotalVendas = g.Sum(v => v.ValorTotal) // Soma o valor total das vendas
                })
                .OrderBy(v => int.Parse(v.Trimestre.Split(" - ")[0])) // Ordena pelo ano
                .ThenBy(v => int.Parse(v.Trimestre.Split("T")[1])) // Ordena pelo trimestre
                .ToList();
        }
        //-----------------------------------------------

        //------------- Lista de vendas mensais
        protected List<VendasMensal> vendasMensais;

        public class VendasMensal
        {
            public string Mes { get; set; }
            public decimal TotalVendas { get; set; }
        }

        protected void CalcularVendasMensais()
        {
            // Agrupar os itens de venda por ano e mês
            vendasMensais = vendaitens
                .GroupBy(v => new
                {
                    Ano = v.Data.Year, // Ano da venda
                    Mes = v.Data.Month // Mês da venda
                })
                .Select(g => new VendasMensal
                {
                    Mes = $"{g.Key.Ano} - {g.Key.Mes}", // Formato: "Ano - Mês"
                    TotalVendas = g.Sum(v => v.ValorTotal) // Soma o valor total das vendas
                })
                .OrderBy(v => int.Parse(v.Mes.Split(" - ")[0])) // Ordena pelo ano
                .ThenBy(v => int.Parse(v.Mes.Split(" - ")[1])) // Ordena pelo mês
                .ToList();
        }


        //------------- Lista de vendas por produto
        protected List<ProductSales> productSales;
        protected void CalculateProductSales()
        {
            // Agrupar os itens de venda por NomeProduto e somar o ValorTotal
            productSales = vendaitens
                .GroupBy(v => v.NomeProduto)
                .Select(g => new ProductSales
                {
                    NomeProduto = g.Key,
                    TotalVendas = g.Sum(v => v.ValorTotal)
                })
                .ToList();
        }

        // Classe auxiliar para armazenar o total de vendas por produto
        public class ProductSales
        {
            public string NomeProduto { get; set; }
            public decimal TotalVendas { get; set; }
        }
        //-----------------------------------------------



        //------------- Lista de vendas por cliente
        protected List<ClientSales> clientSales;
        protected void CalculateClientSales()
        {
            // Agrupar os itens de venda por NomeCliente e somar o ValorTotal
            clientSales = vendaitens
                .GroupBy(v => v.NomeCliente)
                .Select(g => new ClientSales
                {
                    NomeCliente = g.Key,
                    TotalVendas = g.Sum(v => v.ValorTotal)
                })
                .ToList();
        }

        // Classe auxiliar para armazenar o total de vendas por cliente
        public class ClientSales
        {
            public string NomeCliente { get; set; }
            public decimal TotalVendas { get; set; }
        }

        // talvez exportar para pdf

    }
}
