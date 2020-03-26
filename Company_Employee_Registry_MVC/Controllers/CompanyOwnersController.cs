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
    public class CompanyOwnersController : Controller
    {
        private readonly Company_Employee_Registry_DBContext _context;

        public CompanyOwnersController(Company_Employee_Registry_DBContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: CompanyOwners
        public async Task<IActionResult> Index()
        {
            return View(await _context.CompanyOwner.ToListAsync());
        }
        [Authorize]
        // GET: CompanyOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyOwner = await _context.CompanyOwner
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyOwner == null)
            {
                return NotFound();
            }

            return View(companyOwner);
        }
        [Authorize]
        // GET: CompanyOwners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyOwners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ContactEmail")] CompanyOwner companyOwner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyOwner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyOwner);
        }
        [Authorize]
        // GET: CompanyOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyOwner = await _context.CompanyOwner.FindAsync(id);
            if (companyOwner == null)
            {
                return NotFound();
            }
            return View(companyOwner);
        }

        // POST: CompanyOwners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ContactEmail")] CompanyOwner companyOwner)
        {
            if (id != companyOwner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyOwnerExists(companyOwner.Id))
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
            return View(companyOwner);
        }
        [Authorize]
        // GET: CompanyOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyOwner = await _context.CompanyOwner
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyOwner == null)
            {
                return NotFound();
            }

            return View(companyOwner);
        }

        // POST: CompanyOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyOwner = await _context.CompanyOwner.FindAsync(id);
            _context.CompanyOwner.Remove(companyOwner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyOwnerExists(int id)
        {
            return _context.CompanyOwner.Any(e => e.Id == id);
        }
    }
}
