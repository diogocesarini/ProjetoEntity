using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Data.Interface;

namespace ProvaCandidato.Data.Repositorio
{
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(ContextoPrincipal context) : base(context)
        {
        }

    }
}
