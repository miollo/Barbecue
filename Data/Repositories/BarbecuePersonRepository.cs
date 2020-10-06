using BarbecueSpace.Data.Repositories.Interfaces;
using BarbecueSpace.Models;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BarbecueSpace.Data.Repositories
{
    public class BarbecuePersonRepository : RepositoryBase<BarbecuePerson>, IBarbecuePersonRepository
    {
        public BarbecuePersonRepository(MyDbContext context) : base(context)
        {

        }

        /// <summary>
        /// Busca a pessoa no churrasco pelas duas Fk
        /// </summary>
        /// <param name="barbecueId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        public BarbecuePerson GetBarbecuePerson(int barbecueId, int personId)
        {
            return _context.BarbecuePeople.FirstOrDefault(x => x.BarbecueId == barbecueId && x.PersonId == personId);
        }
    }
}