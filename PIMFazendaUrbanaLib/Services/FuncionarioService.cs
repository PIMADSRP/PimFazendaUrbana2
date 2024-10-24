namespace PIMFazendaUrbanaLib
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioDAO funcionarioDAO;

        public FuncionarioService(string connectionString)
        {
            this.funcionarioDAO = new FuncionarioDAO(connectionString);
        }

        public Funcionario AutenticarFuncionario(string usuario, string senha)
        {
            try
            {
                Funcionario funcionario = funcionarioDAO.AutenticarFuncionario(usuario, senha);
                if (funcionario != null)
                {
                    if (funcionario.StatusAtivo)
                    {
                        return funcionario;
                    }
                    else
                    {
                        throw new UserInactiveException("Usuário inativado.");
                    }
                }
                else
                {
                    throw new AuthenticationException("Usuário ou senha inválidos.");
                }
            }
            catch (UserInactiveException)
            {
                throw; // Propaga a exceção de usuário inativo
            }
            catch (AuthenticationException)
            {
                throw; // Propaga a exceção de autenticação
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao autenticar funcionário: " + ex.Message); // Para demais tipos de exceção
            }
        }
    }
}
