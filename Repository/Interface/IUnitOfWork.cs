

using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<LeaveType> LeaveTypes { get; }
        IGenericRepository<LeaveRequest> LeaveRequests { get; }
        IGenericRepository<Amount> Amount { get; }
        IGenericRepository<LeaveAllocation> LeaveAllocations { get; }
        Task Save();
    }
}
