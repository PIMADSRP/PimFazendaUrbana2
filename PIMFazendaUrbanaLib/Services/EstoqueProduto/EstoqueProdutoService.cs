namespace PIMFazendaUrbanaLib
{
    public class ProdutoService
    {
        private EstoqueProdutoDAO estoqueProdutoDAO;

        public ProdutoService()
        {
            this.estoqueProdutoDAO = new EstoqueProdutoDAO();
        }

        public List<EstoqueProduto> ListarEstoqueProdutoAtivos()
        {
            try
            {
                List<EstoqueProduto> produtos = estoqueProdutoDAO.ListarEstoqueProdutoAtivos();
                return produtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar estoque de produtos: " + ex.Message);
            }
        }

        public List<EstoqueProduto> FiltrarProdutosPorNome(string produtoNome)
        {
            try
            {
                List<EstoqueProduto> produtos = estoqueProdutoDAO.FiltrarProdutosPorNome(produtoNome);
                return produtos;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar produtos por nome: " + ex.Message);
            }
        }

        // Método para filtrar produtos pela unidade
        public List<EstoqueProduto> FiltrarProdutosPorUnidade(string unidade)
        {
            try
            {
                return estoqueProdutoDAO.FiltrarProdutosPorUnidade(unidade);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao filtrar produtos pela unidade: " + ex.Message);
            }
        }


        // ----------------------------------------------
        // Não tem validação porque só dá saída (finaliza) na produção inteira

    }
}
