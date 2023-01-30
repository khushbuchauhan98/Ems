
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ILeaveTypeRepository : IRepositoryBase<LeaveType>
    {
        Task<ICollection<LeaveType>> GetEmployeesByLeaveType(int id);
    }
}
