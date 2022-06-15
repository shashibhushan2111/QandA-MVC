using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QandA_MVC.Data;
using QandA_MVC.Models;

namespace QandA_MVC.Controllers
{
    public class QandAsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QandAsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QandAs
        public async Task<IActionResult> Index()
        {
            return _context.QandA != null ?
                        View(await _context.QandA.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.QandA'  is null.");
        }

        // GET: QandAs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.QandA == null)
            {
                return NotFound();
            }

            var qandA = await _context.QandA
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qandA == null)
            {
                return NotFound();
            }

            return View(qandA);
        }

        // GET: QandAs/Create
        public IActionResult Create()
        {
            return View();
        }
        //GET: QandAs/ShowSearchForm
        public IActionResult ShowSearchForm()
        {
            return View();
        }

        public async Task<IActionResult> ShowSearchResultAsync(string SearchPhrase, string SearchAns)
        {

            return View("Index", await _context.QandA.Where(Q => Q.Questions.Contains(SearchPhrase)).ToListAsync());

        }


        // POST: QandAs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Questions,Answer")] QandA qandA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qandA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(qandA);
        }

        // GET: QandAs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.QandA == null)
            {
                return NotFound();
            }

            var qandA = await _context.QandA.FindAsync(id);
            if (qandA == null)
            {
                return NotFound();
            }
            return View(qandA);
        }

        // POST: QandAs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Questions,Answer")] QandA qandA)
        {
            if (id != qandA.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qandA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QandAExists(qandA.Id))
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
            return View(qandA);
        }

        // GET: QandAs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.QandA == null)
            {
                return NotFound();
            }

            var qandA = await _context.QandA
                .FirstOrDefaultAsync(m => m.Id == id);
            if (qandA == null)
            {
                return NotFound();
            }

            return View(qandA);
        }

        // POST: QandAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.QandA == null)
            {
                return Problem("Entity set 'ApplicationDbContext.QandA'  is null.");
            }
            var qandA = await _context.QandA.FindAsync(id);
            if (qandA != null)
            {
                _context.QandA.Remove(qandA);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QandAExists(int id)
        {
          return (_context.QandA?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
