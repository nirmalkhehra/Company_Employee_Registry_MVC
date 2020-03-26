using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Company_Employee_Registry_MVC.Data;
using Company_Employee_Registry_MVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace Company_Employee_Registry_MVC.Controllers
{
    public class CompanyEmployeeRegistrationsController : Controller
    {
        private readonly Company_Employee_Registry_DBContext _context;

        public CompanyEmployeeRegistrationsController(Company_Employee_Registry_DBContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: CompanyEmployeeRegistrations
        public async Task<IActionResult> Index()
        {
            var company_Employee_Registry_DBContext = _context.CompanyEmployeeRegistration.Include(c => c.Company).Include(c => c.Employee);
            return View(await company_Employee_Registry_DBContext.ToListAsync());
        }
        [Authorize]
        // GET: CompanyEmployeeRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyEmployeeRegistration = await _context.CompanyEmployeeRegistration
                .Include(c => c.Company)
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyEmployeeRegistration == null)
            {
                return NotFound();
            }

            return View(companyEmployeeRegistration);
        }
        [Authorize]
        // GET: CompanyEmployeeRegistrations/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id");
            ViewData["EmployeeId"] = new SelectList(_context.Set<Employee>(), "Id", "Id");
            return View();
        }

        // POST: CompanyEmployeeRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyId,EmployeeId")] CompanyEmployeeRegistration companyEmployeeRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyEmployeeRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id", companyEmployeeRegistration.CompanyId);
            ViewData["EmployeeId"] = new SelectList(_context.Set<Employee>(), "Id", "Id", companyEmployeeRegistration.EmployeeId);
            return View(companyEmployeeRegistration);
        }
        [Authorize]
        // GET: CompanyEmployeeRegistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyEmployeeRegistration = await _context.CompanyEmployeeRegistration.FindAsync(id);
            if (companyEmployeeRegistration == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id", companyEmployeeRegistration.CompanyId);
            ViewData["EmployeeId"] = new SelectList(_context.Set<Employee>(), "Id", "Id", companyEmployeeRegistration.EmployeeId);
            return View(companyEmployeeRegistration);
        }

        // POST: CompanyEmployeeRegistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyId,EmployeeId")] CompanyEmployeeRegistration companyEmployeeRegistration)
        {
            if (id != companyEmployeeRegistration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyEmployeeRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyEmployeeRegistrationExists(companyEmployeeRegistration.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Id", companyEmployeeRegistration.CompanyId);
            ViewData["EmployeeId"] = new SelectList(_context.Set<Employee>(), "Id", "Id", companyEmployeeRegistration.EmployeeId);
            return View(companyEmployeeRegistration);
        }
        [Authorize]
        // GET: CompanyEmployeeRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyEmployeeRegistration = await _context.CompanyEmployeeRegistration
                .Include(c => c.Company)
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyEmployeeRegistration == null)
            {
                return NotFound();
            }

            return View(companyEmployeeRegistration);
        }

        // POST: CompanyEmployeeRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyEmployeeRegistration = await _context.CompanyEmployeeRegistration.FindAsync(id);
            _context.CompanyEmployeeRegistration.Remove(companyEmployeeRegistration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyEmployeeRegistrationExists(int id)
        {
            return _context.CompanyEmployeeRegistration.Any(e => e.Id == id);
        }
    }
}
