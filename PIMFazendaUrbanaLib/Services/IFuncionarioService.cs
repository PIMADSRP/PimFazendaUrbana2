namespace PIMFazendaUrbanaLib
{
    public interface IFuncionarioService
    {
        Funcionario AutenticarFuncionario(string usuario, string senha);
    }
}