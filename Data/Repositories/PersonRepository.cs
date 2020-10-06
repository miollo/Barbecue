using BarbecueSpace.Data.Repositories.Interfaces;
using BarbecueSpace.Models;

namespace BarbecueSpace.Data.Repositories
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        public PersonRepository(MyDbContext context) : base(context)
        {

        }
    }
}
