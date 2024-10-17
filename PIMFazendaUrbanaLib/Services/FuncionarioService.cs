namespace PIMFazendaUrbanaLib
{
    public class FuncionarioService
    {
        private readonly FuncionarioDAO funcionarioDAO;
        public FuncionarioService()
        {
            this.funcionarioDAO = new FuncionarioDAO();
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
                        throw new AuthenticationException("Usuário ou senha inválidos.");
                    }
                }
                else
                {
                    throw new UserInactiveException("Usuário inativado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao autenticar funcionário: " + ex.Message);
            }
        }



    }
}
