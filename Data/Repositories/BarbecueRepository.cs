using BarbecueSpace.Data.Repositories.Interfaces;
using BarbecueSpace.Models;

namespace BarbecueSpace.Data.Repositories
{ 
    public class BarbecueRepository : RepositoryBase<Barbecue>, IBarbecueRepository
{
    public BarbecueRepository(MyDbContext context) : base(context)
    {

    }
}
}
