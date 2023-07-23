using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LEDGER_BOOK.Data;
using LEDGER_BOOK.Models;

namespace LEDGER_BOOK.Controllers
{
    public class CreditStatementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CreditStatementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CreditStatements
        public async Task<IActionResult> Index()
        {
              return _context.Credits != null ? 
                          View(await _context.Credits.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Credits'  is null.");
        }

        // GET: CreditStatements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Credits == null)
            {
                return NotFound();
            }

            var creditStatement = await _context.Credits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditStatement == null)
            {
                return NotFound();
            }

            return View(creditStatement);
        }

        // GET: CreditStatements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CreditStatements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerName,Address,Mobile,Product,Amount,Description,Date")] CreditStatement creditStatement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(creditStatement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(creditStatement);
        }

        // GET: CreditStatements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Credits == null)
            {
                return NotFound();
            }

            var creditStatement = await _context.Credits.FindAsync(id);
            if (creditStatement == null)
            {
                return NotFound();
            }
            return View(creditStatement);
        }

        // POST: CreditStatements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerName,Address,Mobile,Product,Amount,Description,Date")] CreditStatement creditStatement)
        {
            if (id != creditStatement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(creditStatement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreditStatementExists(creditStatement.Id))
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
            return View(creditStatement);
        }

        // GET: CreditStatements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Credits == null)
            {
                return NotFound();
            }

            var creditStatement = await _context.Credits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditStatement == null)
            {
                return NotFound();
            }

            return View(creditStatement);
        }

        // POST: CreditStatements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Credits == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Credits'  is null.");
            }
            var creditStatement = await _context.Credits.FindAsync(id);
            if (creditStatement != null)
            {
                _context.Credits.Remove(creditStatement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditStatementExists(int id)
        {
          return (_context.Credits?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
