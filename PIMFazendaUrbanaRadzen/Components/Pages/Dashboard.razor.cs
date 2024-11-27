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
            await CalcularValoresVendas();
        }

        //**************** VENDAS ****************

        protected List<PedidoVendaItemDTO> vendaitens; // lista de vendaitens

        protected async Task LoadPedidoVendaItens()
        {
            try
            {
                vendaitens = await VendaApiService.GetAllAsync();
                if (vendaitens == null || !vendaitens.Any())
                {
                    throw new Exception("Nenhum item de venda encontrado.");
                }

                errorMessage = string.Empty; // Limpa mensagens de erro
                
            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar vendas: {ex.Message}";
                Console.WriteLine(errorMessage);
                NotificationService.Notify(NotificationSeverity.Error, "Erro", errorMessage, duration: 5000);
            }
        }

        protected string errorMessage = string.Empty; // mensagem de erro


        // ----------------------------------------------------------------------------------
        // ********* LINHA 1 **********

        protected async Task CalcularValoresVendas()
        {
            CalcularValorTotalVendas();
            CalcularValorTotalVendasSemana();
            CalcularValorTotalVendasMes();
            CalcularValorTotalVendasAno();

            CalcularVendasTrimestrais();

            CalcularVendasPorProduto();
            CalcularVendasPorCliente();
        }

        protected decimal valorTotalVendas;
        protected string valorTotalVendasString;

        protected void CalcularValorTotalVendas()
        {
            valorTotalVendas = vendaitens.Sum(v => v.ValorTotal);
            valorTotalVendasString = valorTotalVendas.ToString("C2");
        }

        // *** VENDAS NO ANO POR TRIMESTRE

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
        //-------------------------------------------------------------------------------------------

        // *** SEMANA (segunda-domingo)

        protected List<string> listaOpcoesPeriodoVendasSemana = new List<string>
        {
            "Última semana", "Semana atual"
        };

        protected string opcaoPeriodoVendasSemana = "Semana atual"; // opção padrão para o período de semana

        protected async Task AtualizarOpcaoPeriodoVendasSemana(object value)
        {
            opcaoPeriodoVendasSemana = value.ToString(); // Atualiza a opção selecionada
            CalcularValorTotalVendasSemana(); // Recalcula o valor
            StateHasChanged(); // Atualiza a interface
        }

        protected decimal valorTotalVendasSemana;
        protected string valorTotalVendasSemanaString;
        protected string intervaloSemana;
        protected void CalcularValorTotalVendasSemana()
        {
            var hoje = DateTime.Now.Date;

            DateTime dataInicio, dataFim;

            switch (opcaoPeriodoVendasSemana)
            {
                case "Última semana":
                    // Subtrai 7 dias e calcula segunda a domingo da semana anterior
                    dataFim = hoje.AddDays(-(int)hoje.DayOfWeek); // Domingo da última semana
                    dataInicio = dataFim.AddDays(-6); // Segunda-feira da última semana
                    break;

                case "Semana atual":
                    // Calcula segunda a domingo da semana atual
                    dataInicio = hoje.AddDays(-(int)hoje.DayOfWeek + 1); // Segunda-feira da semana atual
                    dataFim = dataInicio.AddDays(6); // Domingo da semana atual
                    break;

                default:
                    throw new InvalidOperationException("Período inválido");
            }

            // Calcula o valor total das vendas no intervalo
            valorTotalVendasSemana = vendaitens
                .Where(v => v.Data >= dataInicio && v.Data <= dataFim)
                .Sum(v => v.ValorTotal);

            valorTotalVendasSemanaString = valorTotalVendasSemana.ToString("C2");

            // Formata o intervalo de datas para exibição
            intervaloSemana = $"{dataInicio:dd/MM/yyyy} - {dataFim:dd/MM/yyyy}";
        }


        // *** MÊS

        protected List<string> listaOpcoesPeriodoVendasMes = new List<string>
        {
            "Último mês", "Mês atual"
        };

        protected string opcaoPeriodoVendasMes = "Mês atual"; // opção padrão para o período do mês

        protected async Task AtualizarOpcaoPeriodoVendasMes(object value)
        {
            opcaoPeriodoVendasMes = value.ToString(); // Atualiza a opção selecionada
            CalcularValorTotalVendasMes(); // Recalcula o valor
            StateHasChanged(); // Atualiza a interface
        }

        protected decimal valorTotalVendasMes;
        protected string valorTotalVendasMesString;
        protected void CalcularValorTotalVendasMes()
        {
            var ultimoMes = new DateTime();
            var dataInicio = new DateTime();
            var dataFim = new DateTime();

            switch (opcaoPeriodoVendasMes)
            {
                case "Último mês":
                    ultimoMes = DateTime.Now.AddMonths(-1); // Mês anterior
                    break;
                case "Mês atual":
                    ultimoMes = DateTime.Now; // Mês atual
                    break;
                default:
                    break;
            }

            dataInicio = new DateTime(ultimoMes.Year, ultimoMes.Month, 1); // Primeiro dia do mês anterior
            dataFim = dataInicio.AddMonths(1).AddSeconds(-1); // Último dia do mês anterior (23:59:59)

            valorTotalVendasMes = vendaitens
                .Where(v => v.Data >= dataInicio && v.Data <= dataFim) // Filtro por intervalo
                .Sum(v => v.ValorTotal); // Soma os valores

            valorTotalVendasMesString = valorTotalVendasMes.ToString("C2");

            nomeMesAnterior = CapitalizarPrimeiraLetra(ObterNomeDoMes(ultimoMes.Month, ultimoMes.Year));
        }

        private string CapitalizarPrimeiraLetra(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            return char.ToUpper(texto[0]) + texto.Substring(1);
        }

        protected string nomeMesAnterior;

        protected string ObterNomeDoMes(int mes, int ano)
        {
            var nomeMesAnterior = new DateTime(ano, mes, 1);
            return nomeMesAnterior.ToString("MMMM"); // Retorna o nome do mês
        }

        // *** ANO

        protected List<string> listaOpcoesPeriodoVendasAno = new List<string>
        {
            "Último ano", "Ano atual"
        };

        protected string opcaoPeriodoVendasAno = "Ano atual"; // opção padrão para o período do mês

        protected async Task AtualizarOpcaoPeriodoVendasAno(object value)
        {
            opcaoPeriodoVendasAno = value.ToString(); // Atualiza a opção selecionada
            CalcularValorTotalVendasAno(); // Recalcula o valor
            StateHasChanged(); // Atualiza a interface
        }

        protected decimal valorTotalVendasAno;
        protected string valorTotalVendasAnoString;
        protected string AnoSelecionado;

        protected void CalcularValorTotalVendasAno()
        {
            var hoje = DateTime.Now;
            DateTime dataInicio, dataFim;

            switch (opcaoPeriodoVendasAno)
            {
                case "Ano atual":
                    // Intervalo do ano atual: 1º de janeiro até hoje
                    dataInicio = new DateTime(hoje.Year, 1, 1);
                    dataFim = hoje;
                    break;

                case "Último ano":
                    // Intervalo do ano anterior: 1º de janeiro até 31 de dezembro do ano anterior
                    dataInicio = new DateTime(hoje.Year - 1, 1, 1);
                    dataFim = new DateTime(hoje.Year - 1, 12, 31);
                    break;

                default:
                    throw new InvalidOperationException("Período inválido");
            }

            // Calcula o valor total das vendas no intervalo
            valorTotalVendasAno = vendaitens
                .Where(v => v.Data >= dataInicio && v.Data <= dataFim)
                .Sum(v => v.ValorTotal);

            valorTotalVendasAnoString = valorTotalVendasAno.ToString("C2");

            // Formata o intervalo de datas para exibição
            AnoSelecionado = $"{dataInicio.Year}";
        }

        // ---------------------------------------------------------------
        // ********* LINHA 2 **********

        protected List<string> listaOpcoesPeriodoTrimestre = new List<string>
        {
            "Trimestre atual", "Último trimestre"
        };

        protected string opcaoPeriodoProductSales = "Trimestre atual"; // Padrão para produtos
        protected string opcaoPeriodoClientSales = "Trimestre atual";  // Padrão para clientes

        protected List<ProductSales> productSales; // Dados para o gráfico de produtos
        protected List<ClientSales> clientSales;   // Dados para o gráfico de clientes

        protected string intervaloProductSales;    // Intervalo exibido no gráfico de produtos
        protected string intervaloClientSales;     // Intervalo exibido no gráfico de clientes


        protected async Task AtualizarPeriodoProductSales(object value)
        {
            opcaoPeriodoProductSales = value.ToString(); // Atualiza a opção selecionada
            CalcularVendasPorProduto();                 // Recalcula os dados
            StateHasChanged();                          // Atualiza a interface
        }

        protected async Task AtualizarPeriodoClientSales(object value)
        {
            opcaoPeriodoClientSales = value.ToString(); // Atualiza a opção selecionada
            CalcularVendasPorCliente();                // Recalcula os dados
            StateHasChanged();                         // Atualiza a interface
        }


        protected void CalcularVendasPorProduto()
        {
            var (dataInicio, dataFim) = ObterDatasTrimestre(opcaoPeriodoProductSales);
            intervaloProductSales = $"De {dataInicio:dd/MM/yyyy} até {dataFim:dd/MM/yyyy}";

            productSales = vendaitens
                .Where(v => v.Data >= dataInicio && v.Data <= dataFim)
                .GroupBy(v => v.NomeProduto)
                .Select(g => new ProductSales
                {
                    NomeProduto = g.Key,
                    TotalVendas = g.Sum(v => v.ValorTotal)
                })
                .OrderByDescending(p => p.TotalVendas)
                .ToList();
        }

        protected void CalcularVendasPorCliente()
        {
            var (dataInicio, dataFim) = ObterDatasTrimestre(opcaoPeriodoClientSales);
            intervaloClientSales = $"De {dataInicio:dd/MM/yyyy} até {dataFim:dd/MM/yyyy}";

            clientSales = vendaitens
                .Where(v => v.Data >= dataInicio && v.Data <= dataFim)
                .GroupBy(v => v.NomeCliente)
                .Select(g => new ClientSales
                {
                    NomeCliente = g.Key,
                    TotalVendas = g.Sum(v => v.ValorTotal)
                })
                .OrderByDescending(c => c.TotalVendas)
                .ToList();
        }

        protected (DateTime, DateTime) ObterDatasTrimestre(string opcaoPeriodo)
        {
            var hoje = DateTime.Now;

            if (opcaoPeriodo == "Trimestre atual")
            {
                var trimestreAtual = (hoje.Month - 1) / 3 + 1;
                var dataInicio = new DateTime(hoje.Year, (trimestreAtual - 1) * 3 + 1, 1);
                var dataFim = dataInicio.AddMonths(3).AddDays(-1);
                return (dataInicio, dataFim);
            }
            else if (opcaoPeriodo == "Último trimestre")
            {
                var trimestreAtual = (hoje.Month - 1) / 3 + 1;
                var dataInicio = new DateTime(hoje.Year, (trimestreAtual - 2) * 3 + 1, 1);
                var dataFim = dataInicio.AddMonths(3).AddDays(-1);
                return (dataInicio, dataFim);
            }

            // Valor padrão
            return (DateTime.MinValue, DateTime.MinValue);
        }

        public class ProductSales
        {
            public string NomeProduto { get; set; }
            public decimal TotalVendas { get; set; }
        }

        public class ClientSales
        {
            public string NomeCliente { get; set; }
            public decimal TotalVendas { get; set; }
        }


        //----------------------------------------------------------------------------------------------------------

        // talvez exportar para pdf

    }
}
