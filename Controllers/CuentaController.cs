using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Facturacion.iso810.Models;

namespace Facturacion.iso810.Controllers
{
    public class CuentaController : Controller
    {
        private readonly facturacioncompletaiso810Context _context;

        public CuentaController(facturacioncompletaiso810Context context)
        {
            _context = context;
        }

        // GET: Cuenta
        public async Task<IActionResult> Index()
        {
              return _context.Cuenta != null ? 
                          View(await _context.Cuenta.ToListAsync()) :
                          Problem("Entity set 'facturacioncompletaiso810Context.Cuenta'  is null.");
        }

        // GET: Cuenta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cuenta == null)
            {
                return NotFound();
            }

            var cuentum = await _context.Cuenta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuentum == null)
            {
                return NotFound();
            }

            return View(cuentum);
        }

        // GET: Cuenta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cuenta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NoCuenta,Activo")] Cuentum cuentum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuentum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cuentum);
        }

        // GET: Cuenta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cuenta == null)
            {
                return NotFound();
            }

            var cuentum = await _context.Cuenta.FindAsync(id);
            if (cuentum == null)
            {
                return NotFound();
            }
            return View(cuentum);
        }

        // POST: Cuenta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NoCuenta,Activo")] Cuentum cuentum)
        {
            if (id != cuentum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuentum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuentumExists(cuentum.Id))
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
            return View(cuentum);
        }

        // GET: Cuenta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cuenta == null)
            {
                return NotFound();
            }

            var cuentum = await _context.Cuenta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuentum == null)
            {
                return NotFound();
            }

            return View(cuentum);
        }

        // POST: Cuenta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cuenta == null)
            {
                return Problem("Entity set 'facturacioncompletaiso810Context.Cuenta'  is null.");
            }
            var cuentum = await _context.Cuenta.FindAsync(id);
            if (cuentum != null)
            {
                _context.Cuenta.Remove(cuentum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuentumExists(int id)
        {
          return (_context.Cuenta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
