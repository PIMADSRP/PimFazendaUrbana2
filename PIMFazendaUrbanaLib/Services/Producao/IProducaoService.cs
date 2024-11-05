namespace PIMFazendaUrbanaLib
{
    public interface IProducaoService
    {
        List<Producao> ListarProducoesComFiltros(string search);
        void CadastrarProducao(Producao producao);
        void AlterarProducao(Producao producao);
        void FinalizarProducao(int producaoId);
        List<Producao> ListarProducoes();
        List<Producao> ListarProducoesNaoFinalizadas();
        List<Producao> FiltrarProducoesNome(string cultivoNome);
        Producao ConsultarProducaoID(int producaoId);
        List<Producao> FiltrarProducoesPorNomeEPeriodo(string cultivoNome, DateTime dataInicio, DateTime dataFim);
        void ValidarProducao(Producao producao);
    }
}