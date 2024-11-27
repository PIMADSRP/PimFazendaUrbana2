using Microsoft.AspNetCore.Components;
using Radzen;
using PIMFazendaUrbanaAPI.DTOs;
using PIMFazendaUrbanaRadzen.Services;
using System.Globalization;
using Radzen.Blazor;
using Microsoft.JSInterop;

namespace PIMFazendaUrbanaRadzen.Components.Pages.Compras
{
    public partial class CadastrarCompra
    {
        [Inject]
        public CompraApiService<PedidoCompraItemDTO> CompraItemApiService { get; set; }

        [Inject]
        public CompraApiService<PedidoCompraDTO> CompraApiService { get; set; }

        [Inject]
        public InsumosApiService<InsumoDTO> InsumosApiService { get; set; }

        [Inject]
        public FornecedorApiService<FornecedorDTO> FornecedorApiService { get; set; }

        [Inject]
        private ExportacaoApiService<object> exportacaoApiService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public NotificationService NotificationService { get; set; }

        protected PedidoCompraDTO pedidoCompra;

        protected List<PedidoCompraItemDTO> pedidoCompraItens;

        protected PedidoCompraItemDTO pedidoCompraItemAdicionar;

        protected RadzenDataGrid<PedidoCompraItemDTO> grid0;
        protected RadzenDataGrid<InsumoDTO> grid1;

        protected bool errorVisible;
        protected string errorMessage = string.Empty;

        protected List<InsumoDTO> insumos;
        protected InsumoDTO insumoSelecionado;

        protected List<FornecedorDTO> fornecedores;
        protected FornecedorDTO fornecedorSelecionado;

        protected override async Task OnInitializedAsync()
        {
            fornecedores = new List<FornecedorDTO>();
            foreach (var fornecedor in fornecedores)
            {
                fornecedor.Endereco = new EnderecoDTO();
                fornecedor.Telefone = new TelefoneDTO();
            }

            insumos = new List<InsumoDTO>();

            pedidoCompra = new PedidoCompraDTO
            {
                Itens = new List<PedidoCompraItemDTO>()
            };

            pedidoCompraItens = new List<PedidoCompraItemDTO>();

            fornecedorSelecionavelTravado = false;

            InicializarObjetosFormulario();

            await LoadFornecedores();
            await LoadInsumos();
            await GetSetUltimoIdPedidoCompra();
            pedidoCompra.Data = DateTime.Now;
        }

        protected void InicializarObjetosFormulario()
        {
            pedidoCompraItemAdicionar = new PedidoCompraItemDTO();

            if (fornecedorSelecionavelTravado == false)
            {
                fornecedorSelecionado = new FornecedorDTO
                {
                    Endereco = new EnderecoDTO(),
                    Telefone = new TelefoneDTO()
                };
            }

            insumoSelecionado = new InsumoDTO();
        }

        // PARA O PEDIDO
        protected async Task GetSetUltimoIdPedidoCompra()
        {
            try
            {
                int ultimoId = await CompraItemApiService.GetUltimoIdPedidoCompra();
                pedidoCompra.Id = ultimoId + 1;

                errorMessage = string.Empty; // Limpa mensagens de erro
            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar obter último Id de item de compra: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
        }

        // PARA OS ITENS
        protected async Task GetSetUltimoIdPedidoCompraItem()
        {
            try
            {
                if (pedidoCompraItens.Any())
                {
                    // obter o id do ultimo item da lista pedidoCompraItens
                    pedidoCompraItemAdicionar.Id = pedidoCompraItens.Max(p => p.Id) + 1;
                }
                else
                {
                    int ultimoIdItem = await CompraItemApiService.GetUltimoIdPedidoCompraItem();
                    pedidoCompraItemAdicionar.Id = ultimoIdItem + 1;

                    errorMessage = string.Empty; // Limpa mensagens de erro
                }

            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar obter último Id de pedido: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
        }

        protected async Task LoadFornecedores()
        {
            try
            {
                fornecedores = await FornecedorApiService.GetAllAsync(); // Carrega todos os fornecedores

                errorMessage = string.Empty; // Limpa mensagens de erro
            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar fornecedores: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
        }

        protected async Task LoadInsumos()
        {
            try
            {
                insumos = await InsumosApiService.GetAllAsync(); // Carrega todos os insumos

                errorMessage = string.Empty; // Limpa mensagens de erro
            }
            catch (Exception ex)
            {
                errorMessage = $"Erro ao carregar insumos: {ex.Message}";
                Console.WriteLine(errorMessage);
            }
        }

        protected void AtualizarFornecedorSelecionado(object args)
        {
            if (args is FornecedorDTO fornecedor)
            {
                fornecedorSelecionado = fornecedor;
                pedidoCompra.IdFornecedor = fornecedorSelecionado.Id;
            }
        }

        protected void AtualizarInsumoSelecionado(object args)
        {
            if (args is InsumoDTO insumo)
            {
                insumoSelecionado = insumo;
            }
        }

        private decimal valorTotalItem;

        protected void CalcularValorTotalItem()
        {
            valorTotalItem = pedidoCompraItemAdicionar.Valor * pedidoCompraItemAdicionar.Qtd;
        }

        private bool fornecedorSelecionavelTravado = false;
        private string estiloCSSfornecedorSelecionado = "width: 100%;";

        protected void TravarFornecedorSelecionado()
        {
            fornecedorSelecionavelTravado = true;
            estiloCSSfornecedorSelecionado = "width: 100%; background-color: rgba(161, 161, 161, 0.15);";
        }

        protected async Task AdicionarItemAoCarrinho()
        {
            if (fornecedorSelecionado == null)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Selecione um fornecedor.", 3000);
                return;
            }

            if (insumoSelecionado == null)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Selecione um insumo.", 3000);
                return;
            }

            if (pedidoCompraItemAdicionar.Qtd <= 0)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Quantidade deve ser maior que zero.", 3000);
                return;
            }

            if (pedidoCompraItemAdicionar.Valor <= 0)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Valor unitário deve ser maior que zero.", 3000);
                return;
            }

            await GetSetUltimoIdPedidoCompraItem();
            pedidoCompraItemAdicionar.IdPedidoCompra = pedidoCompra.Id;
            pedidoCompraItemAdicionar.IdInsumo = insumoSelecionado.Id;
            pedidoCompraItemAdicionar.Data = DateTime.Now;
            pedidoCompraItemAdicionar.NomeFornecedor = fornecedorSelecionado.Nome;
            pedidoCompraItemAdicionar.NomeInsumo = insumoSelecionado.Nome;
            pedidoCompraItemAdicionar.UnidQtd = insumoSelecionado.Unidqtd;

            valorTotalItem = 0;

            pedidoCompraItens.Add(pedidoCompraItemAdicionar);

            AtualizarValorTotalPedido();

            TravarFornecedorSelecionado();

            InicializarObjetosFormulario();

            ReloadDatagrid();
            StateHasChanged();

        }

        private decimal valorTotalPedido = 0;

        protected void AtualizarValorTotalPedido()
        {
            valorTotalPedido = pedidoCompraItens.Sum(i => i.ValorTotal);

        }

        protected void ReloadDatagrid()
        {
            grid0.Reload();
        }

        [Inject]
        protected DialogService DialogService { get; set; }

        private async Task ConfirmarRemoverItemCarrinho(PedidoCompraItemDTO item)
        {
            bool? confirm = await DialogService.Confirm($"Tem certeza que deseja remover {item.NomeInsumo}?",
                                                         "Confirmação de Exclusão",
                                                         new ConfirmOptions { OkButtonText = "Remover", CancelButtonText = "Cancelar" });

            if (confirm == true)
            {
                RemoverItemCarrinho(item);
                ReloadDatagrid();
                StateHasChanged();
            }
        }

        protected void RemoverItemCarrinho(PedidoCompraItemDTO item)
        {
            pedidoCompraItens.Remove(item);
        }

        protected void EditarItemCarrinho(PedidoCompraItemDTO item)
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
            if (errorVisible)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", $"Ocorreram erros no processo, não é possível realizar o cadastro. {errorMessage}", 5000);
                return;
            }

            if (pedidoCompraItens.Any())
            {
                bool? confirm = await DialogService.Confirm($"Confirma o cadastro da compra?",
                             "Confirmação de Compra",
                             new ConfirmOptions { OkButtonText = "Confirmar", CancelButtonText = "Cancelar" });

                if (confirm == true)
                {
                    await CadastrarPedidoCompra();
                }
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Não há itens para cadastrar.", 3000);
            }
        }

        protected async Task CadastrarPedidoCompra()
        {
            try
            {
                pedidoCompra.IdFornecedor = fornecedorSelecionado.Id; // define o fornecedor do pedido de compra como o fornecedor selecionado
                pedidoCompra.NomeFornecedor = fornecedorSelecionado.Nome; // define o nome do fornecedor do pedido de compra como o nome do fornecedor selecionado
                pedidoCompra.Itens = pedidoCompraItens; // define a lista de itens do pedido de compra como os itens do carrinho
                pedidoCompra.Data = DateTime.Now;

                Console.WriteLine($"Chamando ApiService: CreateAsync" + " hora atual: " + DateTime.Now);
                var response = await CompraApiService.CreateAsync(pedidoCompra); // Chama ApiService
                Console.WriteLine("Retornou de ApiService: Create Async" + " hora atual: " + DateTime.Now);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Navegando para /compras");
                    // Redireciona para a página de compras e exibe mensagem de sucesso
                    NavigationManager.NavigateTo("/compras");
                    NotificationService.Notify(NotificationSeverity.Success, "Sucesso", "Pedido de compra cadastrado com sucesso!", duration: 5000);
                }
                else
                {
                    // Usando ApiResponseHelper apenas para processar resposta de erro
                    var errorMessage = await ApiResponseHelper.HandleErrorResponseAsync(response);
                    NotificationService.Notify(NotificationSeverity.Error, "Erro", $"Falha ao cadastrar pedido de compra: {errorMessage}", duration: 10000);
                }
            }
            catch (Exception ex)
            {
                errorVisible = true; // Exibe mensagem de erro
                Console.WriteLine($"Erro ao cadastrar pedido de compra: {ex.Message}");
            }
        }

        protected async Task CancelButtonClick()
        {
            // Redireciona para a página de compras
            NavigationManager.NavigateTo("/compras");
        }


        protected async Task OnExportarClick(RadzenSplitButtonItem args)
        {
            if (args == null || string.IsNullOrEmpty(args.Value.ToString()))
            {
                NotificationService.Notify(NotificationSeverity.Error, "Erro", "Por favor, selecione um formato de exportação.", duration: 2000);
                return;
            }

            string format = args.Value.ToString(); // "xlsx" ou "csv"
            string fileName = $"PedidoCompra_{DateTime.Now:yyyyMMdd_HHmmss}";

            try
            {
                // Verifique se há dados
                if (pedidoCompra == null || !pedidoCompraItens.Any())
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Erro", "Não há dados para exportar.", duration: 2000);
                    return;
                }

                // Chama o serviço para exportação com base no formato selecionado
                var fileBytes = await exportacaoApiService.ExportarAsync(pedidoCompraItens, format, fileName);

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