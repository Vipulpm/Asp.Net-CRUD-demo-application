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
    public class DebitStatementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DebitStatementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DebitStatements
        public async Task<IActionResult> Index()
        {
              return _context.Debits != null ? 
                          View(await _context.Debits.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Debits'  is null.");
        }

        // GET: DebitStatements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Debits == null)
            {
                return NotFound();
            }

            var debitStatement = await _context.Debits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (debitStatement == null)
            {
                return NotFound();
            }

            return View(debitStatement);
        }

        // GET: DebitStatements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DebitStatements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OwnerName,Address,Mobile,Product,Amount,Description,Date")] DebitStatement debitStatement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(debitStatement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(debitStatement);
        }

        // GET: DebitStatements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Debits == null)
            {
                return NotFound();
            }

            var debitStatement = await _context.Debits.FindAsync(id);
            if (debitStatement == null)
            {
                return NotFound();
            }
            return View(debitStatement);
        }

        // POST: DebitStatements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerName,Address,Mobile,Product,Amount,Description,Date")] DebitStatement debitStatement)
        {
            if (id != debitStatement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(debitStatement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DebitStatementExists(debitStatement.Id))
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
            return View(debitStatement);
        }

        // GET: DebitStatements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Debits == null)
            {
                return NotFound();
            }

            var debitStatement = await _context.Debits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (debitStatement == null)
            {
                return NotFound();
            }

            return View(debitStatement);
        }

        // POST: DebitStatements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Debits == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Debits'  is null.");
            }
            var debitStatement = await _context.Debits.FindAsync(id);
            if (debitStatement != null)
            {
                _context.Debits.Remove(debitStatement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DebitStatementExists(int id)
        {
          return (_context.Debits?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
