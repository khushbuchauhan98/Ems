using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Employees : BaseEntities
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string address { get; set; }
        public DateTime DateofJoin { get; set; }
        public DateTime DateofBirth { get; set; }
        public string AadharNumber { get; set; }
        
    }
}
