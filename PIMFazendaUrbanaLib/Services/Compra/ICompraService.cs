namespace PIMFazendaUrbanaLib
{
    public interface ICompraService
    {
        List<PedidoCompra> ListarComprasComFiltros(string search);

        //void CadastrarPedidoCompraComItens(PedidoCompra pedidoCompra, List<PedidoCompraItem> compraItems);
        void CadastrarPedidoCompra(PedidoCompra pedidoCompra);
        List<PedidoCompra> ListarPedidosCompra();
        PedidoCompra ConsultarPedidoCompra(int idPedidoCompra);
        int? ObterUltimoIdPedidoCompra();
        List<PedidoCompraItem> ListarRegistrosDeCompra();
        PedidoCompraItem ConsultarCompraItem(int idCompraItem);
        List<PedidoCompraItem> FiltrarRegistrosDeCompraNome(string insumoNome);
        List<PedidoCompraItem> FiltrarRegistrosDeCompraPorNomeEPeriodo(string insumoNome, DateTime dataInicio, DateTime dataFim);

        //void ValidarCompra(PedidoCompra pedidoCompra, List<PedidoCompraItem> compraItems);
        void ValidarCompra(PedidoCompra pedidoCompra);
    }
}
