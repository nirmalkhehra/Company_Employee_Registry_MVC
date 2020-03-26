using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company_Employee_Registry_MVC.Models
{
    //Company  Information
    public class Company
    {
        //Compnay  Id
        public int Id { get; set; }

        //Compnay owner related key
        public int CompanyOwnerId { get; set; }

        //Company owner reference
        public CompanyOwner CompanyOwner { get; set; }

        //Compnay owner name
        public string Name { get; set; }

    }
}

