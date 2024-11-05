namespace PIMFazendaUrbanaLib
{
    public interface IFuncionarioService
    {
        List<Funcionario> ListarFuncionariosComFiltros(string search);
        Funcionario AutenticarFuncionario(string usuario, string senha);
        string AutenticarGerente(string usuario);
        bool VerificarUsuarioDisponivel(string usuario);
        bool AlterarSenhaFuncionario(string usuario, string novaSenha);
        bool VerificarSenhaForte(string senha);
        void CadastrarFuncionario(Funcionario funcionario);
        void AlterarFuncionario(Funcionario funcionario);
        void ExcluirFuncionario(int funcionarioId);
        List<Funcionario> ListarFuncionariosAtivos();
        List<Funcionario> ListarFuncionariosInativos();
        Funcionario ConsultarFuncionarioID(int funcionarioId);
        Funcionario ConsultarFuncionarioNome(string funcionarioNome);
        Funcionario ConsultarFuncionarioUsuario(string funcionarioUsuario);
        List<Funcionario> FiltrarFuncionariosNome(string funcionarioNome);
        void ValidarFuncionario(Funcionario funcionario);
        List<ValidationError> ValidarSenha(string senha, List<ValidationError> erros);
    }
}