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
    public class CompaniesController : Controller
    {
        private readonly Company_Employee_Registry_DBContext _context;

        public CompaniesController(Company_Employee_Registry_DBContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: Companies
        public async Task<IActionResult> Index()
        {
            var company_Employee_Registry_DBContext = _context.Company.Include(c => c.CompanyOwner);
            return View(await company_Employee_Registry_DBContext.ToListAsync());
        }
        [Authorize]
        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .Include(c => c.CompanyOwner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }
        [Authorize]
        // GET: Companies/Create
        public IActionResult Create()
        {
            ViewData["CompanyOwnerId"] = new SelectList(_context.Set<CompanyOwner>(), "Id", "Id");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyOwnerId,Name")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyOwnerId"] = new SelectList(_context.Set<CompanyOwner>(), "Id", "Id", company.CompanyOwnerId);
            return View(company);
        }
        [Authorize]
        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            ViewData["CompanyOwnerId"] = new SelectList(_context.Set<CompanyOwner>(), "Id", "Id", company.CompanyOwnerId);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyOwnerId,Name")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
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
            ViewData["CompanyOwnerId"] = new SelectList(_context.Set<CompanyOwner>(), "Id", "Id", company.CompanyOwnerId);
            return View(company);
        }
        [Authorize]
        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.Company
                .Include(c => c.CompanyOwner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.Company.FindAsync(id);
            _context.Company.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.Company.Any(e => e.Id == id);
        }
    }
}
