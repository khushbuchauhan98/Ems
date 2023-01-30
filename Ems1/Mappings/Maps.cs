using AutoMapper;
using Data.Entities;
using Data.FormModels;

namespace Ems1.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationVM>().ReverseMap();
            CreateMap<Employees, RegisterViewModel>().ReverseMap();
            CreateMap<LeaveAllocation, EditLeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestVM>().ReverseMap();
            CreateMap<Amount, AmountVM>().ReverseMap();
            /*CreateMap<CheckIn, CheckInVM>().ReverseMap()*/

        }
    }
}
