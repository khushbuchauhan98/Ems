

using Data.Entities;

namespace Repository.Interface
{
    
    public interface IAmountRepository : IRepositoryBase<Amount>
    {
        Task<ICollection<Amount>> GtAmountForEmployee(string employeeid);
    }
}
