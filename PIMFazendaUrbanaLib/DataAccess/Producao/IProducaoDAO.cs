namespace PIMFazendaUrbanaLib
{
    public interface IProducaoDAO
    {
        List<Producao> ListarProducoesComFiltros(string search);
        void CadastrarProducao(Producao producao);
        void AlterarProducao(Producao producao);
        void FinalizarProducao(int producaoId);
        List<Producao> ListarProducoes();
        List<Producao> ListarProducoesNaoFinalizadas();
        List<Producao> FiltrarProducoesNome(string cultivoNome);
        List<Producao> FiltrarProducoesPorNomeEPeriodo(string cultivoNome, DateTime dataInicio, DateTime dataFim);
        Producao ConsultarProducaoID(int producaoId);
    }
}