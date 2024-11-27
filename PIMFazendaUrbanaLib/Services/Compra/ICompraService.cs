namespace PIMFazendaUrbanaLib
{
    public interface ICompraService
    {
        List<PedidoCompraItem> ListarPedidoCompraItensComFiltros(string search);

        //void CadastrarPedidoCompraComItens(PedidoCompra pedidoCompra, List<PedidoCompraItem> compraItems);
        void CadastrarPedidoCompra(PedidoCompra pedidoCompra);
        List<PedidoCompra> ListarPedidosCompra();
        PedidoCompra ConsultarPedidoCompraPorId(int idPedidoCompra);
        int? ObterUltimoIdPedidoCompra();
        int? ObterUltimoIdPedidoCompraItem();
        List<PedidoCompraItem> ListarRegistrosDeCompra();
        PedidoCompraItem ConsultarCompraItemPorId(int idCompraItem);
        List<PedidoCompraItem> FiltrarRegistrosDeCompraPorNome(string insumoNome);
        List<PedidoCompraItem> FiltrarRegistrosDeCompraPorNomeEPeriodo(string insumoNome, DateTime dataInicio, DateTime dataFim);

        //void ValidarCompra(PedidoCompra pedidoCompra, List<PedidoCompraItem> compraItems);
        void ValidarCompra(PedidoCompra pedidoCompra);
    }
}
