using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BarbecueSpace.Data.Repositories.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        /// <summary>
        /// Busca um item por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(int id);

        /// <summary>
        /// Busca todos itens da tabela
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Adiciona um novo item
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Adiciona multiplos itens
        /// </summary>
        /// <param name="entities"></param>
        void AddMultiple(IEnumerable<TEntity> entities);

        /// <summary>
        /// Remove um item
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// Remove multipos items
        /// </summary>
        /// <param name="entities"></param>
        void RemoveMultiple(IEnumerable<TEntity> entities);

        /// <summary>
        /// Persiste as informações 
        /// </summary>
        void SaveChanges();
    }
}