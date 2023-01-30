

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data;
using Data.Entities;
using Repository.Interface;
using Microsoft.AspNetCore.Identity;

namespace Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly AppDBContext _db;
        private readonly UserManager<Employees> userManager;
        public LeaveTypeRepository(AppDBContext db, UserManager<Employees> userManager)
        {
            this.userManager= userManager;
            _db = db;
        }

        public async Task<bool> Create(LeaveType entity)
        {
           

            await _db.LeaveTypes.AddAsync(entity);

            return await Save();
        }

        public async Task<bool> Delete(LeaveType entity)
        {
            _db.LeaveTypes.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveType>> FindAll()
        {
            var leaveTypes = await _db.LeaveTypes.ToListAsync();
            return leaveTypes;
        }

        public async Task<LeaveType> FindById(int id)
        {
            var leaveType = await _db.LeaveTypes.FindAsync(id);
            return leaveType;
        }

        public Task<ICollection<LeaveType>> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task<ICollection<LeaveType>> GetEmployeesByLeaveType(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<bool> isExists(int id)
        {
            var exists = await _db.LeaveTypes.AnyAsync(q => q.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(LeaveType entity)
        {
            _db.LeaveTypes.Update(entity);
            return await Save();
        }
    }
}
