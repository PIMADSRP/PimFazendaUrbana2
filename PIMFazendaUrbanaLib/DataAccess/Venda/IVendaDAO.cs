﻿using MySql.Data.MySqlClient;

namespace PIMFazendaUrbanaLib
{
    public interface IVendaDAO
    {
        List<PedidoVendaItem> ListarPedidoVendaItensComFiltros(string search);
        List<PedidoVenda> ListarPedidosVendaComItems();
        List<PedidoVendaItem> ListarItensPedidoVendaPorId(int idPedidoVenda);

        //void CadastrarPedidoVenda(PedidoVenda pedidoVenda, MySqlTransaction transaction);
        //void CadastrarVendaItem(PedidoVendaItem vendaItem, MySqlTransaction transaction);

        void CadastrarPedidoVenda(PedidoVenda pedidoVenda);
        List<PedidoVenda> ListarPedidosVenda();
        PedidoVenda ConsultarPedidoVenda(int idPedidoVenda);
        int? ObterUltimoIdPedidoVenda();
        List<PedidoVendaItem> ListarRegistrosDeVenda();
        PedidoVendaItem ConsultarVendaItem(int idVendaItem);
        List<PedidoVendaItem> FiltrarRegistrosDeVendaNome(string produtoNome);
        List<PedidoVendaItem> FiltrarRegistrosDeVendaPorNomeEPeriodo(string produtoNome, DateTime dataInicio, DateTime dataFim);
    }
}
