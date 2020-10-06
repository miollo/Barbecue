using BarbecueSpace.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BarbecueSpace.Data.Repositories
{
    /// <summary>
    /// Implementação do repositório base que é herdado pelos outros repositórios
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly MyDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(MyDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Busca um item por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Busca todos itens da tabela
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            return  _dbSet.ToList();
        }

        /// <summary>
        /// Adiciona um novo item
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Adiciona multiplos itens
        /// </summary>
        /// <param name="entities"></param>
        public void AddMultiple(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// Remove um item
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Remove multipos items
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveMultiple(IEnumerable<TEntity> entities)
        {
           _dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Persiste as informações 
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}