using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Employee_Registry_MVC.Models
{//Company employee registration record
    public class CompanyEmployeeRegistration
    {
        //Registration id
        public int Id { get; set; }

        //Compnay id relted key

        public int CompanyId { get; set; }

        //Employee id relted key
        public int EmployeeId { get; set; }

        //Employee relationship
        public Employee Employee { get; set; }

        //Company relationship
        public Company Company { get; set; }

    }
}
