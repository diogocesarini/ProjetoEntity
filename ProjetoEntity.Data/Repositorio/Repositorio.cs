using ProvaCandidato.Data.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProvaCandidato.Data.Repositorio
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        protected readonly ContextoPrincipal _contexto = null;
        protected readonly DbSet<TEntity> entidade;
        private bool _disposed;

        public Repositorio()
        {
            if (_contexto == null)
            {
                _contexto = new ContextoPrincipal();
            }

            entidade = _contexto.Set<TEntity>();
        }

        public Repositorio(ContextoPrincipal contexto)
        {
            _contexto = contexto;
            entidade = _contexto.Set<TEntity>();
        }

        public virtual void Adicionar(TEntity obj)
        {
            entidade.Add(obj);
        }

        public virtual TEntity AdicionarESalvar(TEntity obj)
        {
            entidade.Add(obj);
            _contexto.SaveChanges();
            return obj;
        }

        public virtual TEntity AdicionarOuAtualizarESalvar(TEntity obj)
        {
            entidade.AddOrUpdate(obj);
            _contexto.SaveChanges();
            return obj;
        }

        public virtual void Adicionar(List<TEntity> objs)
        {
            entidade.AddRange(objs);
        }

        public virtual TEntity BuscarPorId(int id)
        {
            return entidade.Find(id);
        }

        public virtual IQueryable<TEntity> BuscarTodos()
        {
            return entidade;
        }

        public virtual void Atualizar(TEntity obj)
        {
            entidade.AddOrUpdate(obj);
        }

        public virtual void Deletar(IEnumerable<TEntity> objs)
        {
            entidade.RemoveRange(objs);
        }

        public virtual void Deletar(int id)
        {
            entidade.Remove(entidade.Find(id));
        }

        public virtual IQueryable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return entidade.Where(predicate);
        }
       
        public virtual void Deletar(TEntity obj)
        {
            entidade.Remove(obj);
        }

       
        public int Salvar()
        {
            try
            {
                return _contexto.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                //Esse try têm a finalidade de inspecionar e mostrar erros no sql server.
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    var entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }
                }

                throw;
            }
            catch 
            {
                throw;
            }
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed && disposing)
            {
                _contexto.Dispose();
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
