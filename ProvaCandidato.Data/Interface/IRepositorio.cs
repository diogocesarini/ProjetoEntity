using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProvaCandidato.Data.Interface
{
    public interface IRepositorio<TEntity> : IDisposable where TEntity : class
    { 
        void Adicionar(TEntity obj);
        void Adicionar(List<TEntity> objs);
        TEntity AdicionarESalvar(TEntity obj);
        TEntity AdicionarOuAtualizarESalvar(TEntity obj);
        TEntity BuscarPorId(int id);
        IQueryable<TEntity> BuscarTodos();
        void Atualizar(TEntity obj);
        void Deletar(int id);
        void Deletar(TEntity obj);
        void Deletar(IEnumerable<TEntity> objs);
        IQueryable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate);
        int Salvar();
    }
}
