//using MySql.Data.MySqlClient;

namespace PIMFazendaUrbanaLib
{
    public interface ICompraDAO
    {
        List<PedidoCompraItem> ListarPedidoCompraItensComFiltros(string search);
        List<PedidoCompra> ListarPedidosCompraComItems();
        List<PedidoCompraItem> ListarItensPedidoCompraPorId(int idPedidoCompra);

        //void CadastrarPedidoCompra(PedidoCompra pedidoCompra, MySqlTransaction transaction);
        //void CadastrarCompraItem(PedidoCompraItem compraItem, MySqlTransaction transaction);

        void CadastrarPedidoCompra(PedidoCompra pedidoCompra);
        List<PedidoCompra> ListarPedidosCompra();
        PedidoCompra ConsultarPedidoCompraPorId(int idPedidoCompra);
        int? ObterUltimoIdPedidoCompra();
        int? ObterUltimoIdPedidoCompraItem();
        List<PedidoCompraItem> ListarRegistrosDeCompra();
        PedidoCompraItem ConsultarCompraItemPorId(int idCompraItem);
        List<PedidoCompraItem> FiltrarRegistrosDeCompraPorNome(string insumoNome);
        List<PedidoCompraItem> FiltrarRegistrosDeCompraPorNomeEPeriodo(string insumoNome, DateTime dataInicio, DateTime dataFim);
    }
}
