namespace PIMFazendaUrbanaLib
{
    public interface IFuncionarioDAO
    {
        Funcionario AutenticarFuncionario(string usuario, string senha);
    }
}