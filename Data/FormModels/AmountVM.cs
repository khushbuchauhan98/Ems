
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Data.FormModels
{
    public class AmountVM
    {
        public int Id { get; set; }
      
        public RegisterViewModel RequestingEmployee { get; set; }
        [Display(Name = "Employee Name")]
        public string RequestingEmployeeId { get; set; }
        [Display(Name = "Salarydate ")]
        public DateTime Salarydate { get; set; }
        [Required]
        [Display(Name = "SalaryAmount ")]
        public double SalaryAmount { get; set; }
        [ForeignKey("CreatedbyId")]
        public RegisterViewModel Createdby { get; set; }
        public string CreatedbyId { get; set; }

        //public EmployeeVM Createdby { get; set; }
        //[Display(Name = "Approver Name")]
        //public string CreatedbyId { get; set; }

    }
    public class AmountRequestVM
    {
    //    [Display(Name = "Total Number Of Requests")]
    //    public int TotalRequests { get; set; }
    //    [Display(Name = "Approved Requests")]
    //    public int ApprovedRequests { get; set; }
    //    [Display(Name = "Pending Requests")]
    //    public int PendingRequests { get; set; }
    //    [Display(Name = "Rejected Requests")]
    //    public int RejectedRequests { get; set; }
        public List<AmountVM> Amounts { get; set; }
    }
    public class CreateAmountVM
    {
        public RegisterViewModel RequestingEmployee { get; set; }
        [Display(Name = "Employee Name")]
        public string RequestingEmployeeId { get; set; }
        [Required]
        [Display(Name = "SalaryAmount ")]
        public double SalaryAmount { get; set; }
        public RegisterViewModel EmployeeVM { get; set; }
    }
    public class EmployeeDataVm
    {
    
        public List<AmountVM> AmountVMs { get; set; }
    }
    
}
