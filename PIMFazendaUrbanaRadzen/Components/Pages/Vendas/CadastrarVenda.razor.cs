using Microsoft.AspNetCore.Components;
using Radzen;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaRadzen.Services;
using System.Globalization;
using Radzen.Blazor;
using Microsoft.JSInterop;
using PIMFazendaUrbanaLib;
using PIMFazendaUrbanaRadzen.Components.Pages.Producao;

namespace PIMFazendaUrbanaRadzen.Components.Pages.Vendas
{
    public partial class CadastrarVenda
    {
        [Inject]
        public VendaApiService<PedidoVendaItemDTO> VendaItemApiService { get; set; }

        [Inject]
        public VendaApiService<PedidoVendaDTO> VendaApiService { get; set; }

        [Inject]
        public EstoqueProdutoApiService<EstoqueProdutoDTO> EstoqueProdutoApiService { get; set; }

        [Inject]
        public ClienteApiService<ClienteDTO> ClienteApiService { get; set; }

        [Inject]
        private ExportacaoApiService<object> exportacaoApiService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public NotificationService NotificationService { get; set; }

        protected PedidoVendaDTO pedidoVenda;

        protected List<PedidoVendaItemDTO> pedidoVendaItens;

        protected PedidoVendaItemDTO pedidoVendaItemAdicionar;

        protected RadzenDataGrid<PedidoVendaItemDTO> grid0;

        protected bool errorVisible;
        protected string errorMessage = string.Empty;

        protected List<EstoqueProdutoDTO> estoqueProdutos;
        protected EstoqueProdutoDTO estoqueProdutoSelecionado;

        protected List<ClienteDTO> clientes;
        protected ClienteDTO clienteSelecionado;

        protected override async Task OnInitializedAsync()
        {
            clientes = new List<ClienteDTO>();
            foreach (var cliente in clientes)
            {
                cliente.Endereco = new EnderecoDTO();
                cliente.Telefone = new TelefoneDTO();
            }

            estoqueProdutos = new List<EstoqueProdutoDTO>();
            foreach (var estoqueProduto in estoqueProdutos)
            {
                estoqueProduto.Producao = new ProducaoDTO();
                estoqueProduto.Producao.Cultivo = new CultivoDTO();
            }

            pedidoVenda = new PedidoVendaDTO
            {
                Itens = new List<PedidoVendaItemDTO>()
            };

            pedidoVendaItens = new List<PedidoVendaItemDTO>();

            clienteSelecionavelTravado = false;

            InicializarObjetosFormulario();

            await LoadClientes();
            await LoadEstoqueProdutos();
            await GetSetUltimoIdPedidoVenda();
            pedidoVenda.Data = DateTime.Now;
        }

        protected void InicializarObjetosFormulario()
        {
            pedidoVendaItemAdicionar = new PedidoVendaItemDTO();

            if (clienteSelecionavelTravado == false)
            {
                clienteSelecionado = new ClienteDTO
                {
                    Endereco = new EnderecoDTO(),
                    Telefone = new TelefoneDTO()
                };
            }

            estoqueProdutoSelecionado = new EstoqueProdutoDTO
            {
                Producao = new ProducaoDTO
                {
                    Cultivo = new CultivoDTO()
                }
            };
        }

        // PARA O PEDIDO
        protected async Task GetSetUltimoIdPedidoVenda()
        {
            try
            {
                int ultimoId = await VendaItemApiService.GetUltimoIdPedidoVenda();
                pedidoVenda.Id = ultimoId + 1; 

                errorMessage = string.Empty; // Limpa mensagens de erro
            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar obter último Id de item de venda: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
        }

        // PARA OS ITENS
        protected async Task GetSetUltimoIdPedidoVendaItem()
        {
            try
            {
                if (pedidoVendaItens.Any())
                {
                    // obter o id do ultimo item da lista pedidoVendaItens
                    pedidoVendaItemAdicionar.Id = pedidoVendaItens.Max(p => p.Id) + 1;
                }
                else
                {
                    int ultimoIdItem = await VendaItemApiService.GetUltimoIdPedidoVendaItem();
                    pedidoVendaItemAdicionar.Id = ultimoIdItem + 1;

                    errorMessage = string.Empty; // Limpa mensagens de erro
                }

            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar obter último Id de pedido: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
        }

        protected async Task LoadClientes()
        {
            try
            {
                clientes = await ClienteApiService.GetAllAsync(); // Carrega todos os clientes

                errorMessage = string.Empty; // Limpa mensagens de erro
            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar clientes: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
        }

        protected async Task LoadEstoqueProdutos()
        {
            try
            {
                estoqueProdutos = await EstoqueProdutoApiService.GetAllAsync(); // Carrega todos os produtos em estoque

                errorMessage = string.Empty; // Limpa mensagens de erro
            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar produtos em estoque: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
        }

        protected void AtualizarClienteSelecionado(object args)
        {
            if (args is ClienteDTO cliente)
            {
                clienteSelecionado = cliente;
                pedidoVenda.IdCliente = clienteSelecionado.Id;
            }
        }

        protected void AtualizarEstoqueProdutoSelecionado(object args)
        {
            if (args is EstoqueProdutoDTO estoqueProduto)
            {
                estoqueProdutoSelecionado = estoqueProduto;
            }
        }

        private decimal valorTotalItem;

        protected void CalcularValorTotalItem()
        {
            valorTotalItem = pedidoVendaItemAdicionar.Valor * pedidoVendaItemAdicionar.Qtd;
        }

        private bool clienteSelecionavelTravado = false;
        private string estiloCSSclienteSelecionado = "width: 100%;";

        protected void TravarClienteSelecionado()
        {
            clienteSelecionavelTravado = true;
            estiloCSSclienteSelecionado = "width: 100%; background-color: rgba(161, 161, 161, 0.15);";
        }

        protected async Task AdicionarItemAoCarrinho()
        {
            if (clienteSelecionado == null)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Selecione um cliente.", 3000);
                return;
            }

            if (estoqueProdutoSelecionado == null)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Selecione um produto.", 3000);
                return;
            }

            if (pedidoVendaItemAdicionar.Qtd <= 0)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Quantidade deve ser maior que zero.", 3000);
                return;
            }

            if (pedidoVendaItemAdicionar.Qtd > estoqueProdutoSelecionado.Qtd)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Quantidade do item não pode ser maior que a quantidade em estoque.", 3000);
                return;
            }

            if (pedidoVendaItemAdicionar.Valor <= 0)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Valor unitário deve ser maior que zero.", 3000);
                return;
            }

            await GetSetUltimoIdPedidoVendaItem();
            pedidoVendaItemAdicionar.IdPedidoVenda = pedidoVenda.Id;
            pedidoVendaItemAdicionar.IdProduto = estoqueProdutoSelecionado.Id;
            pedidoVendaItemAdicionar.Data = DateTime.Now;
            pedidoVendaItemAdicionar.NomeCliente = clienteSelecionado.Nome;
            pedidoVendaItemAdicionar.NomeProduto = estoqueProdutoSelecionado.Producao.Cultivo.Variedade;
            pedidoVendaItemAdicionar.UnidQtd = estoqueProdutoSelecionado.Unidqtd;

            valorTotalItem = 0;

            pedidoVendaItens.Add(pedidoVendaItemAdicionar);

            AtualizarValorTotalPedido();

            TravarClienteSelecionado();

            InicializarObjetosFormulario();

            ReloadDatagrid();
            StateHasChanged();

        }

        private decimal valorTotalPedido = 0;

        protected void AtualizarValorTotalPedido()
        {
            valorTotalPedido = pedidoVendaItens.Sum(i => i.ValorTotal);

        }

        protected void ReloadDatagrid()
        {
            grid0.Reload();
        }



        [Inject]
        protected DialogService DialogService { get; set; }

        private async Task ConfirmarRemoverItemCarrinho(PedidoVendaItemDTO item)
        {
            bool? confirm = await DialogService.Confirm($"Tem certeza que deseja remover {item.NomeProduto}?",
                                                         "Confirmação de Exclusão",
                                                         new ConfirmOptions { OkButtonText = "Remover", CancelButtonText = "Cancelar" });

            if (confirm == true)
            {
                RemoverItemCarrinho(item);
                ReloadDatagrid();
                StateHasChanged();
            }
        }

        protected void RemoverItemCarrinho(PedidoVendaItemDTO item)
        {
            pedidoVendaItens.Remove(item);
        }

        protected void EditarItemCarrinho(PedidoVendaItemDTO item)
        {
            /*
            AlternarModoEditar();

            // Cria uma cópia do cultivo para edição
            cultivoCadastrarOuEditar = new CultivoDTO
            {
                Id = cultivo.Id,
                Nome = cultivo.Nome,
                Variedade = cultivo.Variedade,
                Categoria = cultivo.Categoria,
                TempoProdTradicional = cultivo.TempoProdTradicional,
                TempoProdControlado = cultivo.TempoProdControlado,
                StatusAtivo = cultivo.StatusAtivo
            };
            */
        }

        private async Task ConfirmarCompra()
        {
            if(errorVisible)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", $"Ocorreram erros no processo, não é possível realizar o cadastro. {errorMessage}", 5000);
                return;
            }

            if (pedidoVendaItens.Any())
            {
                bool? confirm = await DialogService.Confirm($"Confirma o cadastro da venda?",
                             "Confirmação de Venda",
                             new ConfirmOptions { OkButtonText = "Confirmar", CancelButtonText = "Cancelar" });

                if (confirm == true)
                {
                    await CadastrarPedidoVenda();
                }
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Não há itens para cadastrar.", 3000);
            }
        }

        protected async Task CadastrarPedidoVenda()
        {
            try
            {
                pedidoVenda.IdCliente = clienteSelecionado.Id; // define o cliente do pedido de venda como o cliente selecionado
                pedidoVenda.NomeCliente = clienteSelecionado.Nome; // define o nome do cliente do pedido de venda como o nome do cliente selecionado
                pedidoVenda.Itens = pedidoVendaItens; // define a lista de itens do pedido de venda como os itens do carrinho
                pedidoVenda.Data = DateTime.Now;

                Console.WriteLine($"Chamando ApiService: CreateAsync" + " hora atual: " + DateTime.Now);
                var response = await VendaApiService.CreateAsync(pedidoVenda); // Chama ApiService
                Console.WriteLine("Retornou de ApiService: Create Async" + " hora atual: " + DateTime.Now);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Navegando para /vendas");
                    // Redireciona para a página de vendas e exibe mensagem de sucesso
                    NavigationManager.NavigateTo("/vendas");
                    NotificationService.Notify(NotificationSeverity.Success, "Sucesso", "Pedido de venda cadastrado com sucesso!", duration: 5000);
                }
                else
                {
                    // Usando ApiResponseHelper apenas para processar resposta de erro
                    var errorMessage = await ApiResponseHelper.HandleErrorResponseAsync(response);
                    NotificationService.Notify(NotificationSeverity.Error, "Erro", $"Falha ao cadastrar pedido de venda: {errorMessage}", duration: 10000);
                }
            }
            catch (Exception ex)
            {
                errorVisible = true; // Exibe mensagem de erro
                Console.WriteLine($"Erro ao cadastrar pedido de venda: {ex.Message}");
            }
        }

        protected async Task CancelButtonClick()
        {
            // Redireciona para a página de vendas
            NavigationManager.NavigateTo("/vendas");
        }


        protected async Task OnExportarClick(RadzenSplitButtonItem args)
        {
            if (args == null || string.IsNullOrEmpty(args.Value.ToString()))
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Por favor, selecione um formato de exportação.", duration: 2000);
                return;
            }

            string format = args.Value.ToString(); // "xlsx" ou "csv"
            string fileName = $"PedidoVenda_{DateTime.Now:yyyyMMdd_HHmmss}";

            try
            {
                // Verifique se há dados
                if (pedidoVenda == null || !pedidoVendaItens.Any())
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Erro", "Não há dados para exportar.", duration: 2000);
                    return;
                }

                // Chama o serviço para exportação com base no formato selecionado
                var fileBytes = await exportacaoApiService.ExportarAsync(pedidoVendaItens, format, fileName);

                if (fileBytes != null)
                {
                    // Gera o download no navegador
                    await JSRuntime.InvokeVoidAsync("downloadFile", fileName + "." + format, Convert.ToBase64String(fileBytes));
                }
                else
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Erro ao exportar", "Nenhum arquivo foi gerado.", duration: 2000);
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro ao exportar", ex.Message, duration: 5000);
            }
        }


        private int quantidade;
        private string valorUnitario;
        protected void FormatarValorUnitario()
        {
            if (decimal.TryParse(valorUnitario, NumberStyles.Number, CultureInfo.GetCultureInfo("pt-BR"), out decimal valor))
            {
                valorUnitario = valor.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"));
            }
        }

        protected void RemoverCaracteresErrados()
        {
            valorUnitario = valorUnitario.Replace(".", "");
        }




    }
}