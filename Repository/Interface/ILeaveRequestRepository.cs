using Data.Entities;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ILeaveRequestRepository : IRepositoryBase<LeaveRequest>
    {
        Task<ICollection<LeaveRequest>> GetLeaveRequestsByEmployee(string employeeid);
    }
}
