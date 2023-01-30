
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{

    public class AmountRepository : IAmountRepository
    {
        private readonly AppDBContext _db;
        public AmountRepository(AppDBContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Amount entity)
        {
            await _db.Amounts.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Amount entity)
        {
            _db.Amounts.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<Amount>> FindAll()
        {
            var leaveRequests = await _db.Amounts
                .Include(q => q.RequestingEmployee)
                .Include(q => q.Createdby)
              
                .ToListAsync();
            return leaveRequests;
        }

        public async Task<Amount> FindById(int id)
        {
            var leaveRequest = await _db.Amounts
                .Include(q => q.RequestingEmployee)
                .Include(q => q.Createdby)

                .FirstOrDefaultAsync(q => q.Id == id);
            return leaveRequest;
        }

        public async Task<ICollection<Amount>> GtAmountForEmployee(string employeeid)
        {
            var leaveRequests = await FindAll();
            return leaveRequests.Where(q => q.RequestingEmployeeId == employeeid)
            .ToList();
        }

        public async Task<bool> isExists(int id)
        {
            var exists = await _db.Amounts.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Amount entity)
        {
            _db.Amounts.Update(entity);
            return await Save();
        }
    }
}
