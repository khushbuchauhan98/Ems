using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Amount :Base
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("RequestingEmployeeId")]
        public Employees RequestingEmployee { get; set; }
        public string RequestingEmployeeId { get; set; }
        public DateTime Salarydate { get; set; }
        public double SalaryAmount { get; set; }
      
        [ForeignKey("CreatedbyId")]
        public Employees Createdby { get; set; }
        public string CreatedbyId { get; set; }
    }
}
