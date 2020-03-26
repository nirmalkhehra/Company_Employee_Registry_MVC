using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Company_Employee_Registry_MVC.Models;

namespace Company_Employee_Registry_MVC.Data
{
    public class Company_Employee_Registry_DBContext : DbContext
    {
        public Company_Employee_Registry_DBContext (DbContextOptions<Company_Employee_Registry_DBContext> options)
            : base(options)
        {
        }

        public DbSet<Company_Employee_Registry_MVC.Models.Company> Company { get; set; }

        public DbSet<Company_Employee_Registry_MVC.Models.CompanyEmployeeRegistration> CompanyEmployeeRegistration { get; set; }

        public DbSet<Company_Employee_Registry_MVC.Models.CompanyOwner> CompanyOwner { get; set; }

        public DbSet<Company_Employee_Registry_MVC.Models.Employee> Employee { get; set; }
    }
}
