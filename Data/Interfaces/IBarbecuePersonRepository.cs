using BarbecueSpace.Models;

namespace BarbecueSpace.Data.Repositories.Interfaces
{
    public interface IBarbecuePersonRepository : IRepositoryBase<BarbecuePerson>
    {
        /// <summary>
        /// Busca a pessoa no churrasco pelas duas Fk
        /// </summary>
        /// <param name="barbecueId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public BarbecuePerson GetBarbecuePerson(int barbecueId, int personId);
    }
}
