

using Data;
using Data.Entities;
using Repository.Interface;

namespace Repository
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;
        private IGenericRepository<LeaveType> _leaveTypes;
        private IGenericRepository<LeaveRequest> _leaveRequests;
        private IGenericRepository<LeaveAllocation> _leaveAllocations;
        private IGenericRepository<Amount> _Amount;

        public UnitOfWork(AppDBContext context)
        {
            _context = context;
        }

        public IGenericRepository<LeaveType> LeaveTypes
            => _leaveTypes ??= new GenericRepository<LeaveType>(_context);

        public IGenericRepository<LeaveRequest> LeaveRequests
             => _leaveRequests ??= new GenericRepository<LeaveRequest>(_context);
        public IGenericRepository<LeaveAllocation> LeaveAllocations
              => _leaveAllocations ??= new GenericRepository<LeaveAllocation>(_context);


        public IGenericRepository<Amount> Amount
            => _Amount ??= new GenericRepository<Amount>(_context);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool dispose)
        {
            if (dispose)
            {
                _context.Dispose();
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
