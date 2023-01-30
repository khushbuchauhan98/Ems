
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Data.FormModels
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; }
        [Display(Name = "Number Of Days")]

        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public int Period { get; set; }

        public RegisterViewModel Employee { get; set; }
        public string EmployeeId { get; set; }

        public LeaveTypeVM LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
    }

    public class CreateLeaveAllocationVM
    {
        public int NumberUpdated { get; set; }
        public List<LeaveTypeVM> LeaveTypes { get; set; }
    }

    public class EditLeaveAllocationVM
    {
        public int Id { get; set; }

        public RegisterViewModel Employee { get; set; }
        public string EmployeeId { get; set; }
        [Display(Name = "Number Of Days")]
        [Range(1, 50, ErrorMessage = "Enter Valid Number")]
        public int NumberOfDays { get; set; }
        public LeaveTypeVM LeaveType { get; set; }

    }

    public class ViewAllocationsVM
    {
        public RegisterViewModel Employee { get; set; }
        public string EmployeeId { get; set; }
        public List<LeaveAllocationVM> LeaveAllocations { get; set; }
    }
}
