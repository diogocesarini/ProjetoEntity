using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Data.Interface;

namespace ProvaCandidato.Data.Repositorio
{
    public class CidadeRepositorio : Repositorio<Cidade>, ICidadeRepositorio
    {
        public CidadeRepositorio(ContextoPrincipal context) : base(context)
        {
        }

    }
}
