using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class LeaveRequest:Base
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("RequestingEmployeeId")]
        public Employees RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequested { get; set; }
        public string RequestComments { get; set; }
        public DateTime DateActioned { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }
        [ForeignKey("ApprovedById")]
        public Employees ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
    }
}
